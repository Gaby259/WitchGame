using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputController _inputController;
    private CharacterController _characterController;
    private Vector2 _movemeInput;
    private void Awake()
    {
        _inputController = GetComponent<InputController>();
    }

    private void OnEnable()
    {
        if (_inputController != null)
        {
            _inputController.MoveEvent += HandleMoveInput;
            _inputController.JumpEvent += Jump;
        }
    }

    private void Update()
    {
        //Handle Character Movement Here 
    }
    private void HandleMoveInput(Vector2 movement)
    {
        _movemeInput = movement;
        //here is the variables created for movement in the S.C
        Debug.Log ($"X: {movement.x}, Y: {movement.y}");
    }
    private void Jump()
    {
        //Same variables for jumping from S.C
        Debug.Log("Jump");    
    }
    
}
