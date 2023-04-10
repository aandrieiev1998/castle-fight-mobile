using Michsky.MUIP;
using UnityEngine;

namespace UI.View
{
    public class LoadingMenuView : MonoBehaviour
    {
        [SerializeField] private UIManagerProgressBarLoop _spinner;

        public UIManagerProgressBarLoop Spinner => _spinner;

        
    }
}