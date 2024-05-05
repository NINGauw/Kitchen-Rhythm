using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private bool isWalking;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private float movingSpeed = 2f;
    [SerializeField]private float rotateSpeed = 10f;

    void Update()
    {
        Vector2 InputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(InputVector.x, 0, InputVector.y);
        isWalking = moveDir != Vector3.zero;
        transform.position += moveDir * movingSpeed * Time.deltaTime;
        transform.forward = Vector3.Lerp(transform.forward, moveDir, rotateSpeed*Time.deltaTime);
    }

    public bool IsWalking(){
        return isWalking;
    }
}
