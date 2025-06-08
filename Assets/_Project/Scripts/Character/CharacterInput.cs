using UnityEngine;
using VContainer.Unity;

public class CharacterInput : IStartable, ITickable
{
    private GameInputSystem _gameInputSystem;

    public void Start()
    {
        _gameInputSystem = new GameInputSystem();
        _gameInputSystem.Enable();
    }

    public void Tick()
    {
        
    }
}
