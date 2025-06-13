using UnityEngine;

public class CharacterInput
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
