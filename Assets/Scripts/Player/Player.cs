using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class Player : NetworkBehaviour
    {
        private void Start()
        {
            Debug.Log($"Player spawned: {NetworkObjectId}, {NetworkBehaviourId}");
        }
    }
}