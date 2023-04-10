using System.Collections;
using System.Collections.Generic;
using UI.View;
using Unity.Netcode;
using UnityEngine;

namespace UI.Controller
{
    public class LobbyMenuController : MonoBehaviour
    {
        [SerializeField] private LobbyMenuView _lobbyMenuView;

        [SerializeField] private LobbyListItemView _lobbyListItemPrefab;

        // [SerializeField] private LoadingMenuController _loadingMenuController;
        private readonly Dictionary<ulong, LobbyListItemView> lobbyList = new();

        private NetworkManager networkManager;

        public LobbyMenuView LobbyMenuView => _lobbyMenuView;

        private void Awake()
        {
            networkManager = NetworkManager.Singleton;
            networkManager.OnServerStarted += OnServerStarted;
            networkManager.OnClientConnectedCallback += OnClientConnection;
            networkManager.OnClientDisconnectCallback += OnClientDisconnect;
            // NetworkManager.Singleton.SceneManager.LoadScene("OnlineScene", LoadSceneMode.Additive);
        }
        

        private void OnServerStarted()
        {
            ConsoleController.Singleton.WriteLog("Server started");
            // StartCoroutine(LongLoading());
            UIManager.Singleton.NavigateToMenu(typeof(LobbyMenuView));
            AddPlayerToList(networkManager.LocalClientId);
            _lobbyMenuView.LabelText.SetText("Lobby, you are server");
        }

        private IEnumerator LongLoading()
        {
            yield return new WaitForSeconds(2.0f);
            
            UIManager.Singleton.NavigateToMenu(typeof(LobbyMenuView));
        }

        private void OnClientConnection(ulong clientId)
        {
            ConsoleController.Singleton.WriteLog($"Client connected {clientId}");

            if (networkManager.IsHost && clientId != networkManager.LocalClientId)
            {
                var clientRpcParams = new ClientRpcParams
                {
                    Send = new ClientRpcSendParams
                    {
                        TargetClientIds = new []{clientId}
                    }
                };
                var connectedClientsIds = networkManager.ConnectedClientsIds;
                ConsoleController.Singleton.WriteLog($"Sending connectedClientsIds = {string.Join(",", connectedClientsIds)} to {clientId}");
                SendConnectedPlayersToClient(connectedClientsIds, clientRpcParams);
                AddPlayerToList(clientId);
            }
            else if (networkManager.IsClient && clientId == networkManager.LocalClientId)
            {
                UIManager.Singleton.NavigateToMenu(typeof(LobbyMenuView));
                _lobbyMenuView.LabelText.SetText("Lobby, you are client");
            }
        }

        private void OnClientDisconnect(ulong clientId)
        {
            Destroy(lobbyList[clientId].gameObject);
            lobbyList.Remove(clientId);
        }
        

        [ClientRpc]
        private void SendConnectedPlayersToClient(IReadOnlyList<ulong> connectedClientsIds,
            ClientRpcParams clientRpcParams = default)
        {
            ConsoleController.Singleton.WriteLog($"Received connectedClientsIds: {string.Join(",", connectedClientsIds)}, host = {networkManager.IsHost}, client = {networkManager.IsClient}" );
            if (networkManager.IsHost) return;
            
            foreach (var clientId in connectedClientsIds) AddPlayerToList(clientId);
        }

        private void AddPlayerToList(ulong clientId)
        {
            var listItem = Instantiate(_lobbyListItemPrefab, Vector3.zero, Quaternion.identity,
                _lobbyMenuView.ListRoot);
            listItem.PlayerNameText.SetText($"Player#{clientId}");

            lobbyList.Add(clientId, listItem);
        }
    }
}