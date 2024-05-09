using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    public event EventHandler<OnProgressChangeEventArgs> OnProgressChange;
    public class OnProgressChangeEventArgs : EventArgs{
        public float progressNomalized;
    }
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
                OnProgressChange?.Invoke(this, new OnProgressChangeEventArgs{
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
