using System;

public class SkillOptionsPresenter {
    public event Action SelectedSkillChanged;
    public event Action<int> ChangedAmountOfAvailableSkillPoints;

    private SkillTreeData _skillTreeData;
    private SkillPointsData _skillPointsData;
    private SkillPresenter _selectedSkillPresenter;

    public SkillOptionsPresenter(SkillTreeData skillTreeData, SkillPointsData skillPointsData) {
        _skillTreeData = skillTreeData;
        _skillPointsData = skillPointsData;
        
        _skillTreeData.SelectedSkillChanged += HandleSelectedSkillChanged;
        _skillPointsData.ChangedAmountOfSkillPoints += HandleChangedAmountOfAvailableSkillPoints;
    }

    private void HandleSelectedSkillChanged() {
        _selectedSkillPresenter = new SkillPresenter(_skillTreeData.GetSelectedSkill());
        SelectedSkillChanged?.Invoke();
    }

    private void HandleChangedAmountOfAvailableSkillPoints(int amount) {
        ChangedAmountOfAvailableSkillPoints?.Invoke(amount);
    }

    public int GetAmountOfAvailableSkillPoints() {
        return _skillPointsData.AmountOfSkillPoints;
    }

    public void EarnSkillPoint() {
        _skillPointsData.AddSkillPoints(1);
    }
    
    public bool CanLearnSelectedSkill() {
        return _skillTreeData.CanLearnSelectedSkill(_skillPointsData.AmountOfSkillPoints);
    }

    public bool CanForgetSelectedSkill() {
        return _skillTreeData.CanForgetSelectedSkill();
    }

    public void LearnSelectedSkill() {
        _skillTreeData.LearnSelectedSkill(out var cost);
        _skillPointsData.AddSkillPoints(-cost);
    }

    public void ForgetSelectedSkill() {
        _skillTreeData.ForgetSelectedSkill(out var cost);
        _skillPointsData.AddSkillPoints(cost);
    }
    
    public void ForgetAllSkills() {
        _skillTreeData.ForgetAllSkills(out var cost);
        _skillPointsData.AddSkillPoints(cost);
    }

    public SkillPresenter GetPresenterForSelectedSkill() {
        return _selectedSkillPresenter;
    }
}