using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerControllerConfig", menuName = "Scriptable Objects/PlayerControllerConfig")]
public class PlayerControllerConfig : ScriptableObject
{
    [Header("Movement")]
   public float _movementSpeed = 6f;
   public float _groundAcceleration = 10f; //max velocity when player is on the floor 
   public float _groundDeceleration = 10f;
   
    [Header("Jump")]
    public float _jumpHeight = 10f;
    public float _gravity = 9.81f;
    public float _airAceleration = 5f; //max velocity when player is in the air 
    
    [Header("Rotation")]
    public float _xCameraBounds = 80f;
    public float _mouseSensitivity = 100f;
    

}
