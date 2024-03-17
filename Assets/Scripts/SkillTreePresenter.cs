using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SkillTreePresenter {
    private SkillTreeData _data;

    public SkillTreePresenter(SkillTreeData data) {
        _data = data;
    }

    public List<SkillData> GetSkills() {
        return _data.Skills.Values.ToList();
    }

    public SkillData GetSelectedSkill() {
        return _data.SelectedSkill;
    }

    public int GetAmountOfAvailableSkillPoints() {
        return _data.AvailableSkillPoints;
    }

    public void EarnSkillPoint() {
        _data.EarnSkillPoint();
    }

    public void Select(String skillId) {
        _data.Select(skillId);
    }

    public bool CanLearnSelectedSkill() {
        return _data.CanLearnSelectedSkill();
    }

    public bool CanForgetSelectedSkill() {
        return _data.CanForgetSelectedSkill();
    }

    public void LearnSelectedSkill() {
        _data.LearnSelectedSkill();
    }

    public void ForgetSelectedSkill() {
        _data.ForgetSelectedSkill();
    }

    public void ForgetAllSkills() {
        _data.ForgetAllSkills();
    }

    public SkillData GetSkillById(String skillId) {
        return _data.GetSkillById(skillId);
    }
}