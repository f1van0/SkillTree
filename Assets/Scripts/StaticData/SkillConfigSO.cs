using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillTree/SkillConfig", fileName = "SkillConfig", order = 0)]
public class SkillConfigSO : ScriptableObject {
    [HideInInspector] public String Id;
    
    public String Name;
    public Vector2 Position;
    public int Cost;
    public bool IsLearnedAtTheBeginning;
    public List<SkillConfigSO> Requirements;

    private void OnValidate() {
        if (Id == "")
            Id = Guid.NewGuid().ToString();
    }
}