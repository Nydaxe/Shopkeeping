using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D ridgidbody2D;
    [SerializeField] float moveSpeed;
    Vector2 moveInput;

    void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        ridgidbody2D.linearVelocity = moveInput * moveSpeed * Time.deltaTime * 50;
    }
}