using UnityEngine;

public class Hat : MonoBehaviour, IInteractable
{
    [Header("Movement")]
    [SerializeField] private float movementAmplitude = 0.5f;
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 180, 0);

    [Header("Hat")]
    [SerializeField] private GameObject hatPrefab;
    [SerializeField] private Transform playerHead;

    [Header("Shield")]
    [SerializeField] private GameObject shieldEffectPrefab;

    private Vector3 _startPosition;
    private bool _isHatEquipped = false;

    private InputController _inputController;
    private PlayerController _playerController;
    private PlayerHealth _playerHealth;

    private GameObject _equippedHat;
    private GameObject _shieldEffectInstance;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (!_isHatEquipped)//Movement of the collectables goes up and down 
        {
            transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
            float amplitude = Mathf.PingPong(Time.time, movementAmplitude);
            transform.position = _startPosition + Vector3.up * amplitude;
        }
    }

    public void Interact()
    {
        _isHatEquipped = true;

        _playerController = FindObjectOfType<PlayerController>();
        _playerHealth = _playerController.GetComponent<PlayerHealth>();
        _inputController = _playerController.GetComponent<InputController>();

        if (hatPrefab != null && playerHead != null)
        {
            _equippedHat = Instantiate(hatPrefab, playerHead.position, playerHead.rotation, playerHead);
            _equippedHat.transform.localScale = Vector3.one;//Scale the Hat at the wanted sizes 
        }
        //use the input system (press Q) for activate the shield  
        _inputController.ShieldEvent += ActivateShield;
        _inputController.ShieldEventCancelled += DeactivateShield;

        Destroy(gameObject);
    }

    private void ActivateShield()
    {
        _playerController.SetMovementEnabled(false);
        _playerHealth.isShieldActive = true;

        if (shieldEffectPrefab != null && _shieldEffectInstance == null)
        {
            _shieldEffectInstance = Instantiate(shieldEffectPrefab, _playerController.transform);
            _shieldEffectInstance.transform.localPosition = Vector3.zero;
        }
    }

    private void DeactivateShield()
    {
        _playerController.SetMovementEnabled(true);
        _playerHealth.isShieldActive = false;

        if (_shieldEffectInstance != null)
        {
            Destroy(_shieldEffectInstance);
            _shieldEffectInstance = null;
        }
    }
}