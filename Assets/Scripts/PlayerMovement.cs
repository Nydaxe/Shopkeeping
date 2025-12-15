using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;

    void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Tile newTile = GridManager.grid.GetTileWithWorldPosition((Vector2)transform.position + moveInput);

        if(moveInput.x != 0)
        {
            transform.localScale = new Vector3(moveInput.x , 1, 1);
        }

        if(newTile.IsOccupied())
        {
            return;
        }

        transform.position = newTile.centerPosition;
    }
}