using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

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
}
