using System;
using Model;
using UnityEngine;

public class Starter : MonoBehaviour {
    [SerializeField] private SkillsContainerSO _skillsContainerSO;
    [SerializeField] private SkillTreeScreenView _skillTreeScreenPrefab;

    private SkillTreeScreenView _skillTreeScreen;
    
    private void Awake() {
        var skillTreeData = new SkillTreeData(_skillsContainerSO);
        var skillPointsData = new SkillPointsData();
        var skillTreeProgressionData = new SkillTreeProgressionData(skillTreeData, skillPointsData);
        var skillTreePresenter = new SkillTreeScreenPresenter(skillTreeProgressionData, skillTreeData, skillPointsData);
        
        _skillTreeScreen = Instantiate(_skillTreeScreenPrefab);
        _skillTreeScreen.Initialize(skillTreePresenter);
        _skillTreeScreen.Show();
    }
}