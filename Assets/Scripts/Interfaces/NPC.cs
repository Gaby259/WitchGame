using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Hi! you need to escape!!");
    }
}
