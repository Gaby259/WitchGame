using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ProBuilder;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Animator _playerAnimator;
    private float currentHealth;
    public UnityEvent<float> OnPlayerTakeDamage;
    private int _takingDamageHash = Animator.StringToHash("IsTakingDamage");

    private void Awake()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        currentHealth =maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0, maxHealth);//health can't go bellow 0 
        _playerAnimator.SetTrigger(_takingDamageHash);
        OnPlayerTakeDamage.Invoke(GetHealthPercentage());
        //play sound of taking damage 
        Debug.Log("current health" + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        
    }
    private void Die()
    {
        Debug.Log("Player died!");
        GameManager.Instance.PlayerLose();
        //Death animation sequence can be play here 
        
        //deactivate the player
        //set new positon to the checkpoint
        //reactivate player again
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }
}
