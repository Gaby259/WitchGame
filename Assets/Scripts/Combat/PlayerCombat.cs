using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private InputController _inputController;

    [SerializeField] private Weapon _equippedWeapon; //We just need to have the weapon and this script is in charge only of shoot and stopped

    private void Awake()
    {
        _inputController = GetComponent<InputController>();
       
    }

    void Start()
    {
        _inputController.AttackEvent += FireWeapon;
        _inputController.AttackEventCancelled += StopFireWeapon;
    }

    void FireWeapon()
    {
      
        _equippedWeapon.Fire();

    }

    void StopFireWeapon()
    {
      
    }

}
