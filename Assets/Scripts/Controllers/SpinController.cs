using Rullet;
using UnityEngine;

namespace Controllers
{
    public class SpinController : MonoBehaviour
    {
        private void Awake()
        {
            EventController.OnPlayerReachBonus += ResetSpin;
        }

        private void OnDestroy()
        {
            EventController.OnPlayerReachBonus -= ResetSpin;
        }

        void ResetSpin(int turnCount)
        {
            switch (turnCount)
            {
                case 5:
                    EventController.OnSpinStateChange?.Invoke(SpinState.Safe);
                    break;

                case 30:
                    EventController.OnSpinStateChange?.Invoke(SpinState.Super);
                    break;

                case 31:
                EventController.OnSpinStateChange?.Invoke(SpinState.Super);
                    break;
                
                default:
                    EventController.OnSpinStateChange?.Invoke(SpinState.Normal);
                    break;
            }
        }

    }
}