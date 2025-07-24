using System;
using UnityEngine;

public class ProjectileBullet : Projectile
{
  private void Update()
  {  
    transform.Translate(Vector3.forward * (projectileSpeed * Time.deltaTime));
    Debug.DrawRay(transform.position, transform.forward * projectileSpeed, Color.red);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.TryGetComponent<Enemy>(out Enemy enemy))
    {
      enemy.TakeDamage(damage);
    }
    //if hits player ignore

    if (impactParticles != null)
    {
      Debug.Log(other.name + " is impacted");
      Instantiate(impactParticles, transform.position, transform.rotation);
      Destroy(gameObject); 
    }
   
  }
}
