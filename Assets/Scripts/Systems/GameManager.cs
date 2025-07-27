using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static GameObject playerInstance;
    public PlayerData playerData; 
    
    [SerializeField] private GameObject _player;

    public override void Awake()
    {
        base.Awake();
        playerInstance = _player;
    }
    
    //1. Will call GameOver when player is killed 
    
    //2. Should pause game when "ESC" is pressed
    
    //3. Should keep track of score for the player
    
    //
}