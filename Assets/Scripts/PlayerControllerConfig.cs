using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControllerConfig", menuName = "Scriptable Objects/PlayerControllerConfig")]
public class PlayerControllerConfig : ScriptableObject
{
    [Header("Movement")]
   public float _movementSpeed = 6f;
   public float _jumpHeight = 10f;
   public float _gravity = 9.81f;
   
    
    [Header("Rotation")]
    public float _smoothTime = 0.1f;
    public float _xCameraBounds = 80f;

}
