using System;
using UnityEngine;

public class Starter : MonoBehaviour {
    [SerializeField] private SkillsContainerSO _skillsContainerSO;
    [SerializeField] private SkillTreeScreenView _skillTreeScreenPrefab;

    private SkillTreeScreenView _skillTreeScreen;
    
    private void Start() {
        var skillTreeData = new SkillTreeData(_skillsContainerSO);
        var skillPointsData = new SkillPointsData();
        var skillTreePresenter = new SkillTreeScreenPresenter(skillTreeData,skillPointsData);
        
        _skillTreeScreen = Instantiate(_skillTreeScreenPrefab);
        _skillTreeScreen.Initialize(skillTreePresenter);
        _skillTreeScreen.Show();
    }
}