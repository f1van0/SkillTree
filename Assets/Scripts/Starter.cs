using System;
using UnityEngine;

public class Starter : MonoBehaviour {
    [SerializeField] private SkillsContainerSO _skillsContainerSO;
    [SerializeField] private SkillTreeView _skillTreeViewPrefab;

    private SkillTreeView _skillTreeView;
    
    private void Start() {
        var skillTreeData = new SkillTreeData(_skillsContainerSO);
        var skillTreePresenter = new SkillTreePresenter(skillTreeData);
        _skillTreeView = Instantiate(_skillTreeViewPrefab);
        _skillTreeView.Initialize(skillTreePresenter);
    }
}