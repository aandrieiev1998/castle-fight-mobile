using Michsky.MUIP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class JoinMenuView : MonoBehaviour
    {
        [SerializeField] private ButtonManager _joinButton;
        [SerializeField] private ButtonManager _backButton;
        [SerializeField] private TMP_InputField _connectionInputField;

        public ButtonManager JoinButton => _joinButton;

        public ButtonManager BackButton => _backButton;

        public TMP_InputField ConnectionInputField => _connectionInputField;
    }
}