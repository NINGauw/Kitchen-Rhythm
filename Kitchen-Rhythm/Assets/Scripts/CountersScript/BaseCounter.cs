using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    public virtual void Interact(Player player){
        Debug.Log("BaseCounter.Interact();");
    }
    public virtual void InteractAlternate(Player player){
        Debug.Log("BaseCounter.Interact();");
    }
    public Transform GetKitchenObjectFollowParent(){
        return counterTopPoint;
    }
    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
    }
    public void ClearKitchenObject(){
        this.kitchenObject = null;
    }
    public bool HasKitchenObject(){
        return this.kitchenObject != null;
    }  
}
