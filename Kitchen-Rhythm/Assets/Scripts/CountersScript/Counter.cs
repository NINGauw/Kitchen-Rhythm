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
        }
    }
}
