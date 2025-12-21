using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerInputs : MonoBehaviour
{
    public event Action PlaceItem;
    [SerializeField] float interactRange = 1f;

    void OnInteract()
    {
        Tile interactTile = GridManager.grid.GetTileWithWorldPosition(new Vector2(gameObject.transform.position.x + interactRange * Mathf.Sign(gameObject.transform.localScale.x), gameObject.transform.position.y));
        if (interactTile == null)
        {
            return;
        }

        if(interactTile.contents.Count == 0)
        {
            return;
        }

        Interactable interactable = interactTile.contents[0].GetComponent<Interactable>();
        if(interactable == null)
        {
            return;
        }

        interactable.Interact();
    }

    void OnPlaceItem()
    {
        PlaceItem?.Invoke();
    }
}
