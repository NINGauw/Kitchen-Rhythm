using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    public void Interact(){
        Debug.Log("Interact!");
        Instantiate(kitchenObjectSO.prefab, counterTopPoint);
    }
}
