using Presenter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View {
    public class SkillView : MonoBehaviour {
        public string Id { get; private set; }

        [SerializeField] private TMP_Text _label;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _learnedColor;
    
        private SkillPresenter _presenter;

        public void Initialize(SkillPresenter presenter) {
            _presenter = presenter;
            SetupValues();
        }

        private void OnEnable() {
            _presenter.Changed += SetupValues;
            _button.onClick.AddListener(HandleClick);
        }

        private void OnDisable() {
            _presenter.Changed -= SetupValues;
            _button.onClick.RemoveListener(HandleClick);
        }

        public void SetupValues() {
            Id = _presenter.Id;
            gameObject.name = "Skill_"+_presenter.Name;
            transform.localPosition = _presenter.Position;
            _label.text = _presenter.Name;
            _image.color = _presenter.IsLearned? _learnedColor : _defaultColor;
        }

        public void HandleClick() {
            _presenter.Select();
        }
    }
}