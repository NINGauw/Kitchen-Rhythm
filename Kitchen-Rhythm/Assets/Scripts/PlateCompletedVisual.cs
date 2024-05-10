using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateCompletedVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject{
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
        public GameObject Icon;
        

    }
    [SerializeField]private PlateKitchenObject plateKitchenObject;
    [SerializeField]private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectsList;
    private void Start(){
        plateKitchenObject.OnIngredientAdded += plateKitchenObject_OnIngredientAdded;
        foreach(KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectsList){
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
            kitchenObjectSO_GameObject.Icon.SetActive(false);
        }
    }

    private void plateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach(KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectsList){
            if(kitchenObjectSO_GameObject.kitchenObjectSO == e.kitchenObjectSO){
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
                kitchenObjectSO_GameObject.Icon.SetActive(true);
            }
        }
        
    }
}
