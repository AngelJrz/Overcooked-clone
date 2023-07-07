using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private GameObject visualGameObject;
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
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

    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == clearCounter) {
            visualGameObject.SetActive(true);
        }
        else {
            visualGameObject.SetActive(false);
        }
    }

    public void Interact(Player player) {
        if (kitchenObject == null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, spawnPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetNewParent(this);
        } else {
            kitchenObject.SetNewParent(player);
        }
    }
}
