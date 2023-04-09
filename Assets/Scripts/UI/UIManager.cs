using System;
using UI.Controller;
using UI.View;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        // [SerializeField] private MainMenuController _mainMenuController;
        // [SerializeField] private LobbyMenuController _lobbyMenuController;
        // [SerializeField] private HostMenuController _hostMenuController;
        // [SerializeField] private JoinMenuController _joinMenuController;

        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private LobbyMenuView _lobbyMenuView;
        [SerializeField] private HostMenuView _hostMenuView;
        [SerializeField] private JoinMenuView _joinMenuView;

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
            Debug.Log($"Navigating to menu {menuType}");
            _mainMenuView.gameObject.SetActive(menuType == typeof(MainMenuView));
            _lobbyMenuView.gameObject.SetActive(menuType == typeof(LobbyMenuView));
            _hostMenuView.gameObject.SetActive(menuType == typeof(HostMenuView));
            _joinMenuView.gameObject.SetActive(menuType == typeof(JoinMenuView));
        }
    }
}