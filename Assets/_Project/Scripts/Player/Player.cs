using System;
using System.Collections;
using UnityEngine;


namespace MyCode
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private MoveUnitComponent _moveUnit;
        [SerializeField] private RotateUnitComponent _rotateUnit;

        private IGridMap _map;
        private Vector2Int _playerIndex;

        private void Start()
        {
            Platform spawn = _map.FindPlatform(PlatformType.PlayerSpawn);
            _playerIndex = spawn.GridIndex;
            transform.position = new Vector3(0, 0.5f, 0) + spawn.transform.position;
        }

        private void Update()
        {
            if (_moveUnit.IsMoved)
                return;

            DirectionType nextDirection = GetDirection();
            if (nextDirection == DirectionType.None)
                return;

            _rotateUnit.StartRotate(nextDirection);
            Vector2Int nextIndex = _playerIndex + nextDirection.ToVector();
            /*if (_map.TryGetPlatform(nextIndex, out Platform platform))
            {
                if (platform.IsCanMove)
                {
                    _playerIndex = nextIndex;
                    _moveUnit.StartMove(platform.transform.position);
                    platform.Event();
                }
            }*/
        }

        private DirectionType GetDirection()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                return DirectionType.Up;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                return DirectionType.Down;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                return DirectionType.Left;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                return DirectionType.Right;

            return DirectionType.None;
        }
    }
}