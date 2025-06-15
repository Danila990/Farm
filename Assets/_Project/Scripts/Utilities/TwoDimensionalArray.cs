using System;
using UnityEngine;

namespace MyCode
{
    [Serializable]
    public class TwoDimensionalArray<T>
    {
        [SerializeField] private ArrayLine<T>[] _values;

        public int SizeX => _values.Length;
        public int SizeY => _values[0].LineValues.Length;

        public void Set(ArrayLine<T>[] values) => _values = values;

        public ArrayLine<T>[] GetAll() => _values;

        public T Get(int x, int y)
        {
            Vector2Int size = GetSize();
            if(x < 0 || y < 0 || x > size.x || y > size.y)
                throw new NullReferenceException($"The desired object is missing");

            return _values[x].LineValues[y];
        }

        public T[,] Convert()
        {
            T[,] newArray = new T[SizeX, SizeY];
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    newArray[x, y] = _values[x].LineValues[y];
                }
            }

            return newArray;
        }

        public Vector2Int GetSize() => new Vector2Int(_values.Length, _values[0].LineValues.Length);
    }

    [Serializable]
    public struct ArrayLine<T>
    {
        public T[] LineValues;
    }
}