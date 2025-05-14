using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public PlayerControllerConfig controllerConfig;
    private CharacterController _controller;
 
    private Vector3 _direction;
    private float _directionY;
    
    private Vector2 _currentMouseDelta;
    private float _xRotation;
    private Vector2 _currentMouseVelocity;
    
    [SerializeField] private Camera _camera;
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
      Movement();
      Rotation();
      Jump();
    }

    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _direction = horizontal * transform.right + vertical * transform.forward;
        
        
    }

    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X");//left and right
        float mouseY = Input.GetAxis("Mouse Y");// up and down
      
        Vector2 targetDelta = new Vector2(mouseX, mouseY);
        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetDelta, ref _currentMouseVelocity, controllerConfig._xCameraBounds);
        _xRotation -= _currentMouseDelta.y;
        _xRotation = Mathf.Clamp(_xRotation,-controllerConfig._xCameraBounds, controllerConfig._xCameraBounds);
        transform.Rotate(Vector3.up, _currentMouseDelta.x);
        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
    }

    void Jump()
    {
        if (Input.GetButtonDown ("Jump") && _controller.isGrounded)
        {
            _directionY = controllerConfig._jumpHeight; 
        }
        //Apply Gravity 
        _directionY-= controllerConfig._gravity * Time.deltaTime;
        _direction.y = _directionY;
        _controller.Move(_direction * controllerConfig._movementSpeed * Time.deltaTime);
        
    }
    
}
