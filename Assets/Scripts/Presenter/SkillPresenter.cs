using System;
using UnityEngine;

public class SkillPresenter {
    public event Action Changed;
    public event Action Selected;
    
    private SkillData _data;
    
    public SkillPresenter(SkillData data) {
        _data = data;
        _data.Changed += HandleSkillChanged;
        _data.Changed += HandleSkillSelected;
    }

    private void HandleSkillChanged() {
        Changed?.Invoke();
    }
    
    private void HandleSkillSelected() {
        Selected?.Invoke();
    }

    public string GetName()
        => _data.Name;
    
    public string GetId()    
        => _data.Id;
    
    public int GetCost()
        => _data.Cost;
    
    public Vector2 GetPosition()
        => _data.Position;
    
    public bool GetLearnState()    
        => _data.IsLearned;
    
    public bool GetLearnFromBeginningState()
        => _data.IsLearnedAtTheBeginning;
}