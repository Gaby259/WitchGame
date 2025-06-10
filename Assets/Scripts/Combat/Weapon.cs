using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Details")]
    [SerializeField] private int maxAmmo;
    private int _currentAmmo;
    [SerializeField] private float fireSpeed = .2f;
    [SerializeField] private bool bAutomatic = false;
    [SerializeField] protected Transform firePoint;


    private void Start()
    {
        _currentAmmo = maxAmmo;
    }
    virtual public void Fire()
    {
        {
            _currentAmmo--;
            //Start Shooting cooldown 
            //PLay sound effect 
            // Spawn particle at muzzle point 
            
        }
           
           
    }

    protected  bool CanFire() //Childs can access to this function "protected" without become public 
    {
        return _currentAmmo > 0;
    }
   
}