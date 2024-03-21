using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillTreeData {
    public event Action SelectedSkillChanged;

    public SkillData SelectedSkill
        => _selectedSkill;
    
    public IReadOnlyList<SkillData> Skills
        => _skills.Values.ToList();
    
    public IReadOnlyList<(string fromId, string toId)> Connections
        => _connections;
    
    private Dictionary<string, SkillData> _skills;
    private SkillData _selectedSkill;
    
    private List<(string fromId, string toId)> _connections;

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
        _connections = new List<(string fromId, string toId)>();
        
        foreach (var skillConfig in container.Skills) {
            if (!_skills.ContainsKey(skillConfig.Id))
                continue;
            
            foreach (var requirement in skillConfig.Requirements) {
                //Вполне возможно, что один из скиллов мог быть убран из списка скиллов, но не убран из Requirement'ов других скиллов,
                //поэтому, если такого скилла нет в списке _skills, то его не должно быть и в connection'ах.
                if (!_skills.ContainsKey(requirement.Id))
                    continue;

                if (!_skills[skillConfig.Id].HasConnectionWithSkill(_skills[requirement.Id]))
                    _skills[skillConfig.Id].TryAddConnection(_skills[requirement.Id]);
                
                if (!_skills[requirement.Id].HasConnectionWithSkill(_skills[skillConfig.Id]))
                    _skills[requirement.Id].TryAddConnection(_skills[skillConfig.Id]);
                
                var foundConnection = _connections.FirstOrDefault(x =>
                    (x.fromId == skillConfig.Id && x.toId == requirement.Id) ||
                    (x.fromId == requirement.Id && x.toId == skillConfig.Id));
                
                if (foundConnection != default)
                    continue;
                
                _connections.Add((skillConfig.Id, requirement.Id));
            }
        }
    }

    public void Select(string skillId) {
        if (!_skills.ContainsKey(skillId))
            throw new Exception("You are trying to select skill that do not exist!");
        
        _selectedSkill = _skills[skillId];
        SelectedSkillChanged?.Invoke();
    }

    public void LearnSelectedSkill(out int cost) {
        if (_selectedSkill == null) {
            throw new Exception("You are trying to learn selected skill but it doesn't exist!");
        }
        
        _selectedSkill.TryLearn();
        cost = _selectedSkill.Cost;
        SelectedSkillChanged?.Invoke();
    }

    private void ForgetSkill(SkillData skill, out int cost) {
        skill.TryForget();
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
            if (skill.Value.IsLearned && !skill.Value.IsBaseSkill) {
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