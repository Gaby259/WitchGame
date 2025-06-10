using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
   private InputController _inputController;

   private void Awake()
   {
       _inputController = GetComponent<InputController>();
   }

   void Start()
   {
       _inputController.AttackEvent += FireWeapon;
       _inputController.AttackEventCancelled += StopFireWeapon; 
   }
   void FireWeapon()
   {
       Debug.Log("Firing the weapon ");
   }

   void StopFireWeapon ()
   {
       Debug.Log("Stopped Firing");
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
