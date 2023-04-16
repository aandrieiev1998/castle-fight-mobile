using TMPro;
using UnityEngine;

namespace UI.View
{
    public class NetStatsView : MonoBehaviour
    {

        [SerializeField] private TMP_Text _netStatsText;

        public TMP_Text NetStatsText => _netStatsText;
    }
}