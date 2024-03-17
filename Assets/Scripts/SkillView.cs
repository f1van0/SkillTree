using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour {
    public event Action<String> Selected;

    public String Id { get; private set; }

    [SerializeField] private TMP_Text _label;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    [SerializeField] private Color DefaultColor;
    [SerializeField] private Color LearnedColor;

    public void Initialize(String id, string name, bool isLearned) {
        Id = id;
        _label.text = name;
        SetLearnedState(isLearned);
        
        _button.onClick.AddListener(HandleClick);
    }

    public void SetLearnedState(bool isLearned) {
        _image.color = isLearned ? LearnedColor : DefaultColor;
    }

    public void HandleClick() {
        Selected?.Invoke(Id);
    }
}