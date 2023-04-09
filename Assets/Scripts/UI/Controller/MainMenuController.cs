using UI.View;
using Unity.Netcode;
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

            UIManager.Singleton.NavigateToMenu(typeof(HostMenuView));
        }

        private void JoinButtonListener()
        {
            var username = _mainMenuView.UsernameField.text;
            if (string.IsNullOrWhiteSpace(username))
            {
                ShowNotification("Error occured", "Username cannot be empty");
                return;
            }
            
            UIManager.Singleton.NavigateToMenu(typeof(JoinMenuView));
        }

        private void ExitButtonListener()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
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