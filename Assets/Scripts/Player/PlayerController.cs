using System;
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
  
    [Header("Animation")]
    private Animator _playerAnimator;
    //These are variables for animation
    private int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private int _jumpHash = Animator.StringToHash("Jump");
    private int _isGroundedHash = Animator.StringToHash("IsGrounded");
    
    [Header("Interaction")]
    [SerializeField] private LayerMask interactionLayer;

   void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputController = GetComponent<InputController>();
        _playerAnimator = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        if (_inputController != null) 
        {
            _inputController.MoveEvent += MovementInput;
            _inputController.JumpEvent += JumpInput;
            _inputController.MouseLookEvent += RotationInput;
            _inputController.InteractEvent += AttempInteractInput;
        }
    }
    

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDestroy()
    {
        GameData gameData = new GameData();
        gameData.PlayerPosition = transform.position;
        SaveManager.Instance.SaveGame(gameData);
        Debug.Log("player transform saved");
    }

    void Update()
    {
      Movement();
      Jump();
      Rotate();
      ClampRotation();
     
    }
    

    private void MovementInput (Vector2 movement)
    {
       _moveInput = movement; 
    }

    private void Movement()
    {
        Vector3 targetDirection = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        Vector3 targetVelocity = targetDirection * controllerConfig.MovementSpeed;

        float acceleration = IsGrounded() ? controllerConfig.groundAcceleration : controllerConfig.AirAcceleration;
        
        _currentVelocity =  Vector3.MoveTowards(_currentVelocity, targetVelocity, acceleration  * Time.deltaTime); //Time.deltaTime is for stopping player to float 
        Vector3 horizontalFinalVelocity = new Vector3(_currentVelocity.x, 0, _currentVelocity.z);//Ignore the Y velocity and take into account x,z 
        Vector3 deceleratedVelocity = Vector3.MoveTowards(horizontalFinalVelocity, Vector3.zero, controllerConfig.groundDeceleration* Time.deltaTime); //Vector3.MoveTowards(a, b, maxDistanceDelta)

        //DECELERATION
       if (targetDirection == Vector3.zero)// if input is realized return;
        {
            _currentVelocity.x = deceleratedVelocity.x;
            _currentVelocity.z = deceleratedVelocity.z;
        }
        _playerAnimator.SetFloat(_moveSpeedHash, horizontalFinalVelocity.magnitude);
        
    }
    

    private void RotationInput(Vector2 mouseRotation)
    {
       _mouseRotation = mouseRotation;
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, _mouseRotation.x * controllerConfig.mouseSensitivity);
        lookTarget.Rotate(Vector3.right, -_mouseRotation.y * controllerConfig.mouseSensitivity);
    }
    
    private void ClampRotation()
    {
        //Camera Clamp Rotation
        float currentX = lookTarget.rotation.eulerAngles.x;
        if (currentX > 180) // look at the opposite direction camerabounds
        {
            if (currentX < 360 - controllerConfig.CameraBounds)
            {
                currentX = 360 - controllerConfig.CameraBounds;
            }
        }
        else if (currentX > controllerConfig.CameraBounds)
        {
            currentX = controllerConfig.CameraBounds;

        }
        Vector3 clampRotation = transform.eulerAngles;
        clampRotation.x = currentX;
        lookTarget.eulerAngles = clampRotation;
    }
    

    private void JumpInput()
    {
        
        if (IsGrounded())
        {
            _currentVelocity.y = controllerConfig.jumpHeight;
            _playerAnimator.SetTrigger(_jumpHash);
        }
        
    }

    private bool IsGrounded()
    {
        _isGrounded = Physics.SphereCast(transform.position, .5f, Vector3.down, out RaycastHit hit, .6f, groundLayer);
        return _isGrounded;
    }

    private void Jump()
    {
        if (!IsGrounded()) //if the player is not touching the floor do this...
        {
            _currentVelocity.y += Physics.gravity.y * controllerConfig.gravity *Time.deltaTime;
        }
        _characterController.Move(_currentVelocity * Time.deltaTime);
        
        _playerAnimator.SetBool(_isGroundedHash, IsGrounded());
    
    }

    private void AttempInteractInput()
    {
        AttempInteract();
    }
    private void AttempInteract()
    {
        Vector3 origin = lookTarget.position;
        Debug.DrawRay(origin, lookTarget.forward * controllerConfig.interactDistance, Color.red);
        if (Physics.Raycast(origin, lookTarget.forward, out RaycastHit hit, controllerConfig.interactDistance,
                interactionLayer))
        {
           IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
           {
               interactable.Interact();
           }
          
        }
       
    }
    
}