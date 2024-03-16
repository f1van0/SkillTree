using System;
using System.Collections;
using System.Collections.Generic;

public class SkillTreePresenter {
    private SkillTreeData _skillTreeData;

    public SkillTreePresenter(SkillTreeData skillTreeData) {
        _skillTreeData = skillTreeData;
    }

    public List<SkillData> GetSkills() {
        return _skillTreeData.Skills;
    }

    public void Select(Guid skillId) {
        
    }

    public void Learn(Guid skillId) {
        
    }

    public void Forget(Guid skillId) {
        
    }
}