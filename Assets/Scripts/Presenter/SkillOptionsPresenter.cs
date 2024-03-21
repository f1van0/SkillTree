using System;
using Model;

public class SkillOptionsPresenter {
    public event Action SelectedSkillChanged;
    public event Action<int> ChangedAmountOfAvailableSkillPoints;

    public SkillPresenter SelectedSkill
        => _selectedSkillPresenter;
    
    public int AmountOfAvailableSkillPoints
        => _skillTreeProgressionData.AmountOfAvailableSkillPoints;

    private SkillTreeProgressionData _skillTreeProgressionData;
    private SkillPresenter _selectedSkillPresenter;

    public SkillOptionsPresenter(SkillTreeProgressionData skillTreeProgressionData) {
        _skillTreeProgressionData = skillTreeProgressionData;
        
        skillTreeProgressionData.SelectedSkillChanged += HandleSelectedSkillChanged;
        skillTreeProgressionData.ChangedAmountOfAvailableSkillPoints += HandleChangedAmountOfAvailableSkillPoints;
    }

    private void HandleSelectedSkillChanged() {
        _selectedSkillPresenter = new SkillPresenter(_skillTreeProgressionData.SelectedSkill);
        SelectedSkillChanged?.Invoke();
    }

    private void HandleChangedAmountOfAvailableSkillPoints(int amount) {
        ChangedAmountOfAvailableSkillPoints?.Invoke(amount);
    }

    public void EarnSkillPoint() {
        _skillTreeProgressionData.EarnSkillPoint();
    }
    
    public bool CanLearnSelectedSkill() {
        return _skillTreeProgressionData.CanLearnSelectedSkill();
    }

    public bool CanForgetSelectedSkill() {
        return _skillTreeProgressionData.CanForgetSelectedSkill();
    }

    public void LearnSelectedSkill() {
        _skillTreeProgressionData.LearnSelectedSkill();
    }

    public void ForgetSelectedSkill() {
        _skillTreeProgressionData.ForgetSelectedSkill();
    }
    
    public void ForgetAllSkills() {
        _skillTreeProgressionData.ForgetAllSkills();
    }
}