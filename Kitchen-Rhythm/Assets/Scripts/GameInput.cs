using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputAction playerInputAction;
    private void Awake(){
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized(){
        Vector2 InputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        InputVector = InputVector.normalized;
        return InputVector;
    }
}
