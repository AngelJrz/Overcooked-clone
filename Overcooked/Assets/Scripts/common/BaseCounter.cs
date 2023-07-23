using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform spawnPoint;
    private KitchenObject kitchenObject;
    public static event EventHandler OnAnyObjectPlaced;

    public virtual void Interact(Player player) {
        Debug.Log("Interact();");
    }

    public virtual void InteractAlternate(Player player) {
        Debug.Log("InteractAlternate();");
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null ) {
            OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return this.kitchenObject;
    }

    public void ClearKitchenObject() {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return this.kitchenObject != null;
    }

    public Transform GetCounterSpawnPoint() {
        return spawnPoint;
    }
}
