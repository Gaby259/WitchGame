using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] Transform doorPivot;
    [SerializeField] float doorOpenSpeed = 40f;
    [SerializeField] private float doorRotation = -90f;
    public void Interact()
    {
        SoundManager.Play("Door");
       OpenDoor();
    }

    private void OpenDoor()
    {
        float totalRotation = 0; 
        while(totalRotation < doorRotation)
        {
            transform.RotateAround(doorPivot.position, Vector3.up, doorOpenSpeed * Time.deltaTime);
            totalRotation += doorOpenSpeed * Time.deltaTime;
        } 
    }
}
