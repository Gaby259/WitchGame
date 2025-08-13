using UnityEngine;
public interface IState
{
    void Enter();
    void Update();
    void Exit();
}
public class PlayingState: IState
{
    public void Enter()
    {
        // Enable player input, start enemy spawning, etc.
        Time.timeScale = 1;
    }

    public void Update()
    {
        // Check for win/loss conditions to transition state
    }

    public void Exit()
    {
        // Disable player input, etc.
    }
    
}
public class PauseState : IState
{
    public void Enter()
    {
        Time.timeScale = 0;
        Debug.Log("Game Paused");
    }

    public void Update()
    {
        // Lógica del estado "Pausa"
    }

    public void Exit()
    {
        Time.timeScale = 1;
        Debug.Log("Resuming Game");
    }
}