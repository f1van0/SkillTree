﻿using System;
using System.Collections.Generic;
using UnityEngine;


namespace StaticData {
    
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
        [field:SerializeField] public string Id { get; private set; }
    
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public Vector2 Position { get; private set; }
        [field:SerializeField] public int Cost { get; private set; }
        [field:SerializeField] public bool IsBaseSkill { get; private set; }
        [field:SerializeField] public List<SkillConfigSO> Requirements { get; private set; }

        private void OnValidate() {
            if (Id == "")
                GenerateNewId();
        }

        public void GenerateNewId() {
            Id = Guid.NewGuid().ToString();
        }
    }
}