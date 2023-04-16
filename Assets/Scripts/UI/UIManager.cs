using System;
using UI.Manager;
using UI.View;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        private NetworkManager networkManager;
        public MainMenuManager MainMenuManager { get; private set; }
        public GUIManager GUIManager { get; private set; }

        public static UIManager Singleton { get; private set; }

        private void Awake()
        {
            if (Singleton != null)
                throw new Exception($"Detected more than 1 instance of {nameof(UIManager)}");

            Singleton = this;
            DontDestroyOnLoad(Singleton);
        }

        private void Start()
        {
            networkManager = NetworkManager.Singleton;
            networkManager.OnServerStarted += OnServerStarted;

            MainMenuManager = FindObjectOfType<MainMenuManager>();
            MainMenuManager.NavigateToMenu(typeof(MainMenuView));
        }

        private void OnDestroy()
        {
            networkManager.SceneManager.OnLoadComplete -= OnNetworkSceneLoaded;
        }

        private void OnServerStarted()
        {
            networkManager.SceneManager.OnLoadComplete += OnNetworkSceneLoaded;
        }

        private void OnNetworkSceneLoaded(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
        {
            Debug.Log("Scene loaded");
        }
    }
}