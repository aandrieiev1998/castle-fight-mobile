using UI.View;
using Unity.Netcode;
using UnityEngine;

namespace UI.Controller
{
    public class NetStatsController : MonoBehaviour
    {
        private const string Format = "GameObjectId {0}\nBehaviourId {1}\nClientId {2}";

        [SerializeField] private NetStatsView _netStatsView;

        public NetStatsView NetStatsView => _netStatsView;

        private void Start()
        {
            var clientId = NetworkManager.Singleton.LocalClientId;
            var networkObjectId = NetworkManager.Singleton.LocalClient.PlayerObject.NetworkObjectId;
            var networkBehaviourId = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<NetworkBehaviour>().NetworkBehaviourId;
            var output = string.Format(Format, networkObjectId, networkBehaviourId, clientId);
            _netStatsView.NetStatsText.SetText(output);
        }
    }
}