using System.Collections;
using UnityEngine;
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
   [SerializeField] protected float enemiesHealth = 20;
   [SerializeField] protected float detectionDistance = 5f;
   [SerializeField] private float chaseSpeed = 5f;
   [SerializeField] private float patrolSpeed = 5f;

   [Header("Attack")] 
   [SerializeField] protected float damage = 5;
   [SerializeField] protected float attackRange =1.5f;
   [SerializeField] protected float _attackCooldown = 2f;
   protected bool _attackOnCooldown = false;
   private bool _canAttack = false;
   
   
   [Header("Target")]
   protected GameObject _currentTarget = GameManager.playerInstance;
   
   [Header("Patrol")]
   [SerializeField] private Transform[] waypoints;
   private int currentWaypointIndex = 0;
   
   [Header("NavMesh")]
   protected NavMeshAgent _navMeshAgent;

   [Header("Animation")]
   [SerializeField] private Animator _enemyAnimator;
   private int _chaseHash = Animator.StringToHash("Chase");
   private int _attackHash = Animator.StringToHash("Attack");
   private void Start()
   {
      _navMeshAgent = GetComponent<NavMeshAgent>();
      _currentTarget = GameManager.playerInstance;
      
      if (_navMeshAgent != null)//that way the child if it doesn't move will not have problem spawning without navmesh
      {
         _navMeshAgent.SetDestination(_currentTarget.transform.position);
      }     
      _canAttack = true; 
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
      float currentDistance = Vector3.Distance(transform.position, _currentTarget.transform.position);
      _navMeshAgent.SetDestination(_currentTarget.transform.position);
    
    if (currentDistance < detectionDistance)
    {
       currentState = EnemyAIState.Attack;
    }
   }

   private void AttackBehavior()
   {
      bool playerInRange = false;
      Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);

      foreach (var hit in hits) // Loop through each collider detected
      {
         if (hit.CompareTag("Player"))
         {
            playerInRange = true;
            
            if (_canAttack)
            {
               PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
               _enemyAnimator.SetTrigger(_attackHash);
               if (playerHealth != null)
               {
                  playerHealth.TakeDamage(damage);
               }
               Debug.Log("AttackCooldown");
               StartCoroutine(AttackCooldown());
            }
            
            
         }
      }
      
      if (!playerInRange)
      {
         currentState = EnemyAIState.Chase;
      }
   }

   IEnumerator AttackCooldown()
   {
      Debug.Log("attack cooldown" + _attackCooldown);
      _canAttack = false;
      yield return new WaitForSeconds(_attackCooldown);
      _canAttack = true;
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

