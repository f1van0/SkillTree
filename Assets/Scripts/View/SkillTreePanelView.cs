using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreePanelView : MonoBehaviour {
    [SerializeField] private TMP_Text _availableSkillPointsLabel;
    [SerializeField] private TMP_Text _debugSelectedSkillLabel;
    [SerializeField] private Button _earnSkillPointButton;
    [SerializeField] private Button _learnButton;
    [SerializeField] private Button _forgetButton;
    [SerializeField] private Button _forgetAllButton;

    private SkillOptionsPresenter _presenter;
    
    public void Initialize(SkillOptionsPresenter presenter) {
        _presenter = presenter;
        
        _presenter.SelectedSkillChanged += HandleSelectedSkillChanged;
        _presenter.ChangedAmountOfAvailableSkillPoints += HandleAmountOfAvailableSkillPointsChanged;
        
        _earnSkillPointButton.onClick.AddListener(EarnSkillPoint);
        _learnButton.onClick.AddListener(LearnSkill);
        _forgetButton.onClick.AddListener(ForgetSkill);
        _forgetAllButton.onClick.AddListener(ForgetAllSkills);
        
        ChangeAmountOfAvailableSkillPoints(_presenter.GetAmountOfAvailableSkillPoints());
        UpdateDebugInfoAboutSelectedSkill();
        SetLearnButtonInteractivity(_presenter.CanLearnSelectedSkill());
        SetForgetButtonInteractivity(_presenter.CanForgetSelectedSkill());
        SetForgetAllButtonInteractivity(true);
    }
    
    private void EarnSkillPoint() {
        _presenter.EarnSkillPoint();
    }

    private void LearnSkill() {
        _presenter.LearnSelectedSkill();
    }

    private void ForgetSkill() {
        _presenter.ForgetSelectedSkill();
    }

    private void ForgetAllSkills() {
        _presenter.ForgetAllSkills();
    }

    private void HandleSelectedSkillChanged() {
        SetLearnButtonInteractivity(_presenter.CanLearnSelectedSkill());
        SetForgetButtonInteractivity(_presenter.CanForgetSelectedSkill());
        UpdateDebugInfoAboutSelectedSkill();
    }

    private void HandleAmountOfAvailableSkillPointsChanged(int amount) {
        SetLearnButtonInteractivity(_presenter.CanLearnSelectedSkill());
        ChangeAmountOfAvailableSkillPoints(amount);
    }
    
    private void ChangeAmountOfAvailableSkillPoints(int amount) {
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
    
    private void UpdateDebugInfoAboutSelectedSkill() {
        var selectedSkillPresenter = _presenter.GetPresenterForSelectedSkill();

        if (selectedSkillPresenter == null) {
            _debugSelectedSkillLabel.text = "Skill is not selected";
            return;
        }
        
        _debugSelectedSkillLabel.text =
            new StringBuilder().Append("Selected skill\n")
                .Append("Name: ").Append(selectedSkillPresenter.GetName())
                .Append("\nId: ").Append(selectedSkillPresenter.GetId())
                .Append("\nCost: ").Append(selectedSkillPresenter.GetCost())
                .Append("\nIsLearned: ").Append(selectedSkillPresenter.GetLearnState())
                .Append("\nIsLearnedFromBeginning: ").Append(selectedSkillPresenter.GetLearnFromBeginningState())
                .ToString();
    }

    private void OnDestroy() {
        _presenter.SelectedSkillChanged -= HandleSelectedSkillChanged;
        _presenter.ChangedAmountOfAvailableSkillPoints -= ChangeAmountOfAvailableSkillPoints;
    }
}