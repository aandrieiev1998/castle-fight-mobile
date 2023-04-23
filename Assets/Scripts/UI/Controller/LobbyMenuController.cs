using System.Collections;
using System.Collections.Generic;
using Network;
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
        private ConnectionsManager connectionsManager;

        private NetworkManager networkManager;

        public LobbyMenuView LobbyMenuView => _lobbyMenuView;

        private void Awake()
        {
            _lobbyMenuView.StartButton.onClick.AddListener(StartButtonListener);
        }

        private void Start()
        {
            networkManager = NetworkManager.Singleton;
            networkManager.OnServerStarted += OnServerStarted;
            networkManager.OnClientConnectedCallback += OnClientConnected;
            networkManager.OnClientDisconnectCallback += OnClientDisconnected;

            connectionsManager = ConnectionsManager.Singleton;
            connectionsManager.ClientsListUpdated += OnClientsListUpdated;
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
                AddPlayerToList(clientId);
            }
            else if (!networkManager.IsHost && networkManager.IsClient && clientId == networkManager.LocalClientId)
            {
                UIManager.Singleton.MainMenuManager.NavigateToMenu(typeof(LobbyMenuView));
                _lobbyMenuView.LabelText.SetText("Lobby, you are client");
                ConsoleController.Singleton.WriteLog($"IsConnectedClient = {networkManager.IsConnectedClient}");
                // todo list loading indicator
            }
        }
        
        
        private void OnClientsListUpdated()
        {
            ConsoleController.Singleton.WriteLog($"Connections = {ConnectionsManager.Singleton.Connections.Count}");
            foreach (var @ulong in ConnectionsManager.Singleton.Connections) AddPlayerToList(@ulong);
        }

        private void OnClientDisconnected(ulong clientId)
        {
            Destroy(lobbyList[clientId].gameObject);
            lobbyList.Remove(clientId);
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