using System;
using Unity.VisualScripting;
using UnityEngine;

public class Ammo :MonoBehaviour
{
  [SerializeField] private Weapon WeaponType;
  
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      WeaponType.RefillAmmo();
      Destroy(this.gameObject);
     
    }
  }
  
}
