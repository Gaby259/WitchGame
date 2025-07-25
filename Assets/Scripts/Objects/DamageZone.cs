using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damagePerSecond = 10;
    
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
           Debug.Log("Player has enter the damage zone");
           PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
           if (playerHealth != null)
           {
               playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
        /*if (other.CompareTag("Player"))
        {
            Debug.Log("Player has enter the damage zone");
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }*/
    }
   
}

