using System;
using UI.Controller;
using UI.View;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private MainMenuController _mainMenuController;
        [SerializeField] private LobbyMenuController _lobbyMenuController;
        [SerializeField] private HostMenuController _hostMenuController;
        [SerializeField] private JoinMenuController _joinMenuController;
        [SerializeField] private LoadingMenuController _loadingMenuController;

        public static UIManager Singleton { get; private set; }

        private void Awake()
        {
            if (Singleton != null)
                throw new Exception($"Detected more than 1 instance of {nameof(UIManager)}");

            Singleton = this;
        }

        private void Start()
        {
            NavigateToMenu(typeof(MainMenuView));
        }

        public void NavigateToMenu(Type menuType)
        {
            ConsoleController.Singleton.WriteLog($"Navigating to menu {menuType.Name}");
            _mainMenuController.MainMenuView.gameObject.SetActive(menuType == typeof(MainMenuView));
            _lobbyMenuController.LobbyMenuView.gameObject.SetActive(menuType == typeof(LobbyMenuView));
            _hostMenuController.HostMenuView.gameObject.SetActive(menuType == typeof(HostMenuView));
            _joinMenuController.JoinMenuView.gameObject.SetActive(menuType == typeof(JoinMenuView));
            _loadingMenuController.LoadingMenuView.gameObject.SetActive(menuType == typeof(LoadingMenuView));
        }
    }
}