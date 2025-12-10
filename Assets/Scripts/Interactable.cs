using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] UnityEvent interact;

    public void Interact()
    {
        Debug.Log("yo3");
        interact?.Invoke();
    }
}
