using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SkillTreeFieldView : MonoBehaviour {
    [SerializeField] private SkillView _skillViewPrefab;

    [SerializeField] private Transform _skillsContainer;
    [SerializeField] private Transform _transitionsContainer;

    private SkillTreePresenter _presenter;
    private List<SkillView> _skills;

    public void Initialize(SkillTreePresenter skillTreePresenter) {
        _presenter = skillTreePresenter;
        _skills = new List<SkillView>();

        foreach (var skillPresenter in _presenter.GetSkillPresenters()) {
            var newSkillView = Instantiate(_skillViewPrefab, _skillsContainer);
            newSkillView.transform.localPosition = skillPresenter.GetPosition();
            newSkillView.Initialize(skillPresenter);

            newSkillView.Selected += SelectSkill;

            _skills.Add(newSkillView);
        }
    }

    private void SelectSkill(String id) {
        _presenter.Select(id);
    }

    private void OnDestroy() {
        foreach (var skillView in _skills) {
            skillView.Selected -= SelectSkill;
        }
    }
}