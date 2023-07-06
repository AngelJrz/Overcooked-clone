using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

	private ClearCounter currentClearCounter;

	public ClearCounter GetClearCounter() {
		return currentClearCounter;
	}

	public void SetClearCounter(ClearCounter newClearCounter) {
        if (this.currentClearCounter != null) {
            this.currentClearCounter.ClearKitchenObject();
        }

        if (newClearCounter.HasKitchenObject()) {
            Debug.LogError("Counter already has an Object");
        } else {

            this.currentClearCounter = newClearCounter;
            this.currentClearCounter.SetKitchenObject(this);

            transform.parent = currentClearCounter.GetCounterSpawnPoint();
            transform.localPosition = Vector3.zero;
        }
    }


	public KitchenObjectsSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }
}
