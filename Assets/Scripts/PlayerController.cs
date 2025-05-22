using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private PlayerControllerConfig controllerConfig;
    private CharacterController _characterController;
    private Vector2 _moveInput;
    private Vector3 _currentVelocity;
    private bool _isGrounded;
    private Vector2 _mouseDirection; 
    private Vector2 _mouseSensitivity;
    private float _mouseRotation;
    
    [SerializeField] private Camera camera;
    [SerializeField] LayerMask groundLayer;
    
    private InputController _inputController;
    
    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputController = GetComponent<InputController>();
    }

    void OnEnable()
    {
        if (_inputController != null)
        {
            Debug.Log("Input controller enabled");
            _inputController.MoveEvent += MovementInput;
            _inputController.JumpEvent += Jump;
            _inputController.MouseLookEvent += Rotation; 
        }
    }
    

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        //MOVEMENT
        Vector3 targetDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        Vector3 targetVelocity = targetDirection * controllerConfig._movementSpeed;

        float acceleration = IsGrounded() ? controllerConfig._groundcceleration : controllerConfig._airAceleration;
        _currentVelocity =  Vector3.MoveTowards(_currentVelocity, targetVelocity, acceleration * Time.deltaTime); //Time.deltaTime is for stopping
        
        
        //JUMP
            //. v0 = sqrt(-2 * a * s) 
        _characterController.Move(_currentVelocity * Time.deltaTime);
        
        //ROTATE
    }

    private void MovementInput (Vector2 movement)
    {
       _moveInput = movement; 
        
    }

    void Rotation(Vector2 mouseMovement)
    {
        float mouseX = mouseMovement.x * controllerConfig._mouseSensitivity;
        float mouseY = mouseMovement.y * controllerConfig._mouseSensitivity;

        // Horizontal rotation
        transform.Rotate(Vector3.up * mouseX);

        // Vertical rotation (camera)
        _mouseRotation -= mouseY;
        _mouseRotation = Mathf.Clamp(_mouseRotation, -controllerConfig._xCameraBounds, controllerConfig._xCameraBounds);
        camera.transform.localRotation = Quaternion.Euler(_mouseRotation, 0f, 0f);
        /*Vector2 mouse= new Vector2(_mouseDirection.x, _mouseDirection.y);
        _mouseDirection = Vector2.SmoothDamp(_mouseDirection, mouse, ref _mouseSensitivity, controllerConfig._xCameraBounds);
        _mouseRotation -= _mouseDirection.y;
        _mouseRotation = Mathf.Clamp(_mouseRotation, -controllerConfig._xCameraBounds, controllerConfig._xCameraBounds);
        transform.Rotate(Vector3.up, _mouseRotation);
        camera.transform.localRotation = Quaternion.Euler(_mouseRotation, 0, 0);*/
        
    }

    void Jump()
    {
        
        if (IsGrounded())
        {
            Debug.Log("Jump");
        }
        /*
        //Apply Gravity 
        _directionY-= controllerConfig._gravity * Time.deltaTime;
        _direction.y = _directionY;
        _controller.Move(_direction * controllerConfig._movementSpeed * Time.deltaTime);*/
        
    }

    private bool IsGrounded()
    {
        _isGrounded = Physics.SphereCast(transform.position, .5f, Vector3.down, out RaycastHit hit, .6f, groundLayer);
        return _isGrounded;
    }
    
}