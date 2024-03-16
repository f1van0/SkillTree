using System.Collections.Generic;

public class SkillTreeData {
    public List<SkillData> Skills;
    public SkillData SelectedSkill;

    public SkillTreeData(SkillsContainerSO container) {
        Skills = new List<SkillData>();
        SelectedSkill = null;
        
        foreach (var skillConfig in container.Skills) {
            var newSkill = new SkillData(skillConfig);
            Skills.Add(newSkill);
        }
    }
}