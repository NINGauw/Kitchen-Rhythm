using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    [SerializeField]private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int plateSpawnAmount;
    private int spawnPlateAmountMax = 4;

    private void Update(){
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax){
            spawnPlateTimer = 0f;

            if(plateSpawnAmount < spawnPlateAmountMax){
                plateSpawnAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject()){
            //Player dont hold anything
            if(plateSpawnAmount > 0){
                //At least 1 plate here
                plateSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
