using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] KitchenObjectsSO plateKitchenObject;

    public event EventHandler OnSpawnPlate;
    public event EventHandler OnPlateRemoved;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 3f;
    private int platesSpawnedAmount = 0;
    private int platesSpawnedAmountMax = 4;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax ) {
            spawnPlateTimer = 0f;

            if (platesSpawnedAmount < platesSpawnedAmountMax) {
                OnSpawnPlate?.Invoke(this, EventArgs.Empty);
                platesSpawnedAmount++;
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            if (platesSpawnedAmount > 0) {

                KitchenObject.SpawnKitchenObject(plateKitchenObject, player);
                platesSpawnedAmount--;
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
