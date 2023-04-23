using System;
using System.Collections.Generic;
using System.Linq;
using UI.Controller;
using Unity.Netcode;

namespace Network
{
    public class ConnectionsManager : NetworkBehaviour
    {
        public HashSet<ulong> Connections { get; } = new();
        public static ConnectionsManager Singleton { get; private set; }

        private NetworkManager networkManager;

        public event Action ClientsListUpdated;

        private void Awake()
        {
            if (Singleton != null)
                throw new Exception($"Detected more than 1 instance of {nameof(ConnectionsManager)}");

            Singleton = this;

            DontDestroyOnLoad(Singleton);
        }

        private void Start()
        {
            networkManager = NetworkManager.Singleton;
            networkManager.OnClientConnectedCallback += OnClientConnectedCallback;
        }

        private void OnClientConnectedCallback(ulong clientId)
        {
            if (!IsHost) return;

            var clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[]{clientId}
                }
            };

            var connectedClientsIds = networkManager.ConnectedClientsIds;
            UpdateConnectionsClientRpc(connectedClientsIds.ToArray(), clientRpcParams);
        }

        [ClientRpc]
        private void UpdateConnectionsClientRpc(ulong[] connections, ClientRpcParams clientRpcParams = default)
        {
            if (IsOwner) return;

            foreach (var connection in connections)
            {
                Connections.Add(connection);
            }
            ConsoleController.Singleton.WriteLog($"Client after update {Connections.Count}");
            ClientsListUpdated?.Invoke();
        }
    }
}