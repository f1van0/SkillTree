using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillTreeData {
    public event Action SelectedSkillChanged;
    
    public Dictionary<string, SkillData> Skills;
    public SkillData SelectedSkill;

    public SkillTreeData(SkillsContainerSO container) {
        Skills = new Dictionary<string, SkillData>();
        SelectedSkill = null;
        
        foreach (var skillConfig in container.Skills) {
            var newSkill = new SkillData(skillConfig);
            Skills.Add(skillConfig.Id, newSkill);
        }

        EstablishTwoWayConnection(container);
    }

    public void EstablishTwoWayConnection(SkillsContainerSO container) {
        foreach (var skillConfig in container.Skills) {
            if (!Skills.ContainsKey(skillConfig.Id))
                continue;
            
            foreach (var requirement in skillConfig.Requirements) {
                if (!Skills.ContainsKey(requirement.Id))
                    continue;

                if (!Skills[skillConfig.Id].HasConnectionWithSkill(Skills[requirement.Id]))
                    Skills[skillConfig.Id].AddConnection(Skills[requirement.Id]);
                
                if (!Skills[requirement.Id].HasConnectionWithSkill(Skills[skillConfig.Id]))
                    Skills[requirement.Id].AddConnection(Skills[skillConfig.Id]);
            }
        }
    }

    public void Select(String skillId) {
        if (!Skills.ContainsKey(skillId))
            throw new Exception("You are trying to select skill that do not exist!");
        
        SelectedSkill = Skills[skillId];
        SelectedSkillChanged?.Invoke();
    }

    public void LearnSelectedSkill(out int cost) {
        cost = SelectedSkill.Cost;

        if (SelectedSkill == null) {
            throw new Exception("You are trying to learn selected skill but it doesn't exist!");
        }
        
        if (SelectedSkill.IsLearned) {
            throw new Exception("You are trying to learn selected skill that was already learned!");
        }
        
        SelectedSkill.IsLearned = true;
        SelectedSkillChanged?.Invoke();
    }

    private void ForgetSkill(SkillData skill, out int cost) {
        if (!skill.IsLearned)
            throw new Exception($"You are trying to forget a skill {skill.Name} that has not been learned");
        
        if (skill.IsLearnedAtTheBeginning)
            throw new Exception($"You are trying to forget a skill {skill.Name} that learned from the beginning");
        
        skill.IsLearned = false;
        cost = skill.Cost;
        SelectedSkillChanged?.Invoke();
    }

    public void ForgetSelectedSkill(out int cost) {
        ForgetSkill(SelectedSkill, out cost);
    }

    public void ForgetAllSkills(out int totalCost) {
        totalCost = 0;
        int cost = 0;
        foreach (var skill in Skills) {
            if (skill.Value.IsLearned && !skill.Value.IsLearnedAtTheBeginning) {
                ForgetSkill(skill.Value, out cost);
                totalCost += cost;
                
                if (skill.Value == SelectedSkill)
                    SelectedSkillChanged?.Invoke();
            }
        }
    }

    public bool CanLearnSelectedSkill(int amountOfSkillPoints) {
        if (SelectedSkill == null || SelectedSkill.IsLearned || SelectedSkill.Cost > amountOfSkillPoints)
            return false;

        foreach (var requirement in SelectedSkill.Connections) {
            if (requirement.IsLearned)
                return true;
        }

        return false;
    }

    public bool CanForgetSelectedSkill() {
        if (SelectedSkill == null || SelectedSkill.IsLearnedAtTheBeginning || !SelectedSkill.IsLearned)
            return false;

        foreach (var anotherSkill in SelectedSkill.Connections) {
            if (!anotherSkill.IsLearned || anotherSkill.IsLearnedAtTheBeginning)
                continue;

            if (!IsSkillConnectedWithBaseSkill(anotherSkill, SelectedSkill))
                return false;
        }
        
        return true;
    }

    public bool IsSkillConnectedWithBaseSkill(SkillData skillData, SkillData ignoreSkill = null) {
        HashSet<SkillData> visitedSkills = new HashSet<SkillData>();

        bool hasConnectionWithBaseSkill = TryToFindBaseSkillInConnections(skillData);

        return hasConnectionWithBaseSkill;
        
        bool TryToFindBaseSkillInConnections(SkillData skillData) {
            foreach (var connectedSkill in skillData.Connections) {
                if (connectedSkill.IsLearnedAtTheBeginning)
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