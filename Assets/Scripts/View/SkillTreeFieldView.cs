using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SkillTreeFieldView : MonoBehaviour {
    [SerializeField] private SkillView _skillPrefab;
    [SerializeField] private ConnectionView _connectionPrefab;

    [SerializeField] private Transform _skillsContainer;
    [SerializeField] private Transform _connectionsContainer;

    private SkillTreePresenter _presenter;
    private List<SkillView> _skills;
    private List<ConnectionView> _connections;

    public void Initialize(SkillTreePresenter skillTreePresenter) {
        _presenter = skillTreePresenter;
        _skills = new List<SkillView>();
        _connections = new List<ConnectionView>();

        foreach (var skillPresenter in _presenter.GetSkillPresenters()) {
            var newSkillView = Instantiate(_skillPrefab, _skillsContainer);
            newSkillView.transform.localPosition = skillPresenter.GetPosition();
            newSkillView.Initialize(skillPresenter);

            newSkillView.Selected += SelectSkill;

            _skills.Add(newSkillView);
        }

        foreach (var connection in _presenter.GetConnectionBetweenSkills()) {
            var newTransitionView = Instantiate(_connectionPrefab, _connectionsContainer);
            var fromSkill = _skills.Find(x => x.Id == connection.fromId);
            var toSkill = _skills.Find(x => x.Id == connection.toId);
            newTransitionView.Setup(fromSkill, toSkill);
            _connections.Add(newTransitionView);
        }
    }

    private void SelectSkill(String id) {
        _presenter.Select(id);
    }

    private void OnDestroy() {
        foreach (var skillView in _skills) {
            skillView.Selected -= SelectSkill;
            Destroy(skillView);
        }

        foreach (var connection in _connections) {
            Destroy(connection);
        }
    }
}