using System.Threading.Tasks;
using UnityEngine;

namespace MyCode
{
    public class PlayerInputController : MonoBehaviour
    {
        public async Task<DirectionType> WaitForDirectionAsync()
        {
            while (true)
            {
                DirectionType dir = GetDirection();
                if (dir != DirectionType.None)
                    return dir;

                await Task.Yield(); // ждать следующий кадр
            }
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