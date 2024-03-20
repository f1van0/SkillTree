using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SkillConfigSO))]
[CanEditMultipleObjects]
public class SkillConfigSO_Editor : Editor {
    public override void OnInspectorGUI()
    {
        SkillConfigSO skillConfigSo = (SkillConfigSO)target;
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Generate new Id")) {
            skillConfigSo.GenerateNewId();
        }
    }
}

#endif

[CreateAssetMenu(menuName = "SkillTree/SkillConfig", fileName = "SkillConfig", order = 0)]
public class SkillConfigSO : ScriptableObject {
    public String Id;
    
    public String Name;
    public Vector2 Position;
    public int Cost;
    public bool IsLearnedAtTheBeginning;
    public List<SkillConfigSO> Requirements;

    private void OnValidate() {
        if (Id == "")
            GenerateNewId();
    }

    public void GenerateNewId() {
        Id = Guid.NewGuid().ToString();
    }
}