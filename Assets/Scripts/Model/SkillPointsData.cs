using System;

public class SkillPointsData
{
    public event Action<int> ChangedAmountOfSkillPoints;

    public int AmountOfSkillPoints
    {
        get
        {
            return _amountOfSkillPoints; 
        }
        private set
        {
            if (_amountOfSkillPoints != value) {
                _amountOfSkillPoints = value;
                ChangedAmountOfSkillPoints?.Invoke(_amountOfSkillPoints);
            }
        }
    }

    private int _amountOfSkillPoints;

    public SkillPointsData() {
        AmountOfSkillPoints = 0;
    }

    public void AddSkillPoints(int amount) {
        AmountOfSkillPoints += amount;
    }
}