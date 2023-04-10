using UI.View;
using UnityEngine;

namespace UI.Controller
{
    public class LoadingMenuController : MonoBehaviour
    {
        [SerializeField] private LoadingMenuView _loadingMenuView;

        public LoadingMenuView LoadingMenuView => _loadingMenuView;

        public void Show()
        {
            _loadingMenuView.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _loadingMenuView.gameObject.SetActive(false);
        }
        
    }
}