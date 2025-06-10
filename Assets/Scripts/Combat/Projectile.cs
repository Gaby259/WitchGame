using UnityEngine;

public class Projectile : Weapon
{
    [SerializeField] Projectile _projectile;

    public override void Fire()
    {
        if (!CanFire())
        {
            return;
        }
        Debug.Log("Projectile is Fire");
        base.Fire(); // "base" go to the parent and call this particular function 
        Instantiate(_projectile, firePoint.transform.position, firePoint.transform.rotation);
    }

} 