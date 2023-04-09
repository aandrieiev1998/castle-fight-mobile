using UI.View;
using Unity.Netcode;
using UnityEngine;

namespace UI.Controller
{
    public class JoinMenuController : MonoBehaviour
    {
        [SerializeField] private JoinMenuView _joinMenuView;

        public JoinMenuView JoinMenuView => _joinMenuView;
        
        private void Awake()
        {
            _joinMenuView.JoinButton.onClick.AddListener(JoinButtonListener);
            _joinMenuView.BackButton.onClick.AddListener(BackButtonListener);
        }

        private void BackButtonListener()
        {
            UIManager.Singleton.NavigateToMenu(typeof(MainMenuView));
        }

        private void JoinButtonListener()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}