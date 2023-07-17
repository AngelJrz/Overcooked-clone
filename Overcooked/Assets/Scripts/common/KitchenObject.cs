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

    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectsSO kitchenObjectSO, IKitchenObjectParent parent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetNewParent(parent);

        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if (this is PlateKitchenObject) {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        } else {
            plateKitchenObject = null;
            return false;
        }
    }
}
