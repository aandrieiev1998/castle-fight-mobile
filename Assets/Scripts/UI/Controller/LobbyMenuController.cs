using System.Collections.Generic;
using Network;
using UI.View;
using Unity.Netcode;
using UnityEngine;

namespace UI.Controller
{
    public class LobbyMenuController : MonoBehaviour
    {
        [SerializeField] private LobbyMenuView _lobbyMenuView;
        [SerializeField] private LobbyListItemView _lobbyListItemPrefab;

        private NetworkConnectionsManager networkConnectionsManager;
        private readonly Dictionary<ulong, LobbyListItemView> lobbyList = new();

        private void Awake()
        {
            networkConnectionsManager = NetworkConnectionsManager.Singleton;
            networkConnectionsManager.OnClientConnection += OnClientConnection;
        }

        private void OnClientConnection(ulong clientId, ConnectionStatus clientStatus)
        {
            if (clientStatus == ConnectionStatus.Connected)
            {
                var listItem = Instantiate(_lobbyListItemPrefab, Vector3.zero, Quaternion.identity, _lobbyMenuView.ListRoot);
                listItem.PlayerNameText.SetText($"Player#{clientId}");
                
                lobbyList.Add(clientId, listItem);
            }
            else
            {
                Destroy(lobbyList[clientId].gameObject);
                lobbyList.Remove(clientId);
            }
        }
    }
}