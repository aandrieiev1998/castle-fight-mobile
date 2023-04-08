using System;
using Unity.Netcode;
using UnityEngine;

namespace Network
{
    public class NetworkConnectionsManager : MonoBehaviour
    {
        private NetworkManager networkManager;
        public static NetworkConnectionsManager Singleton { get; private set; }

        private void Awake()
        {
            if (Singleton != null)
                throw new Exception($"Detected more than 1 instance of {nameof(NetworkConnectionsManager)}");

            Singleton = this;

            networkManager = NetworkManager.Singleton;
            networkManager.OnClientConnectedCallback += OnClientConnected;
            networkManager.OnClientDisconnectCallback += OnClientDisconnect;
        }

        public event Action<ulong, ConnectionStatus> OnClientConnection;

        private void OnClientConnected(ulong clientId)
        {
            Debug.Log($"Client connected: {clientId}");
            OnClientConnection?.Invoke(clientId, ConnectionStatus.Connected);
        }

        private void OnClientDisconnect(ulong clientId)
        {
            Debug.Log($"Client disconnected: {clientId}");
            OnClientConnection?.Invoke(clientId, ConnectionStatus.Disconnected);
        }
    }
}