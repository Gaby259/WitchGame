using System;
using System.Data.Common;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyAIState
{
   Idle,
   Patrol,
   Chase,
   Attack
}
public class Enemy : MonoBehaviour
{
   [Header("Movement")]
   [SerializeField] private EnemyAIState currentState = EnemyAIState.Idle;
   [SerializeField] private float enemiesHealth = 20;
   [SerializeField] private float detectionDistance = 5f;
   [SerializeField] private float chaseSpeed = 5f;
   [SerializeField] private float patrolSpeed = 5f;
   
   
   [Header("Target")]
   [SerializeField] GameObject currentTarget;
   
   [Header("Patrol")]
   [SerializeField] private Transform[] waypoints;
   private int currentWaypointIndex = 0;
   
   [Header("NavMesh")]
   NavMeshAgent _navMeshAgent;
   private void Start()
   {
      _navMeshAgent = GetComponent<NavMeshAgent>();
      _navMeshAgent.SetDestination(currentTarget.transform.position);
   }

   private void Update()
   { 
      switch (currentState)
      {
         case EnemyAIState.Idle:
            IdleBehaviour();
            break;
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

   void IdleBehaviour()
   {
      Debug.Log(gameObject.name + " is idle");
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

      if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.3f)
      {
         currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
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

