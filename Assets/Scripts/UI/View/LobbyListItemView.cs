using TMPro;
using UnityEngine;

namespace UI.View
{
    public class LobbyListItemView : MonoBehaviour
    {

        [SerializeField] private TMP_Text _playerNameText;

        public TMP_Text PlayerNameText => _playerNameText;
    }
}