using UnityEngine;

namespace MyCode
{
    public class HealthUnitComponent : MonoBehaviour
    {
        [SerializeField] private int _health;

        public int Health => _health;

    }
}