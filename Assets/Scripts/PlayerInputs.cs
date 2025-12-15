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
            Debug.Log("tile invalid");
            return;
        }

        if(interactTile.contents.Count == 0)
        {
            Debug.Log("tile empty");
            return;
        }

        Interactable interactable = interactTile.contents[0].GetComponent<Interactable>();
        Debug.Log(interactTile.contents[0].name);
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
