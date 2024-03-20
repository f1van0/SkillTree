using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillTree/SkillsContainer", fileName = "SkillsContainer", order = 0)]
public class SkillsContainerSO : ScriptableObject {
    public List<SkillConfigSO> Skills;
}