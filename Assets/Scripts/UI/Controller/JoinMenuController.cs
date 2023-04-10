using UI.View;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
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
            var connection = _joinMenuView.ConnectionInputField.text;
            var connectionSplit = connection.Split(':');
            var ipv4 = connectionSplit[0];
            var port = ushort.Parse(connectionSplit[1]);

            NetworkManager.Singleton.GetComponent<UnityTransport>()
                .SetConnectionData(ipv4, port, null);
            NetworkManager.Singleton.StartClient();

            UIManager.Singleton.NavigateToMenu(typeof(LoadingMenuView));
        }
    }
}