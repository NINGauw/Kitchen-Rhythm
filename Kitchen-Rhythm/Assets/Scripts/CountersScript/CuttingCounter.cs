using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CuttingCounter : BaseCounter, IProgressBar
{
    public event EventHandler<IProgressBar.OnProgressChangeEventArgs> OnProgressChange;
    [SerializeField]private CuttingRecipeSO[] cuttingRecipesSOArray;
    private float cuttingProgress;
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //No KitchenObject here
            if(player.HasKitchenObject()){
                //Player carry somethings
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress = 0f;
            }
            else{
                //player not carry anything

            }
        }
        else{
            //Has KitchenObject here
            if(!player.HasKitchenObject()){
                //Player not carry somethings
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else{
                //Player hold somethings
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    //Player hold a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                    }   
                }
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject()){
            //Have object here
            if(HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){
               //Ready for cutter
               cuttingProgress++;
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeEventArgs{
                    progressNomalized = (float)cuttingProgress / (float)cuttingRecipeSO.cuttingProgressMax
                });
                if(cuttingProgress >= cuttingRecipeSO.cuttingProgressMax){
                    KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO()); 
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);  
                }
                
                }
            else{
                //Not ready for cutter
            }
            
        }
        else{
            //Not ready for cutter
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipesSOArray){
            if(cuttingRecipeSO.input == kitchenObjectSO){
                return true;
            }
        }
        return false;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipesSOArray){
            if(cuttingRecipeSO.input == inputKitchenSO){
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipesSOArray) {
            if(cuttingRecipeSO.input == inputKitchenSO){
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
