
using System.Collections;
using UnityEngine;

public class ShootingEnemy : Enemy
{
   [SerializeField] private Transform shootingPoint;
   [SerializeField] private GameObject bulletPrefab;
   [SerializeField] private float fireRate = 5;
   private float targetDistance;
   [SerializeField] private float turnSpeed = 10f;
   [SerializeField] private float aimHeight = 1.2f;
   private bool _canShoot = false;
   
   private void Awake()
   {
         _currentTarget = GameManager.playerInstance;
         _canShoot = true; 
         
   }
   
   private void Update ()
   {
       LookAtTarget(); 

       if (_canShoot && IsTargetInDetection())
       {
           ShootOnce();
           StartCoroutine(ShootCooldown());
       }

   }
   private bool IsTargetInDetection()
   {
       if (_currentTarget == null) return false;
       float targetDistance = Vector3.Distance(transform.position, _currentTarget.transform.position);
       return targetDistance <= detectionDistance;
   }

   private void LookAtTarget()
   {
       if (_currentTarget == null) return;
       Vector3 directionTarget = (_currentTarget.transform.position - transform.position);
       directionTarget.y = 0f; //will rotate horizontally 
       if (directionTarget.sqrMagnitude > 0.001f) //Stops the enemy to arrive to 0 
       {
           transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionTarget), turnSpeed * Time.deltaTime);
       }
   }

   private void ShootOnce()
   {
       if (bulletPrefab == null || shootingPoint == null || _currentTarget == null) return;

       Vector3 shootingDistance = (_currentTarget.transform.position + Vector3.up *aimHeight) - shootingPoint.position;
       bulletPrefab = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.LookRotation(shootingDistance));

   }

  IEnumerator ShootCooldown()
   {
       _canShoot = false;
       yield return new WaitForSeconds(fireRate);
       _canShoot = true;
   }
   private void OnDrawGizmosSelected()
   {
       Gizmos.color = Color.cyan;
       Gizmos.DrawWireSphere(transform.position, detectionDistance);
       
       if (shootingPoint != null)
       {
           Gizmos.color = Color.yellow;
           Gizmos.DrawSphere(shootingPoint.position, 0.06f);
       }
       
       if (_currentTarget != null && shootingPoint != null)
       {
           Vector3 aimPoint = _currentTarget.transform.position + Vector3.up * aimHeight;

           Gizmos.color = Color.red;
           Gizmos.DrawLine(shootingPoint.position, aimPoint);
           Gizmos.DrawSphere(aimPoint, 0.06f);
       }
   }
}

