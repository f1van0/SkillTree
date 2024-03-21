using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillPresenter {
    public event Action Changed;
    public event Action<string> Selected;
    
    public string Name
        => _skillData.Name;
    
    public string Id
        => _skillData.Id;
    
    public int Cost
        => _skillData.Cost;
    
    public Vector2 Position
        => _skillData.Position;
    
    public bool IsLearned
        => _skillData.IsLearned;
    
    public bool IsBaseSkill
        => _skillData.IsBaseSkill;
    
    private SkillData _skillData;
    
    public SkillPresenter(SkillData skillData) {
        _skillData = skillData;
        _skillData.Changed += HandleSkillChanged;
    }

    public void Select() {
        Selected?.Invoke(Id);
    }

    private void HandleSkillChanged() {
        Changed?.Invoke();
    }

    public List<(string fromId, string toId)> GetConnections() {
        List<(string fromId, string toId)> connections = new List<(string fromId, string toId)>();

        foreach (var connection in _skillData.Connections) {
            connections.Add((_skillData.Id, connection.Id));
        }

        return connections;
    }
}