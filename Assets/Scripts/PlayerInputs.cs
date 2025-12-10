using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerInputs : MonoBehaviour
{
    public event Action PlaceItem;
    [SerializeField] float interactRange = 1f;

    void OnInteract()
    {
        Debug.Log("hi");
        Tile interactTile = GridManager.grid.GetTileWithWorldPosition(new Vector2(gameObject.transform.position.x + interactRange * Mathf.Sign(gameObject.transform.localScale.x), gameObject.transform.position.y));
        if (interactTile == null || !interactTile.IsOccupied())
        {
            return;
        }
        Debug.Log("yo");
        Interactable interactable = interactTile.contents[0].GetComponent<Interactable>();
        if(interactable == null)
        {
            return;
        }
        Debug.Log("yo2");
        interactable.Interact();
    }

    void OnPlaceItem()
    {
        PlaceItem?.Invoke();
    }
}
