using Model;

namespace Presenter {
    public class SkillTreeScreenPresenter {
        public readonly SkillTreePresenter SkillTreePresenter;
        public readonly SkillOptionsPresenter SkillOptionsPresenter;

        public SkillTreeScreenPresenter(
            SkillTreeProgressionData skillTreeProgressionData,
            SkillTreeData skillTreeData,
            SkillPointsData skillPointsData)
        {
            SkillTreePresenter = new SkillTreePresenter(skillTreeData);
            SkillOptionsPresenter = new SkillOptionsPresenter(skillTreeProgressionData);
        }
    }
}