using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damagePerSecond = 10;
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered DamageZone");
            
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

