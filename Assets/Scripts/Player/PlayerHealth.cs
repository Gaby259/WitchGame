using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Animator _playerAnimator;
    private int _takingDamageHash = Animator.StringToHash("IsTakingDamage");
    
    //Send info to player data
    private PlayerData _playerData;

    private void Awake()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _playerData = GameManager.Instance.playerData;
        if (_playerData.currentHealth <= 0)
        {
            _playerData.currentHealth = maxHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        _playerData.currentHealth -= amount;
        _playerAnimator.SetTrigger(_takingDamageHash);
        if (_playerData.currentHealth <= 0)
        {
            Die();
        }
        
    }
    private void Die()
    {
        Debug.Log("Player died!");
        //Death animation sequence can be play here 
        
        //deactivate the player
        //set new positon to the checkpoint
        //reactivate player again
    }
}
