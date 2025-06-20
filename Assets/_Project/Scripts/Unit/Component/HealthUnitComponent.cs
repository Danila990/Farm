using UnityEngine;

namespace MyCode
{
    public class HealthUnitComponent : MonoBehaviour
    {
        private int _health;

        public int Health => _health;

        public void Setup(int health)
        {
            _health = health;
        }
    }
}