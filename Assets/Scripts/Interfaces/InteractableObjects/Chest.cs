using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        SoundManager.Play("Chest");
        Debug.Log("Interact");
        Destroy(gameObject);
    }
}
