using System;

[Serializable]
public class SkillData {
    public SkillConfigSO Config;
    public bool IsLearned;

    public SkillData(SkillConfigSO config) {
        Config = config;
        IsLearned = config.IsLearnedAtTheBeginning;
    }
}