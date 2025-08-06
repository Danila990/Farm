using Project1;
using System.Threading;
using UnityEngine;


namespace ProjectCode
{
    public class PlayerDirectionArrow : MonoBehaviour
    {
        [SerializeField] private GameObject _arrow;
        [SerializeField] private GameObject _arrowNone;


        private IInputService _input;
        private PlayerGridMover _gridMover;
        private RotateComponent _rotate;
        private CancellationTokenSource _cts;

        public void Init(PlayerGridMover gridMover, PlayerInfo playerInfo)
        {
            _rotate = new RotateComponent(_arrow.transform, playerInfo.SpeedArrow);
            _cts = new CancellationTokenSource();
            
            _gridMover = gridMover;
            _input = ServiceLocator.Get<IInputService>();
        }

        private void Update()
        {
            DirectionType inputType = _input.GetDirection();
            if (_gridMover.IsActive)
            {
                _arrow.SetActive(true);
                _arrowNone.SetActive(false);
            }
            else
            {
                _arrow.SetActive(false);
                _arrowNone.SetActive(true);
            }

            if (inputType == DirectionType.None || _rotate.IsRotated) return;

            _cts?.Cancel();
            _rotate.Rotate(inputType, _cts.Token);
        }
    }
}