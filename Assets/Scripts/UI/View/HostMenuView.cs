using Michsky.MUIP;
using TMPro;
using UnityEngine;

namespace UI.View
{
    public class HostMenuView : MonoBehaviour
    {
        [SerializeField] private ButtonManager _hostButton;
        [SerializeField] private ButtonManager _backButton;
        [SerializeField] private TMP_InputField _connectionInputField;

        public ButtonManager HostButton => _hostButton;

        public ButtonManager BackButton => _backButton;

        public TMP_InputField ConnectionInputField => _connectionInputField;
        
    }
}