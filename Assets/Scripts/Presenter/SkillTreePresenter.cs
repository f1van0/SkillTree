using System;
using System.Collections.Generic;

public class SkillTreePresenter {
    private SkillTreeData _data;
    private List<SkillPresenter> _skillPresenters;

    public SkillTreePresenter(SkillTreeData data) {
        _data = data;

        _skillPresenters = new List<SkillPresenter>();
        foreach (var skill in _data.Skills) {
            _skillPresenters.Add(new SkillPresenter(skill.Value));
        }
    }

    public List<SkillPresenter> GetSkillPresenters() {
        return _skillPresenters;
    }

    public void Select(String skillId) {
        _data.Select(skillId);
    }
}