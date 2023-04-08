using UI.View;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEditor;
using UnityEngine;

namespace UI.Controller
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private LobbyMenuView _lobbyMenuView;

        private void OnEnable()
        {
            _mainMenuView.HostButton.onClick.AddListener(HostButtonListener);
            _mainMenuView.JoinButton.onClick.AddListener(JoinButtonListener);
            _mainMenuView.ExitButton.onClick.AddListener(ExitButtonListener);
        }

        private void OnDisable()
        {
            _mainMenuView.HostButton.onClick.RemoveListener(HostButtonListener);
            _mainMenuView.JoinButton.onClick.RemoveListener(JoinButtonListener);
            _mainMenuView.ExitButton.onClick.RemoveListener(ExitButtonListener);
        }

        private void HostButtonListener()
        {
            var username = _mainMenuView.UsernameField.text;
            if (string.IsNullOrWhiteSpace(username))
            {
                ShowNotification("Error occured", "Username cannot be empty");
                return;
            }

            _mainMenuView.gameObject.SetActive(false);
            _lobbyMenuView.gameObject.SetActive(true);

            NetworkManager.Singleton.GetComponent<UnityTransport>()
                .SetConnectionData("178.43.241.246", 25565, "0.0.0.0");
            NetworkManager.Singleton.StartHost();

            // NetworkManager.Singleton.SceneManager.LoadScene("OnlineScene", LoadSceneMode.Single);
        }

        private void JoinButtonListener()
        {
            var username = _mainMenuView.UsernameField.text;
            if (string.IsNullOrWhiteSpace(username))
            {
                ShowNotification("Error occured", "Username cannot be empty");
                return;
            }

            NetworkManager.Singleton.StartClient();
        }

        private void ExitButtonListener()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void ShowNotification(string title, string description)
        {
            _mainMenuView.NotificationManager.title = title;
            _mainMenuView.NotificationManager.description = description;
            _mainMenuView.NotificationManager.UpdateUI();
            _mainMenuView.NotificationManager.OpenNotification();
        }
    }
}