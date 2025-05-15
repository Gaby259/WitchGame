using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControllerConfig", menuName = "GameConfig /PlayerControllerConfig")]
public class PlayerControllerConfig : ScriptableObject
{
    [Header("Movement")]
   public float _movementSpeed = 6f;
   public float _jumpHeight = 10f;
   public float _gravity = 9.81f;
   
    
    [Header("Rotation")]
    public float _xCameraBounds = 80f;

}
