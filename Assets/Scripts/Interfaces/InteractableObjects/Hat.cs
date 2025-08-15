using System;
using UnityEngine;

public class Hat : MonoBehaviour, IInteractable
{
    [Header("Movement")] 
    [SerializeField] private float movementAmplitude = 0.5f;
    [Header("Rotation")]
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 180, 0);

    [Header("Hat")]
    [SerializeField] private GameObject hatPrefab;
    [SerializeField] private Transform playerHead;
    private Vector3 _startPosition;
    InputController _inputController;
    PlayerController _playerController;
    private bool _isHatEquipped = false;

    private void Start()
    {
        _inputController.ShieldEvent += ActivateShield;
        _inputController.ShieldEventCancelled += DeactivateShield;
    }
    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
        float _amplitude = Mathf.PingPong(Time.time, movementAmplitude);
        transform.position = _startPosition + Vector3.up * _amplitude; //float movement
    }
    public void Interact()
    {
        //Equip hat and set it true 
        _isHatEquipped = true;
        Destroy(gameObject);
    }

    private  void EquipHat()
    {
        if (_isHatEquipped)
        {
            equippedHat= Instantiate(hatPrefab, playerHead);
        }
        //Intiate the hat to the head of the player 
    }

    private void ActivateShield()
    {
        throw new NotImplementedException();
        //playercontroller enable movement 
    }

    private void DeactivateShield()
    {
        
    }
}