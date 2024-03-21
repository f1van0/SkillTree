using Presenter;
using UnityEngine;

namespace View {
    public class SkillTreeScreenView : MonoBehaviour {
        [SerializeField] private GameObject _root;
        [SerializeField] private SkillTreeView _skillTreeView;
        [SerializeField] private SkillTreePanelView skillTreePanelView;

        private SkillTreeScreenPresenter _presenter;
    
        public void Initialize(SkillTreeScreenPresenter presenter) {
            _presenter = presenter;
        
            _skillTreeView.Initialize(_presenter.SkillTreePresenter);
            skillTreePanelView.Initialize(_presenter.SkillOptionsPresenter);
        }
    
        public void Show() {
            _root.SetActive(true);
        }

        public void Hide() {
            _root.SetActive(false);
        }
    }
}