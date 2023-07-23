using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashContainer : BaseCounter
{
    public static event EventHandler OnTrash;

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();
            OnTrash?.Invoke(this, EventArgs.Empty);
        }
    }
}
