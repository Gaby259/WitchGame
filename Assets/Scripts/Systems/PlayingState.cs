using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playinpublic: IState
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