using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] KitchenObjectsSO cuttingObject;
    
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetNewParent(this);
            }
        } else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetNewParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cuttingObject, this);
        }
    }
}
