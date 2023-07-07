using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

	private IKitchenObjectParent kitchenObjectParent;

	public IKitchenObjectParent GetKitchenObjectParent() {
		return kitchenObjectParent;
	}

	public void SetNewParent(IKitchenObjectParent newKitchenObjectParent) {
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        if (newKitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("Counter already has an Object");
        } else {

            this.kitchenObjectParent = newKitchenObjectParent;
            this.kitchenObjectParent.SetKitchenObject(this);

            transform.parent = kitchenObjectParent.GetCounterSpawnPoint();
            transform.localPosition = Vector3.zero;
        }
    }


	public KitchenObjectsSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }
}
