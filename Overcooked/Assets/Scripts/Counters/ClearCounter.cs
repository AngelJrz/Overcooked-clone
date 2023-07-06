using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
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


    private void Update() {
        if(testing && Input.GetKeyDown(KeyCode.T)) {
            if(kitchenObject != null) {
                kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }

    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == clearCounter) {
            Show();
        }
        else {
            Hide();
        }
    }

    public void Interact() {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, spawnPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        } else {
            Debug.LogError("Counter already has an Object");
        }
    }

    private void Show() {
        visualGameObject.SetActive(true);
    }

    private void Hide() {
        visualGameObject.SetActive(false);
    }
}
