using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Details")]
    [SerializeField] protected int maxAmmo;
    protected int _currentAmmo;
    [SerializeField] private bool bAutomatic = false;
    [SerializeField] protected Transform firePoint;

    [Header("Weapon Animator")] 
    [SerializeField] protected Animator animator;
    private int _fireHash = Animator.StringToHash("Attack");

    private void Start()
    {
        _currentAmmo = maxAmmo;
    }
    public virtual void Fire()
    {
        {
            _currentAmmo--;
            animator?.SetTrigger(_fireHash); //this says "If animator exists, call set trigger on it
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