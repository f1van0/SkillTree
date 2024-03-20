using UnityEngine;
using UnityEngine.Serialization;

public class SkillTreeScreenView : MonoBehaviour {
    [SerializeField] private GameObject _root;
    [SerializeField] private SkillTreeFieldView _skillTreeFieldView;
    [FormerlySerializedAs("_skillPanelView")] [SerializeField] private SkillTreePanelView skillTreePanelView;

    private SkillTreeScreenPresenter _presenter;
    
    public void Initialize(SkillTreeScreenPresenter presenter) {
        _presenter = presenter;
        
        _skillTreeFieldView.Initialize(_presenter.SkillTreePresenter);
        skillTreePanelView.Initialize(_presenter.SkillOptionsPresenter);
    }
    
    public void Show() {
        _root.SetActive(true);
    }

    public void Hide() {
        _root.SetActive(false);
    }
}