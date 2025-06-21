using UnityEngine;
using UnityEngine.InputSystem.XR;
using VContainer;


namespace MyCode
{
    public class FinishPlatform : Platform
    {
        [Inject] private GameGrid _controller;

        public override void Event()
        {
            //_controller.NextGridMap();
            Debug.Log("Finish");
        }
    }
}