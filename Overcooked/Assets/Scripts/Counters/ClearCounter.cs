using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            player.GetKitchenObject()?.SetNewParent(this);
        } else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject()?.SetNewParent(player);
            }
        }
    }
}
