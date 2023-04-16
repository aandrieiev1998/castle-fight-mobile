using System;
using UI.Controller;
using UI.View;
using UnityEngine;

namespace UI.Manager
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private MainMenuController _mainMenuController;
        [SerializeField] private LobbyMenuController _lobbyMenuController;
        [SerializeField] private HostMenuController _hostMenuController;
        [SerializeField] private JoinMenuController _joinMenuController;
        [SerializeField] private LoadingMenuController _loadingMenuController;
        
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