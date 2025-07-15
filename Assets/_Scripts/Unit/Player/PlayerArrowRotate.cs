
using System.Threading;
using UnityEngine;

namespace Code
{
    public class PlayerArrowRotate : UnitRotate
    {
        [SerializeField] private GameObject _arrow;

        public async void ActivateArrow(DirectionType typeDirection, CancellationToken token = default)
        {
            _arrow.SetActive(true);
            await RotateToAsync(typeDirection, token);
        }

        public void DeactivateArrow()
        {
            _arrow.SetActive(false);
        }
    }
}