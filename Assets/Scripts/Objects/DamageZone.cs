using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damagePerSecond = 10;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
           if (playerHealth != null)
           {
               playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
           }
        }
       
    }
   
}

