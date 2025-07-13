using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Animator _playerAnimator;
    private int _takingDamageHash = Animator.StringToHash("IsTakingDamage");
    private float currentHealth;
    

    private void Awake()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        _playerAnimator.SetTrigger(_takingDamageHash);
        if (currentHealth <= 0)
        {
            Die();
        }
        
    }
    private void Die()
    {
        Debug.Log("Player died!");
        //Death animation sequence can be play here 
    }
   
}
