using System.Collections.Generic;
using System.Linq;

public class SkillTreeScreenPresenter {
    public readonly SkillTreePresenter SkillTreePresenter;
    public readonly SkillOptionsPresenter SkillOptionsPresenter;

    public SkillTreeScreenPresenter(SkillTreeData skillTreeData, SkillPointsData skillPointsData) {
        SkillTreePresenter = new SkillTreePresenter(skillTreeData);
        SkillOptionsPresenter = new SkillOptionsPresenter(skillTreeData, skillPointsData);
    }
}