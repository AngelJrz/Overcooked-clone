using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {
    public override void Interact(Player player) {
        if (player.HasKitchenObject() && player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate)) {
            DeliveryManager.instance.DeliverRecipe(plate);
            player.GetKitchenObject().DestroySelf();
        }
    }
}
