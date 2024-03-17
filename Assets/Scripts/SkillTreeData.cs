using System;
using System.Collections.Generic;

public class SkillTreeData {
    public Dictionary<string, SkillData> Skills;
    public SkillData SelectedSkill;
    public int AvailableSkillPoints;

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

    public void EarnSkillPoint() {
        AvailableSkillPoints++;
    }

    public void Select(String skillId) {
        SelectedSkill = GetSkillById(skillId);
    }

    public SkillData GetSkillById(String skillId) {
        return Skills[skillId];
    }

    public void LearnSelectedSkill() {
        SelectedSkill.IsLearned = true;
        AvailableSkillPoints -= SelectedSkill.Cost;
    }

    public void ForgetSkill(SkillData skill, bool forceMode = false) {
        if (!skill.IsLearned)
            throw new Exception($"You are trying to forget a skill {skill.Name} that has not been learned");
        
        if (skill.IsLearnedAtTheBeginning)
            throw new Exception($"You are trying to forget a skill {skill.Name} that learned from the beginning");
        
        skill.IsLearned = false;
        AvailableSkillPoints += skill.Cost;
    }

    public void ForgetSelectedSkill() {
        ForgetSkill(SelectedSkill);
    }

    public void ForgetAllSkills() {
        foreach (var skill in Skills) {
            if (skill.Value.IsLearned && !skill.Value.IsLearnedAtTheBeginning) {
                ForgetSkill(skill.Value, true);
            }
        }
    }

    public bool CanLearnSelectedSkill() {
        if (SelectedSkill == null || SelectedSkill.IsLearned || SelectedSkill.Cost > AvailableSkillPoints)
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