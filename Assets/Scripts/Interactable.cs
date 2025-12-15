using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] UnityEvent interact;

    public void Interact()
    {
        interact?.Invoke();
    }
}
