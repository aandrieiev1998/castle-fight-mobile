using UI.View;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Controller
{
    public class HostMenuController : MonoBehaviour
    {
        [SerializeField] private HostMenuView _hostMenuView;

        public HostMenuView HostMenuView => _hostMenuView;

        private void Awake()
        {
            _hostMenuView.HostButton.onClick.AddListener(HostButtonListener);
            _hostMenuView.BackButton.onClick.AddListener(BackButtonListener);
        }

        private void BackButtonListener()
        {
            UIManager.Singleton.NavigateToMenu(typeof(MainMenuView));
        }

        private void HostButtonListener()
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>()
                .SetConnectionData("178.43.241.246", 25565, "0.0.0.0");
            NetworkManager.Singleton.StartHost();

            NetworkManager.Singleton.SceneManager.LoadScene("OnlineScene", LoadSceneMode.Single);
        }
    }
}