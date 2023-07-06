using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

	private ClearCounter clearCounter;

	public ClearCounter ClearCounter {
		get { return clearCounter; }

		set {
            if (clearCounter != null) {
				clearCounter.ClearKitchenObject();
			}

			if (value.HasKitchenObject()) {
				Debug.LogError("Counter already has an Object");
			} else {

				clearCounter = value;
				clearCounter.KitchenObject = this;

				transform.parent = value.GetCounterSpawnPoint();
				transform.localPosition = Vector3.zero;
			}
		}
	}


	public KitchenObjectsSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }
}
