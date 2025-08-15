using UnityEngine;

public class EnemyBullet : Projectile
{
    private void Update()
    {  
        transform.Translate(Vector3.forward * (projectileSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(damage);
        }

        if (impactParticles != null)
        {
            Instantiate(impactParticles, transform.position, transform.rotation);
            Destroy(gameObject); 
        }
   
    }
}
