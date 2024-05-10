using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs{
        public KitchenObjectSO kitchenObjectSO;
    }
    [SerializeField]private List<KitchenObjectSO> validKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;
    private void Awake(){
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO){
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO)){
            //kitchenObject not valid
            return false;
        }
        if(kitchenObjectSOList.Contains(kitchenObjectSO)){
            //Already has this type
            return false;
        }
        else{
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs{
                kitchenObjectSO = kitchenObjectSO,
            });
            return true;
        }
        
    }
}