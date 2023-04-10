using TMPro;
using UnityEngine;

namespace UI.View
{
    public class LobbyMenuView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _labelText;
        [SerializeField] private RectTransform _listRoot;

        public RectTransform ListRoot => _listRoot;
        
        public TMP_Text LabelText => _labelText;
    }
}