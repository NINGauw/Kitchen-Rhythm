using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance{get ; private set;} 
    public event EventHandler<OnSelectedCounterChangedEventArgs>OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs: EventArgs{
        public BaseCounter selectedCounter;
    }
    [SerializeField]private GameInput gameInput;
    [SerializeField]private float movingSpeed = 2f;
    [SerializeField]private float rotateSpeed = 10f;
    [SerializeField]private LayerMask countersLayer;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private KitchenObject kitchenObject;
    private bool isWalking;
    private Vector3 lastInteraction;
    private BaseCounter selectedCounter;
    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }
    private void Awake(){
        if(Instance != null){
            Debug.Log("Already have Instance!!");
        }
        Instance = this;
    }
    private void Start(){
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if(selectedCounter != null){
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(selectedCounter != null){
            selectedCounter.Interact(this);
        }
    }
    private void HandleInteraction(){
        Vector2 InputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(InputVector.x, 0, InputVector.y);
        if(moveDir != Vector3.zero){
            lastInteraction = moveDir;
        }
        float interactDistance = 2f;
        //Thu thập thêm thông tin của vật va chạm
        if(Physics.Raycast(transform.position, lastInteraction, out RaycastHit raycastHit, interactDistance, countersLayer)){
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)){
                if(baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }

            } else{
                SetSelectedCounter(null);
            } 
            
        } else{
            SetSelectedCounter(null);
        } 
        Debug.Log(selectedCounter);
    }
    private void HandleMovement()
    {
        Vector2 InputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(InputVector.x, 0, InputVector.y);
        isWalking = moveDir != Vector3.zero;
        transform.position += moveDir * movingSpeed * Time.deltaTime;
        transform.forward = Vector3.Lerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }
    public bool IsWalking(){
        return isWalking;
    }
    private void SetSelectedCounter(BaseCounter counter)
    {
        selectedCounter = counter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter,
        });
    }
    
    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
    }
    public void ClearKitchenObject(){
        kitchenObject = null;
    }
    public bool HasKitchenObject(){
        return kitchenObject != null;
    }

    public Transform GetKitchenObjectFollowParent()
    {
        return kitchenObjectHoldPoint;
    }
}
