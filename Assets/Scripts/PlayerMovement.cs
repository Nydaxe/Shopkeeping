using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] AudioClip walkingAudio;
    [SerializeField] float volume;
    [SerializeField] float maxPitch;
    [SerializeField] float minPitch;

    void OnMovement(InputValue value)
    {
        if(Time.timeScale == 0)
            return;
            
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

        AudioManager.instance.PlaySoundEffect(walkingAudio, volume, minPitch, maxPitch);
        transform.position = newTile.centerPosition;
    }
}