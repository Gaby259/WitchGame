using System.Data.Common;
using UnityEngine;

public enum EnemyAIState
{
   Idle,
   Patrol,
   Chase,
   Attack
}
public class Enemy : MonoBehaviour
{
   [SerializeField] private EnemyAIState currentState = EnemyAIState.Idle;
   [SerializeField] private float enemiesHealth = 20;
   private void Update()
   {
      switch (currentState)
      {
         case EnemyAIState.Idle:
            IdleBehaviour();
            break;
      }
   }

   void IdleBehaviour()
   {
      //Debug.Log(gameObject.name + " is idle");
   }

   void PatrolBehaviour()
   {
      Debug.Log(gameObject.name + " is patrol");
   }

   void ChaseBehaviour()
   {
      Debug.Log(gameObject.name + " is chase");
   }

   void AttackBehavior()
   {
      Debug.Log(gameObject.name +"is Attacking");
   }

   public virtual void TakeDamage(float damageAmount) //Is virtual so enemies subclasses can override it 
   {
      Debug.Log(gameObject.name + " is " + damageAmount + " damage");
      enemiesHealth -= damageAmount;
      if (enemiesHealth <= 0)
      {
         Die();
      }
   }

   protected virtual void Die()
   {
      Destroy(gameObject);
      //Enemy is dead , animation sequence can be play
   }
}

