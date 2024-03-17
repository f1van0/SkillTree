using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SkillTreeView : MonoBehaviour {
    [SerializeField] private SkillView _skillViewPrefab;

    [SerializeField] private Transform _container;
    [SerializeField] private Button _earnSkillPointButton;
    [SerializeField] private Button _learnButton;
    [SerializeField] private Button _forgetButton;
    [SerializeField] private Button _forgetAllButton;
    [SerializeField] private TMP_Text _availableSkillPointsLabel;
    [SerializeField] private TMP_Text _debugSelectedSkillLabel;

    private SkillTreePresenter _presenter;
    private List<SkillView> _skills;

    public void Initialize(SkillTreePresenter skillTreePresenter) {
        _presenter = skillTreePresenter;
        _skills = new List<SkillView>();

        foreach (var skill in _presenter.GetSkills()) {
            var newSkillView = Instantiate(_skillViewPrefab, _container);
            newSkillView.transform.localPosition = skill.Position;
            newSkillView.Initialize(skill.Id, skill.Name, skill.IsLearned);

            newSkillView.Selected += SelectSkill;
            skill.LearnedStateChanged += newSkillView.SetLearnedState;

            _skills.Add(newSkillView);
        }

        _earnSkillPointButton.onClick.AddListener(EarnSkillPoint);
        _learnButton.onClick.AddListener(LearnSkill);
        _forgetButton.onClick.AddListener(ForgetSkill);
        _forgetAllButton.onClick.AddListener(ForgetAllSkills);

        SetAmountOfAvailableSkillPoints(_presenter.GetAmountOfAvailableSkillPoints());
        SetLearnButtonInteractivity(_presenter.CanLearnSelectedSkill());
        SetForgetButtonInteractivity(_presenter.CanForgetSelectedSkill());
        SetForgetAllButtonInteractivity(true);
    }

    private void EarnSkillPoint() {
        _presenter.EarnSkillPoint();
        SetAmountOfAvailableSkillPoints(_presenter.GetAmountOfAvailableSkillPoints());
        SetLearnButtonInteractivity(_presenter.CanLearnSelectedSkill());
    }

    private void SelectSkill(String id) {
        _presenter.Select(id);
        SetLearnButtonInteractivity(_presenter.CanLearnSelectedSkill());
        SetForgetButtonInteractivity(_presenter.CanForgetSelectedSkill());
        UpdateDebugInfoAboutSelectedSkill(_presenter.GetSelectedSkill());
    }

    private void UpdateDebugInfoAboutSelectedSkill(SkillData selectedSkill) {
        _debugSelectedSkillLabel.text = $"Selected skill\n" +
                                        $"Name: {selectedSkill.Name}\n" +
                                        $"Id: {selectedSkill.Id}\n" +
                                        $"Cost: {selectedSkill.Cost}\n" +
                                        $"IsLearned: {selectedSkill.IsLearned}\n" +
                                        $"IsLearnedFromBeginning: {selectedSkill.IsLearnedAtTheBeginning}";
    }

    private void LearnSkill() {
        _presenter.LearnSelectedSkill();
        SetLearnButtonInteractivity(_presenter.CanLearnSelectedSkill());
        SetForgetButtonInteractivity(_presenter.CanForgetSelectedSkill());
        SetAmountOfAvailableSkillPoints(_presenter.GetAmountOfAvailableSkillPoints());
        UpdateDebugInfoAboutSelectedSkill(_presenter.GetSelectedSkill());
    }

    private void ForgetSkill() {
        _presenter.ForgetSelectedSkill();
        SetLearnButtonInteractivity(_presenter.CanLearnSelectedSkill());
        SetForgetButtonInteractivity(_presenter.CanForgetSelectedSkill());
        SetAmountOfAvailableSkillPoints(_presenter.GetAmountOfAvailableSkillPoints());
        UpdateDebugInfoAboutSelectedSkill(_presenter.GetSelectedSkill());
    }

    private void ForgetAllSkills() {
        _presenter.ForgetAllSkills();
        SetLearnButtonInteractivity(_presenter.CanLearnSelectedSkill());
        SetForgetButtonInteractivity(_presenter.CanForgetSelectedSkill());
        SetAmountOfAvailableSkillPoints(_presenter.GetAmountOfAvailableSkillPoints());
        UpdateDebugInfoAboutSelectedSkill(_presenter.GetSelectedSkill());
    }

    private void SetAmountOfAvailableSkillPoints(int amount) {
        _availableSkillPointsLabel.text = $"Skill points: {amount}";
    }

    public void SetLearnButtonInteractivity(bool status) {
        _learnButton.interactable = status;
    }

    public void SetForgetButtonInteractivity(bool status) {
        _forgetButton.interactable = status;
    }

    public void SetForgetAllButtonInteractivity(bool status) {
        _forgetAllButton.interactable = status;
    }

    private void OnDestroy() {
        foreach (var skillView in _skills) {
            skillView.Selected -= SelectSkill;
            _presenter.GetSkillById(skillView.Id).LearnedStateChanged -= skillView.SetLearnedState;
        }
    }
}