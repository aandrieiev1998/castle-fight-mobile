using UnityEngine;

namespace UI.View
{
    public class LobbyMenuView : MonoBehaviour
    {

        [SerializeField] private RectTransform _listRoot;

        public RectTransform ListRoot => _listRoot;
    }
}