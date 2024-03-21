using System.Collections.Generic;
using Model;

namespace Presenter {
    public class SkillTreePresenter {
        public IReadOnlyList<SkillPresenter> Skills
            => _skills;

        public IReadOnlyList<(string fromId, string toId)> Connections
            => _skillTreeData.Connections;
    
        private SkillTreeData _skillTreeData;
        private List<SkillPresenter> _skills;

        public SkillTreePresenter(SkillTreeData skillTreeData) {
            _skillTreeData = skillTreeData;

            _skills = new List<SkillPresenter>();
            foreach (var skill in _skillTreeData.Skills) {
                var newSkillPresenter = new SkillPresenter(skill);
                newSkillPresenter.Selected += Select;
                _skills.Add(newSkillPresenter);
            }
        }

        public void Select(string skillId) {
            _skillTreeData.Select(skillId);
        }
    }
}