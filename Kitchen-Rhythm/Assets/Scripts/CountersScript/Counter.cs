using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : BaseCounter, IKitchenObjectParent
{
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //No KitchenObject here
            if(player.HasKitchenObject()){
                //Player carry somethings
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
                //Player Carry something
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    //Player hold a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                    }   
                }else{
                    //Player carry something else not plate
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                        //Counter have a plate
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())){
                            //Player hold valid kitchenObject to put on
                            player.GetKitchenObject().DestroySelf();
                        };

                    }
                }
            }
        }
    }
}
