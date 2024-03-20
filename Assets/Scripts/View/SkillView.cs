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

    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _learnedColor;
    
    private SkillPresenter _presenter;

    public void Initialize(SkillPresenter presenter) {
        _presenter = presenter;
        _presenter.Changed += SetUpValues;
        _button.onClick.AddListener(HandleClick);
        SetUpValues();
    }

    public void SetUpValues() {
        Id = _presenter.GetId();
        gameObject.name = "Skill_"+_presenter.GetName();
        _label.text = _presenter.GetName();
        _image.color = _presenter.GetLearnState()? _learnedColor : _defaultColor;
    }

    public void HandleClick() {
        Selected?.Invoke(Id);
    }

    private void OnDestroy() {
        _presenter.Changed -= SetUpValues;
        _button.onClick.RemoveListener(HandleClick);
    }
}