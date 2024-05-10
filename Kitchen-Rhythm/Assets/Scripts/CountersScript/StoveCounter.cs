using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IProgressBar
{
    public event EventHandler<IProgressBar.OnProgressChangeEventArgs> OnProgressChange;
    private enum State{
        Raw,
        Cooking,
        Cooked,
        OverCooked,
    }
    [SerializeField]private StoveRecipeSO[] stoveRecipeSOArray;
    private State state;
    private float cookTimer;
    private float overcookedTimer;
    private StoveRecipeSO stoveRecipeSO;

    private void Update(){
        if(HasKitchenObject()){
            switch (state){
                case State.Raw:
                break;
                case State.Cooking:
                cookTimer += Time.deltaTime;
                OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeEventArgs{
                        progressNomalized = cookTimer/stoveRecipeSO.fryingTimeMax,
                    });
                    if(cookTimer > stoveRecipeSO.fryingTimeMax){
                        //Cooked
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(stoveRecipeSO.output, this);
                        overcookedTimer = 0f;
                        state = State.Cooked;
                    }
                break;
                case State.Cooked:
                    overcookedTimer += Time.deltaTime;
                    OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeEventArgs{
                        progressNomalized = overcookedTimer/stoveRecipeSO.fryingTimeMax,
                    });
                    StoveRecipeSO overcookedRecipeSO = GetStoveRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        if(overcookedTimer > overcookedRecipeSO.fryingTimeMax){
                            //Cooked
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(overcookedRecipeSO.output, this);
                            state = State.OverCooked;
                        }
                break;
                case State.OverCooked:
                OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeEventArgs{
                        progressNomalized = 0f,
                    });
                break;
            }
        }
    }
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //No KitchenObject here
            if(player.HasKitchenObject()){
                //Player carry somethings
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    //Player carry something can be cook
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    stoveRecipeSO = GetStoveRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Cooking;
                    cookTimer = 0f;
                    OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeEventArgs{
                        progressNomalized = cookTimer/stoveRecipeSO.fryingTimeMax,
                    }); 
                } 
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
                OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeEventArgs{
                        progressNomalized = 0f,
                    });
            }
            else{
                //Player hold somethings
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    //Player hold a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeEventArgs{
                        progressNomalized = 0f,
                    });
                        GetKitchenObject().DestroySelf();
                    }   
                }
            }
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO){
        foreach(StoveRecipeSO stoveRecipeSO in stoveRecipeSOArray){
            if(stoveRecipeSO.input == kitchenObjectSO){
                return true;
            }
        }
        return false;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenSO){
        foreach(StoveRecipeSO stoveRecipeSO in stoveRecipeSOArray){
            if(stoveRecipeSO.input == inputKitchenSO){
                return stoveRecipeSO.output;
            }
        }
        return null;
    }
    private StoveRecipeSO GetStoveRecipeSOWithInput(KitchenObjectSO inputKitchenSO){
        foreach(StoveRecipeSO stoveRecipeSO in stoveRecipeSOArray) {
            if(stoveRecipeSO.input == inputKitchenSO){
                return stoveRecipeSO;
            }
        }
        return null;
    }
}
