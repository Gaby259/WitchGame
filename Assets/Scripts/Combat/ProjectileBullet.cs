using System;
using UnityEngine;

public class ProjectileBullet : Projectile
{
  private void Update()
  {
    transform.Translate(Vector3.up * (projectileSpeed * Time.deltaTime));
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.TryGetComponent<Enemy>(out Enemy enemy))
    {
      enemy.TakeDamage(damage);
    }

    if (impactParticles != null)
    {
      Instantiate(impactParticles, transform.position, transform.rotation);
      Destroy(gameObject); //Destroy Bullet
    }
    //impact particles
  }
}
