using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class MovingPlatform : MonoBehaviour
{
  [SerializeField]private Transform[] waypoints;
  [SerializeField]private float platformSpeed;
  private int _currentWaypointIndex = 0;
  private PlayerController _player;
  private SphereCollider _collider;

  private void Start()
  {
   //  _player =GameManager.playerInstance.GetComponent<PlayerController>();
     _collider = GameManager.playerInstance.GetComponent<SphereCollider>();
     
  }

  private void Update()
  {
      MovePlatform();
  }

  private void MovePlatform()
  {
      if (Vector3.Distance(transform.position, waypoints[_currentWaypointIndex].position) < 0.1f)
      {
          _currentWaypointIndex++;
          if (_currentWaypointIndex >= waypoints.Length)
          {
              _currentWaypointIndex = 0;
          }
      }
      transform.position = Vector3.MoveTowards(transform.position, 
          waypoints[_currentWaypointIndex].position, Time.deltaTime * platformSpeed);
  }

  private void OnCollisionEnter(Collision collision )
  {
      if (collision.gameObject.CompareTag("Player"))
      {
          collision.gameObject.transform.SetParent(transform); //Move the player with the platform
          _player.enabled = false;
          
      }
  }

  private void OnCollisionExit(Collision collision)
  {
      if (collision.gameObject.CompareTag("Player"))
      {
          _player.enabled = true;
          collision.gameObject.transform.SetParent(null);
      }
  }
}
