using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour
{
    public event Action InputPickup;

    void OnPickup()
    {
        InputPickup?.Invoke();
    }
}
