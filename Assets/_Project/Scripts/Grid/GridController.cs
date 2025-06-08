using MyCode;
using System;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public event Action<GridMap> ChangeMap;

    [SerializeField] private GridMap[] _maps;

    private GridMap _currentMap;

    public GridMap GetGridFieldMap()
    {
        return _currentMap;
    }

    public void ActivateFirstGridField()
    {

    }

    public void PreviousGridField()
    {

    }

    public void NextGridField()
    {

    }

    public void ResetGridField()
    {

    }
}
