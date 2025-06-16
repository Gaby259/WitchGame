using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Details")]
    [SerializeField] protected int maxAmmo;
    protected int _currentAmmo;
    [SerializeField] private float fireSpeed = .2f;
    [SerializeField] private bool bAutomatic = false;
    [SerializeField] protected Transform firePoint;


    private void Start()
    {
        _currentAmmo = maxAmmo;
    }
    public virtual void Fire()
    {
        {
            _currentAmmo--;
            //Start Shooting cooldown 
            //PLay sound effect 
            
        }
           
           
    }

    protected  bool CanFire() //Child can access to this function "protected" without become public 
    {
        return _currentAmmo > 0;
    }

    public void RefillAmmo()
    {
        _currentAmmo = maxAmmo;
    }
}