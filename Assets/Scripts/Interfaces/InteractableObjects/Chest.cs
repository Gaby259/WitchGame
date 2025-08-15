using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interact");
        Destroy(gameObject);
    }
}
