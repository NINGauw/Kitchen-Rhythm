using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public static DeliveryManager Instance { get; private set; }
    [SerializeField]private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;

private void Awake(){
    waitingRecipeSOList = new List<RecipeSO>();
    Instance = this;
}
    private void Update(){
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0){
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < spawnRecipeTimerMax){
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);

                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty); 
            }
            
        }
    }
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject){
        for(int i = 0; i <= waitingRecipeSOList.Count ;i++){
            RecipeSO waitingrecipeSO = waitingRecipeSOList[i];
            if(waitingrecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count){
                //Have same number of material
                bool plateMatchesRecipe = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingrecipeSO.kitchenObjectSOList){
                    //Cycling through all material in waiting recipe
                    bool materialCheck = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()){
                    //Cycling through all material in plate
                    if(plateKitchenObjectSO == recipeKitchenObjectSO){
                        //Material matches
                        materialCheck = true;
                        break;
                    }
                }
                if(!materialCheck){
                    //Plate not in waiting recipe
                    plateMatchesRecipe = false;
                }
                }
                if(plateMatchesRecipe){
                    //Player delivery correct
                    Debug.Log("Correct 1 " + waitingRecipeSOList[i].name);
                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        //No matches found
        //Player didn't delivery correct
        Debug.Log("Player didn't delivery correct");
    }
    public List<RecipeSO> GetWaitingRecipeList(){
        return waitingRecipeSOList;
    }
}
