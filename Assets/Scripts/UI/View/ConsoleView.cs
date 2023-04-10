using UnityEngine;

namespace UI.View
{
    public class ConsoleView : MonoBehaviour
    {
        [SerializeField] private Transform _listRoot;

        public Transform ListRoot => _listRoot;
    }
}