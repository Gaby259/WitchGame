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
    Debug.Log("collide with " + other.name);
    if (other.TryGetComponent<Enemy>(out Enemy enemy))
    {
      Debug.Log("Hit Enemy");
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
