using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SkillTreeView : MonoBehaviour {
    [SerializeField] private SkillView _skillViewPrefab;
    [SerializeField] private Transform _container;
    
    private SkillTreePresenter _skillTreePresenter;
    private List<SkillView> _skills;

    public void Initialize(SkillTreePresenter skillTreePresenter) {
        _skillTreePresenter = skillTreePresenter;

        foreach (var skill in _skillTreePresenter.GetSkills()) {
            var newSkillView = Instantiate(_skillViewPrefab, _container);
            newSkillView.transform.localPosition = skill.Config.Position;
        }
    }
}