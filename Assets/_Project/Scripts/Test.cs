using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject]
    public void Construct()
    {
        Debug.Log("test");
    }
}
