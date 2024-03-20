using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillData {
    public event Action Changed;
    
    public String Id;
    public String Name;
    public Vector2 Position;
    public int Cost;
    public bool IsLearnedAtTheBeginning;
    public List<SkillData> Connections;

    public bool IsLearned
    {
        get
        {
            return _isLearned;
        }
        set
        {
            _isLearned = value;
            Changed?.Invoke();
        }
    }

    private bool _isLearned;

    public SkillData(SkillConfigSO config) {
        Id = config.Id;
        Name = config.Name;
        Position = config.Position;
        Cost = config.Cost;
        Connections = new List<SkillData>();
        IsLearnedAtTheBeginning = config.IsLearnedAtTheBeginning;
        IsLearned = config.IsLearnedAtTheBeginning;
    }

    public bool HasConnectionWithSkill(SkillData skill) {
        return Connections.Contains(skill);
    }

    public void AddConnection(SkillData skill) {
        if (Connections.Contains(skill))
            throw new Exception("You're trying to add a connection to a skill, but this connection is already exists!");

        Connections.Add(skill);
    }
}