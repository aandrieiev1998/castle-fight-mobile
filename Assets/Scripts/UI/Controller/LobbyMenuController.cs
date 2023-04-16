using System.Collections;
using System.Collections.Generic;
using UI.View;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            networkManager.OnClientConnectedCallback += OnClientConnected;
            networkManager.OnClientDisconnectCallback += OnClientDisconnected;

            _lobbyMenuView.StartButton.onClick.AddListener(StartButtonListener);
        }

        private void OnDestroy()
        {
            networkManager.OnServerStarted -= OnServerStarted;
            networkManager.OnClientConnectedCallback -= OnClientConnected;
            networkManager.OnClientDisconnectCallback -= OnClientDisconnected;
            _lobbyMenuView.StartButton.onClick.RemoveListener(StartButtonListener);
        }

        private void StartButtonListener()
        {
            UIManager.Singleton.MainMenuManager.NavigateToMenu(typeof(LoadingMenuView));
            
            networkManager.SceneManager.LoadScene("OnlineScene", LoadSceneMode.Single);
            
        }

        private void OnServerStarted()
        {
            ConsoleController.Singleton.WriteLog("Server started");
            // StartCoroutine(LongLoading());
            UIManager.Singleton.MainMenuManager.NavigateToMenu(typeof(LobbyMenuView));
            AddPlayerToList(networkManager.LocalClientId);
            _lobbyMenuView.LabelText.SetText("Lobby, you are server");
        }

        private IEnumerator LongLoading()
        {
            yield return new WaitForSeconds(2.0f);

            UIManager.Singleton.MainMenuManager.NavigateToMenu(typeof(LobbyMenuView));
        }

        private void OnClientConnected(ulong clientId)
        {
            ConsoleController.Singleton.WriteLog($"Client connected {clientId}");

            if (networkManager.IsHost && clientId != networkManager.LocalClientId)
            {
                var connectedClientsIds = networkManager.ConnectedClientsIds;
                ConsoleController.Singleton.WriteLog(
                    $"Sending connectedClientsIds = {string.Join(",", connectedClientsIds)} to all clients"); // todo sent only to newly connected player
                SendConnectedPlayersToClient(connectedClientsIds);
                AddPlayerToList(clientId);
            }
            else if (networkManager.IsClient && clientId == networkManager.LocalClientId)
            {
                UIManager.Singleton.MainMenuManager.NavigateToMenu(typeof(LobbyMenuView));
                _lobbyMenuView.LabelText.SetText("Lobby, you are client");
                ConsoleController.Singleton.WriteLog($"IsConnectedClient = {networkManager.IsConnectedClient}");
            }
        }

        private void OnClientDisconnected(ulong clientId)
        {
            Destroy(lobbyList[clientId].gameObject);
            lobbyList.Remove(clientId);
        }


        [ClientRpc]
        private void SendConnectedPlayersToClient(IReadOnlyList<ulong> connectedClientsIds)
        {
            ConsoleController.Singleton.WriteLog(
                $"Received connectedClientsIds: {string.Join(",", connectedClientsIds)}, host = {networkManager.IsHost}, client = {networkManager.IsClient}");
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