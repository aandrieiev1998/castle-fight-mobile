using Michsky.MUIP;
using TMPro;
using UnityEngine;

namespace UI.View
{
    public class  MainMenuView : MonoBehaviour
    {
        [SerializeField] private ButtonManager _hostButton;
        [SerializeField] private ButtonManager _joinButton;
        [SerializeField] private ButtonManager _exitButton;
        [SerializeField] private TMP_InputField _usernameField;
        [SerializeField] private NotificationManager _notificationManager;


        public ButtonManager HostButton => _hostButton;

        public ButtonManager JoinButton => _joinButton;

        public ButtonManager ExitButton => _exitButton;

        public TMP_InputField UsernameField => _usernameField;

        public NotificationManager NotificationManager => _notificationManager;
    }
}