using System;
using System.Data.Common;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.AI;

public enum EnemyAIState
{
   Patrol,
   Chase,
   Attack
}
public class Enemy : MonoBehaviour
{
   [Header("Movement")]
   [SerializeField] private EnemyAIState currentState = EnemyAIState.Patrol;
   [SerializeField] private float enemiesHealth = 20;
   [SerializeField] private float detectionDistance = 5f;
   [SerializeField] private float chaseSpeed = 5f;
   [SerializeField] private float patrolSpeed = 5f;

   [Header("Attack")] 
   [SerializeField] private float damage = 5;
   [SerializeField] private float attackRange =1.5f;
   
   
   [Header("Target")]
   [SerializeField] GameObject currentTarget;
   
   [Header("Patrol")]
   [SerializeField] private Transform[] waypoints;
   private int currentWaypointIndex = 0;
   
   [Header("NavMesh")]
   NavMeshAgent _navMeshAgent;

   [Header("Animation")]
   [SerializeField] private Animator _enemyAnimator;
   private int _chaseHash = Animator.StringToHash("Chase");
   private int _attackHash = Animator.StringToHash("Attack");
   private void Start()
   {
      _navMeshAgent = GetComponent<NavMeshAgent>();
      _navMeshAgent.SetDestination(currentTarget.transform.position);
   }

   private void Update()
   { 
      switch (currentState)
      {
         case EnemyAIState.Patrol:
            PatrolBehaviour();
            break;
         case EnemyAIState.Chase:
            ChaseBehaviour();
            break;
         case EnemyAIState.Attack:
            AttackBehavior();
            break;
      }
   }
   
   void PatrolBehaviour()
   {
      Debug.Log(gameObject.name + " is patrol");
      if (waypoints.Length == 0)
      {
         return;
      }
      _navMeshAgent.speed = patrolSpeed;
      Transform targetWaypoint = waypoints[currentWaypointIndex];
      _navMeshAgent.SetDestination(targetWaypoint.position);

      if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.3f) //calculates when the enemy arrive the point go to the next one
      {
         currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         Debug.Log("Enemy saw the player");
         currentState= EnemyAIState.Chase;
      }
   }

   void OnDrawGizmos()
   {
      if (waypoints != null && waypoints.Length > 0)
      {
         Gizmos.color = Color.red;
         foreach (Transform waypoint in waypoints)
         {
            if (waypoint != null)
            {
               Gizmos.DrawSphere(waypoint.position, 0.3f);
            }
         }
         Gizmos.color = Color.green;
         for (int i = 0; i < waypoints.Length; i++)
         {
            Gizmos.DrawLine(waypoints[i].position, waypoints[(i + 1) % waypoints.Length].position);
         }
      }
   }

   void ChaseBehaviour()
   {
      _navMeshAgent.speed = chaseSpeed;
      _enemyAnimator.SetTrigger(_chaseHash);
      Debug.Log(gameObject.name + " is chase");
    float currentDistance = Vector3.Distance(transform.position, currentTarget.transform.position);
    _navMeshAgent.SetDestination(currentTarget.transform.position);
    
    if (currentDistance < detectionDistance)
    {
       currentState = EnemyAIState.Attack;
    }
   }

   void AttackBehavior()
   {
      bool playerInRange = false;
      Debug.Log(gameObject.name + "is Attacking");
      Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);

      foreach (var hit in hits) // Loop through each collider detected
      {
         if (hit.CompareTag("Player"))
         {
            playerInRange = true;
            Debug.Log("Player has enter the damage zone");
            PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
            _enemyAnimator.SetTrigger(_attackHash);
            if (playerHealth != null)
            {
               playerHealth.TakeDamage(damage);
              
            }
            
         }
      }
      //If player is out of range change to chase patrol mode
      if (!playerInRange)
      {
         currentState = EnemyAIState.Chase;
      }
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

