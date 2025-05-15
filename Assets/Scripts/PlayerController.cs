using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private PlayerControllerConfig controllerConfig;
    private CharacterController _characterController;
    private Vector2 _moveInput;
    private Vector3 _currentVelocity;
    
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
        }
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        Vector3 targetDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        Vector3 targetVelocity = targetDirection * controllerConfig._movementSpeed;

        float acceleration = IsGrounded() ? controllerConfig._accelerationRate : controllerConfig._airAcelerationRate;
        _currentVelocity =  Vector3.MoveTowards(_currentVelocity, targetVelocity, acceleration * Time.deltaTime); //Time.deltaTime is for stopping
        
        _characterController.Move(_currentVelocity * Time.deltaTime);
    
      
    }

    private void MovementInput (Vector2 movement)
    {
       Debug.Log(movement);
       _moveInput = movement; 
        
    }

    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X");//left and right
        float mouseY = Input.GetAxis("Mouse Y");// up and down
      
        /*Vector2 targetDelta = new Vector2(mouseX, mouseY);
        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetDelta, ref _currentMouseVelocity, controllerConfig._xCameraBounds);
        _xRotation -= _currentMouseDelta.y;
        _xRotation = Mathf.Clamp(_xRotation,-controllerConfig._xCameraBounds, controllerConfig._xCameraBounds);
        transform.Rotate(Vector3.up, _currentMouseDelta.x);
        camera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);*/
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
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.red, 10f);
        if (Physics.SphereCast(transform.position, .5f, Vector3.down, out RaycastHit hit, .6f, groundLayer))
        {
            Debug.Log("Hit the ground");
            return true;
        }
        Debug.Log("No Grounded");
        return false;
    }
    
    
    
}
