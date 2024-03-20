using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillData {
    public event Action Changed;
    
    public String Id { get; private set; }
    public String Name { get; private set; }
    public Vector2 Position { get; private set; }
    public int Cost { get; private set; }
    public bool IsLearnedAtTheBeginning { get; private set; }
    public List<SkillData> Connections { get; private set; }

    public bool IsLearned
    {
        get
        {
            return _isLearned;
        }
        set
        {
            _isLearned = value;
            Changed?.Invoke();
        }
    }

    private bool _isLearned;

    public SkillData(SkillConfigSO config) {
        Id = config.Id;
        Name = config.Name;
        Position = config.Position;
        Cost = config.Cost;
        Connections = new List<SkillData>();
        IsLearnedAtTheBeginning = config.IsLearnedAtTheBeginning;
        IsLearned = config.IsLearnedAtTheBeginning;
    }

    public bool HasConnectionWithSkill(SkillData skill) {
        return Connections.Contains(skill);
    }

    public void AddConnection(SkillData skill) {
        if (Connections.Contains(skill))
            throw new Exception("You're trying to add a connection to a skill, but this connection is already exists!");

        Connections.Add(skill);
    }

    public void Learn() {
        if (IsLearned) {
            throw new Exception("You are trying to learn selected skill that was already learned!");
        }
        
        IsLearned = true;
    }

    public void Forget() {
        if (!IsLearned)
            throw new Exception($"You are trying to forget a skill {Name} that has not been learned");
        
        if (IsLearnedAtTheBeginning)
            throw new Exception($"You are trying to forget a skill {Name} that learned from the beginning");

        IsLearned = false;
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
        if (IsLearnedAtTheBeginning || !IsLearned)
            return false;
        
        foreach (var anotherSkill in Connections) {
            if (!anotherSkill.IsLearned || anotherSkill.IsLearnedAtTheBeginning)
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