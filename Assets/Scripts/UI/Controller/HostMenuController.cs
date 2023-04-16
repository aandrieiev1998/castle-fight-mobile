using System;
using UI.View;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

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

        private void OnDestroy()
        {
            _hostMenuView.HostButton.onClick.RemoveListener(HostButtonListener);
            _hostMenuView.BackButton.onClick.RemoveListener(BackButtonListener);
        }

        private void BackButtonListener()
        {
            UIManager.Singleton.MainMenuManager.NavigateToMenu(typeof(MainMenuView));
        }

        private void HostButtonListener()
        {
            var connection = _hostMenuView.ConnectionInputField.text;
            var connectionSplit = connection.Split(':');
            var ipv4 = connectionSplit[0];
            var port = ushort.Parse(connectionSplit[1]);

            UIManager.Singleton.MainMenuManager.NavigateToMenu(typeof(LoadingMenuView));
            
            ConsoleController.Singleton.WriteLog("Starting server");
            NetworkManager.Singleton.GetComponent<UnityTransport>()
                .SetConnectionData(null, port, ipv4);
            NetworkManager.Singleton.StartHost();
        }
    }
}