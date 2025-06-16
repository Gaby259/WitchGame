using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private InputController _inputController;
    private CharacterController _characterController;
    
    [Header("Movement")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private PlayerControllerConfig controllerConfig;
    private Vector2 _moveInput;
    private Vector3 _currentVelocity;
    private bool _isGrounded;
    
    [Header("Look Rotation")]
    [SerializeField] private Transform lookTarget;
    private Vector2 _mouseRotation; 
    private Vector2 _mouseSensitivity;
   // private float _mouseRotation;
   
    
    
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
        Vector3 targetDirection = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        Vector3 targetVelocity = targetDirection * controllerConfig._movementSpeed;

        float acceleration = IsGrounded() ? controllerConfig._groundAcceleration : controllerConfig._airAceleration;
        
        _currentVelocity =  Vector3.MoveTowards(_currentVelocity, targetVelocity, acceleration  * Time.deltaTime); //Time.deltaTime is for stopping player to float 

        //DECELERATION
        if (targetDirection == Vector3.zero)// if input is realized return;
        {
            Vector3 horizontalVelocity = new Vector3(_currentVelocity.x, 0, _currentVelocity.z);//Ignore the Y velocity and take into account x,z 
            Vector3 deceleratedVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, controllerConfig._groundDeceleration* Time.deltaTime); //Vector3.MoveTowards(a, b, maxDistanceDelta)

            _currentVelocity.x = deceleratedVelocity.x;
            _currentVelocity.z = deceleratedVelocity.z;
           
        }
        
        //JUMP
        if (!IsGrounded()) //if the player is not touching the floor do this...
        {
            _currentVelocity.y += Physics.gravity.y * controllerConfig._gravity *Time.deltaTime;
        }
            
        
        _characterController.Move(_currentVelocity * Time.deltaTime);
        
        //ROTATE
        transform.Rotate(Vector3.up, _mouseRotation.x * controllerConfig._mouseSensitivity);
        lookTarget.Rotate(Vector3.right, -_mouseRotation.y * controllerConfig._mouseSensitivity);
    }

    private void MovementInput (Vector2 movement)
    {
       _moveInput = movement; 
        
    }

    void Rotation(Vector2 mouseRotation)
    {
       _mouseRotation = mouseRotation;
    }

    void Jump()
    {
        
        if (IsGrounded())
        {
            _currentVelocity.y = controllerConfig._jumpHeight;
        }
        
    }

    private bool IsGrounded()
    {
        _isGrounded = Physics.SphereCast(transform.position, .5f, Vector3.down, out RaycastHit hit, .6f, groundLayer);
        return _isGrounded;
    }
    
}