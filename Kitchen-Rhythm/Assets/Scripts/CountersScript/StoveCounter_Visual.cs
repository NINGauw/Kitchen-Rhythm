using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter_Visual : MonoBehaviour
{
    [SerializeField]private StoveCounter stoveCounter;
    [SerializeField]private GameObject stoveOnGameObject;
    [SerializeField]private GameObject particleOnGameObject;

    private void Start(){
        Hide();
    }
    private void Update(){
        if(stoveCounter.HasKitchenObject()){
            Show();
        }
        else{
            Hide();
        }
    }
    private void Show(){
        stoveOnGameObject.SetActive(true);
        particleOnGameObject.SetActive(true);
    }
    private void Hide(){
        stoveOnGameObject.SetActive(false);
        particleOnGameObject.SetActive(false);
    }
}
