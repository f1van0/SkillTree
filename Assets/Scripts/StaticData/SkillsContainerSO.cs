using System.Collections.Generic;
using UnityEngine;

namespace StaticData {
    [CreateAssetMenu(menuName = "SkillTree/SkillsContainer", fileName = "SkillsContainer", order = 0)]
    public class SkillsContainerSO : ScriptableObject {
        [field: SerializeField] public List<SkillConfigSO> Skills { get; private set; }
    }
}