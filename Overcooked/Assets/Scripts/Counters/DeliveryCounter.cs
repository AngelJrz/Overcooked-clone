using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DeliveryCounter : BaseCounter {

    public static DeliveryCounter Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public override void Interact(Player player) {
        if (player.HasKitchenObject() && player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate)) {
            DeliveryManager.instance.DeliverRecipe(plate);
            player.GetKitchenObject().DestroySelf();
        }
    }
}
