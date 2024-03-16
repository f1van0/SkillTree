using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour {
    public event Action<Guid> Selected;
    
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    [SerializeField] private Color DefaultColor;
    [SerializeField] private Color LearnedColor;

    private Guid _id;

    public void Initialize(Guid id, string name, bool isLearned) {
        _id = id;
        _label.text = name;
        SetLearnedState(isLearned);
        
        _button.onClick.AddListener(HandleClick);
    }

    public void SetLearnedState(bool isLearned) {
        _image.color = isLearned ? LearnedColor : DefaultColor;
    }

    public void HandleClick() {
        Selected?.Invoke(_id);
    }
}