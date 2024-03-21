using System;
using System.Collections.Generic;
using StaticData;
using UnityEngine;

namespace Model {
    public class SkillData {
        public event Action Changed;
    
        public string Id { get; private set; }
        public string Name { get; private set; }
        public Vector2 Position { get; private set; }
        public int Cost { get; private set; }
        public bool IsBaseSkill { get; private set; }
        public List<SkillData> Connections { get; private set; }

        public bool IsLearned
        {
            get
            {
                return _isLearned;
            }
            set
            {
                if (_isLearned != value) {
                    _isLearned = value;
                    Changed?.Invoke();
                }
            }
        }

        private bool _isLearned;

        public SkillData(SkillConfigSO config) {
            Id = config.Id;
            Name = config.Name;
            Position = config.Position;
            Cost = config.Cost;
            Connections = new List<SkillData>();
            IsBaseSkill = config.IsBaseSkill;
            IsLearned = config.IsBaseSkill;
        }

        public bool HasConnectionWithSkill(SkillData skill) {
            return Connections.Contains(skill);
        }

        public bool TryAddConnection(SkillData skill) {
            if (Connections.Contains(skill))
                return false;

            Connections.Add(skill);
            return true;
        }

        public bool TryLearn() {
            if (IsLearned) {
                return true;
            }
        
            IsLearned = true;
            return true;
        }

        public bool TryForget() {
            if (!IsLearned || IsBaseSkill)
                return false;

            IsLearned = false;
            return true;
        }

        public bool CanBeLearned(int amountOfSkillPoints) {
            if (IsLearned || Cost > amountOfSkillPoints)
                return false;

            foreach (var requirement in Connections) {
                if (requirement.IsLearned)
                    return true;
            }
        
            return false;
        }

        public bool CanBeForgotten() {
            if (IsBaseSkill || !IsLearned)
                return false;
        
            foreach (var anotherSkill in Connections) {
                if (!anotherSkill.IsLearned || anotherSkill.IsBaseSkill)
                    continue;

                if (!IsSkillConnectedWithBaseSkill(anotherSkill, this))
                    return false;
            }

            return true;
        }

        private bool IsSkillConnectedWithBaseSkill(SkillData fromSkill, SkillData ignoreSkill) {
            HashSet<SkillData> visitedSkills = new HashSet<SkillData>();

            bool hasConnectionWithBaseSkill = TryToFindBaseSkillInConnections(fromSkill);

            return hasConnectionWithBaseSkill;
        
            bool TryToFindBaseSkillInConnections(SkillData skillData) {
                foreach (var connectedSkill in skillData.Connections) {
                    if (connectedSkill.IsBaseSkill)
                        return true;
                
                    if (visitedSkills.Contains(connectedSkill) || connectedSkill == ignoreSkill)
                        continue;

                    if (connectedSkill.IsLearned) {
                        visitedSkills.Add(connectedSkill);
                        bool result = TryToFindBaseSkillInConnections(connectedSkill);
                        if (result)
                            return true;
                    }
                }

                return false;
            }
        }
    }
}