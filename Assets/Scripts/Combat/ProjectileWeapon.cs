using UnityEngine;

public class ProjectileWeapon : Weapon
{
  [SerializeField] Projectile _projectile;

    public override void Fire()
    {
        if (!CanFire())
        {
    //        Debug.Log("Cant Fire");
            return;
        }
//        Debug.Log("Firing");
        base.Fire(); // "base" go to the parent and call this particular function 
        var bullet = Instantiate(_projectile, firePoint.transform.position, firePoint.transform.rotation);
    }

} 