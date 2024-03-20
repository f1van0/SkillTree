using System;
using System.Collections.Generic;
using System.Linq;

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

    public List<(string fromId, string toId)> GetConnectionBetweenSkills() {
        List<(string fromId, string toId)> connections = new List<(string fromId, string toId)>();
        foreach (var skill in _skillPresenters) {
            List<(string fromId, string toId)> skillConnections =  skill.GetConnections();

            foreach (var skillConnection in skillConnections) {
                var foundConnection = connections.FirstOrDefault(x =>
                    (x.fromId == skillConnection.fromId && x.toId == skillConnection.toId) ||
                    (x.fromId == skillConnection.toId && x.toId == skillConnection.fromId));
                
                if (foundConnection != default)
                    continue;
                
                connections.Add(skillConnection);
            }
        }

        return connections;
    }
}