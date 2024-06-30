using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManagerUI : MonoBehaviour
{
    [SerializeField]private Transform container;
    [SerializeField]private Transform recipeTemplate;

    private void Awake(){
        recipeTemplate.gameObject.SetActive(false);
    }
    private void Start(){
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual(){
        foreach(Transform child in container){
            if(child == recipeTemplate){continue;}
            Destroy(child.gameObject);
        }
        foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeList()){
            Transform recipeTranform = Instantiate(recipeTemplate, container);
            recipeTranform.gameObject.SetActive(true);
            recipeTranform.GetComponent<RecipeManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
