using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillTreeData {
    public event Action SelectedSkillChanged;
    
    private Dictionary<string, SkillData> _skills;
    private SkillData _selectedSkill;

    public SkillTreeData(SkillsContainerSO container) {
        _skills = new Dictionary<string, SkillData>();
        _selectedSkill = null;
        
        foreach (var skillConfig in container.Skills) {
            var newSkill = new SkillData(skillConfig);
            _skills.Add(skillConfig.Id, newSkill);
        }

        EstablishTwoWayConnection(container);
    }

    private void EstablishTwoWayConnection(SkillsContainerSO container) {
        foreach (var skillConfig in container.Skills) {
            if (!_skills.ContainsKey(skillConfig.Id))
                continue;
            
            foreach (var requirement in skillConfig.Requirements) {
                if (!_skills.ContainsKey(requirement.Id))
                    continue;

                if (!_skills[skillConfig.Id].HasConnectionWithSkill(_skills[requirement.Id]))
                    _skills[skillConfig.Id].AddConnection(_skills[requirement.Id]);
                
                if (!_skills[requirement.Id].HasConnectionWithSkill(_skills[skillConfig.Id]))
                    _skills[requirement.Id].AddConnection(_skills[skillConfig.Id]);
            }
        }
    }

    public SkillData[] GetSkills() {
        return _skills.Values.ToArray();
    }
    
    public SkillData GetSelectedSkill() {
        return _selectedSkill;
    }

    public void Select(String skillId) {
        if (!_skills.ContainsKey(skillId))
            throw new Exception("You are trying to select skill that do not exist!");
        
        _selectedSkill = _skills[skillId];
        SelectedSkillChanged?.Invoke();
    }

    public void LearnSelectedSkill(out int cost) {
        if (_selectedSkill == null) {
            throw new Exception("You are trying to learn selected skill but it doesn't exist!");
        }
        
        _selectedSkill.Learn();
        cost = _selectedSkill.Cost;
        SelectedSkillChanged?.Invoke();
    }

    private void ForgetSkill(SkillData skill, out int cost) {
        skill.Forget();
        cost = skill.Cost;
        SelectedSkillChanged?.Invoke();
    }

    public void ForgetSelectedSkill(out int cost) {
        if (_selectedSkill == null) {
            throw new Exception("You are trying to forget selected skill but it doesn't exist!");
        }
        
        ForgetSkill(_selectedSkill, out cost);
    }

    public void ForgetAllSkills(out int totalCost) {
        totalCost = 0;
        int cost = 0;
        foreach (var skill in _skills) {
            if (skill.Value.IsLearned && !skill.Value.IsLearnedAtTheBeginning) {
                ForgetSkill(skill.Value, out cost);
                totalCost += cost;
                
                if (skill.Value == _selectedSkill)
                    SelectedSkillChanged?.Invoke();
            }
        }
    }

    public bool CanLearnSelectedSkill(int amountOfSkillPoints) {
        if (_selectedSkill == null)
            return false;

        return _selectedSkill.CanBeLearned(amountOfSkillPoints);
    }

    public bool CanForgetSelectedSkill() {
        if (_selectedSkill == null)
            return false;

        return _selectedSkill.CanBeForgotten();
    }
}