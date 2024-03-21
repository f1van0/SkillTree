using System;

namespace Model {
    public class SkillTreeProgressionData {
        public event Action SelectedSkillChanged;
        public event Action<int> ChangedAmountOfAvailableSkillPoints;
        
        public int AmountOfAvailableSkillPoints
            => _skillPointsData.AmountOfSkillPoints;

        public SkillData SelectedSkill
            => _skillTreeData.SelectedSkill;

        private SkillTreeData _skillTreeData;
        private SkillPointsData _skillPointsData;

        public SkillTreeProgressionData(SkillTreeData skillTreeData, SkillPointsData skillPointsData) {
            _skillTreeData = skillTreeData;
            _skillPointsData = skillPointsData;

            _skillTreeData.SelectedSkillChanged += HandleSelectedSkillChanged;
            _skillPointsData.ChangedAmountOfSkillPoints += HandleAmountOfSkillPointsChanged;
        }

        private void HandleSelectedSkillChanged() {
            SelectedSkillChanged?.Invoke();
        }

        private void HandleAmountOfSkillPointsChanged(int amount) {
            ChangedAmountOfAvailableSkillPoints?.Invoke(amount);
        }

        public void EarnSkillPoint() {
            _skillPointsData.AddSkillPoints(1);
        }

        public void LearnSelectedSkill() {
            _skillTreeData.LearnSelectedSkill(out var cost);
            _skillPointsData.AddSkillPoints(-cost);
        }

        public void ForgetSelectedSkill() {
            _skillTreeData.ForgetSelectedSkill(out var cost);
            _skillPointsData.AddSkillPoints(cost);
        }

        public void ForgetAllSkills() {
            _skillTreeData.ForgetAllSkills(out var cost);
            _skillPointsData.AddSkillPoints(cost);
        }

        public bool CanLearnSelectedSkill() {
            return _skillTreeData.CanLearnSelectedSkill(AmountOfAvailableSkillPoints);
        }

        public bool CanForgetSelectedSkill() {
            return _skillTreeData.CanForgetSelectedSkill();
        }
    }
}