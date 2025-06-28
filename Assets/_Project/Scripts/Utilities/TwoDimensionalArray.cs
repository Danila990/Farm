using System;
using UnityEngine;

namespace MyCode
{
    [Serializable]
    public class TwoDimensionalArray<T>
    {
        [SerializeField] private ArrayLine<T>[] _values;

        public int LenghtX => _values.Length;
        public int LenghtY => _values[0].LineValues.Length;

        public Vector2Int GetSize()
        {
            return new Vector2Int(LenghtX, LenghtY);
        }

        public void Set(ArrayLine<T>[] values)
        {
            _values = values;
        }

        public ArrayLine<T>[] GetAll()
        {
            return _values;
        }

        public T Get(int x, int y)
        {
            if (!Check(x, y))
                throw new ArgumentException();

            return _values[x].LineValues[y];
        }

        public bool Check(int x, int y)
        {
            if (x < 0 || y < 0 || x >= LenghtX || y >= LenghtY)
                return false;

            return true;
        }

        public T[,] Convert()
        {
            T[,] newArray = new T[LenghtX, LenghtY];
            for (int x = 0; x < LenghtX; x++)
            {
                for (int y = 0; y < LenghtY; y++)
                {
                    newArray[x, y] = _values[x].LineValues[y];
                }
            }

            return newArray;
        }
    }

    [Serializable]
    public struct ArrayLine<T>
    {
        public T[] LineValues;
    }
}