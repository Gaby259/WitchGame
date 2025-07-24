using UnityEngine;

public class ProjectileWeapon : Weapon
{
  [SerializeField] Projectile _projectile;

    public override void Fire()
    {
        if (!CanFire())
        {
            return;
        }
        base.Fire(); // "base" go to the parent and call this particular function 
        
        Ray ray= Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 direction = ray.direction;
        var bullet = Instantiate(_projectile, firePoint.transform.position,Quaternion.LookRotation(direction));
        
    }

} 