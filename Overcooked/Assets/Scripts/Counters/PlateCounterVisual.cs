using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform platePrefab;

    private List<GameObject> plateVisualGameObjectsList;

    private void Awake() {
        plateVisualGameObjectsList = new List<GameObject>();
    }

    private void Start() {
        plateCounter.OnSpawnPlate += PlateCounter_OnSpawnPlate;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateRemoved(object sender, System.EventArgs e) {
        GameObject plateGameObject = plateVisualGameObjectsList[plateVisualGameObjectsList.Count - 1];
        plateVisualGameObjectsList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlateCounter_OnSpawnPlate(object sender, System.EventArgs e) {
        Transform plateTransform = Instantiate(platePrefab, spawnPoint);

        float plateOffSet = .1f;
        plateTransform.localPosition = new Vector3(0, plateOffSet * plateVisualGameObjectsList.Count, 0);

        plateVisualGameObjectsList.Add(plateTransform.gameObject);
    }
}
