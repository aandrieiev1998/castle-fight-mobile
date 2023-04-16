using Michsky.MUIP;
using TMPro;
using UnityEngine;

namespace UI.View
{
    public class LobbyMenuView : MonoBehaviour
    {
        [SerializeField] private RectTransform _listRoot;
        [SerializeField] private TMP_Text _labelText;
        [SerializeField] private ButtonManager _startButton;

        public RectTransform ListRoot => _listRoot;
        
        public TMP_Text LabelText => _labelText;

        public ButtonManager StartButton => _startButton;
    }
}