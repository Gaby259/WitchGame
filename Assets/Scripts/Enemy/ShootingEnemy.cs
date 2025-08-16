
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class ShootingEnemy : Enemy
{
   [SerializeField] private Transform shootingPoint;
   [SerializeField] private GameObject bulletPrefab;
   [SerializeField] private float turnSpeed = 10f;
   [SerializeField] private float aimHeight = 1.2f;
   [SerializeField] private int burstCount = 3;
   [SerializeField] private float burstInterval = 0.12f;
   private bool _canShoot = false;
   [SerializeField] private float lastShot= 1;
   [SerializeField] private float timeForShoot= 1;
   
   private void Awake()
   {
         _currentTarget = GameManager.playerInstance;
         _canShoot = false; 
         
   }
   
   private void Update()
   {
       LookAtTarget();
       // Shoot after time has passed 
       bool timeComplete = Time.time >= lastShot + timeForShoot;
       if (_canShoot && timeComplete )
       {
           lastShot = Time.time;
           StartCoroutine(FireBurst());
       }
   }

   private IEnumerator FireBurst()
   {
       for (int i = 0; i < burstCount; i++)
       {
           Fire();
           yield return new WaitForSeconds(burstInterval);
       }
   }

   private void Fire()
   {
       if (_canShoot)
       {
           Vector3 direction = (_currentTarget.transform.position + Vector3.up * aimHeight) - shootingPoint.position;
           Instantiate(bulletPrefab, shootingPoint.position, Quaternion.LookRotation(direction));

       }
   }

   private void LookAtTarget()
   {
       Vector3 targetPosition = new Vector3(_currentTarget.transform.position.x, transform.position.y, _currentTarget.transform.position.z);

       float playerDistance = Vector3.Distance(transform.position, targetPosition);

       _canShoot = playerDistance <= detectionDistance;
       Vector3 lookDirection = targetPosition - transform.position;
       float enemyRotation = turnSpeed * Time.deltaTime;
       Vector3 newLookDirection = Vector3.RotateTowards(transform.forward, lookDirection, enemyRotation, 0f);
       transform.rotation = Quaternion.LookRotation(newLookDirection);//updates the look of the enemy towards the player
   }
   
}