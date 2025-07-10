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
}
