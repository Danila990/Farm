using UnityEngine;

namespace ProjectCode
{
    [CreateAssetMenu(fileName = nameof(DataFactory), menuName = "SO/DataFactory")]
    public class DataFactory : ScriptableObject
    {
        [SerializeField] private MoodArray<Object> _datas;

        public MoodArray<Object> Data => _datas;
    }
}