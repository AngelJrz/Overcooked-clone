using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    [SerializeField] private SoundsReferenceSO soundsCollection;

    private void Awake() {
        instance = this;
    }

    public void Start() {
        DeliveryManager.instance.OnRecipeSuccess += Instance_OnRecipeSuccess;
        DeliveryManager.instance.OnRecipeFailed += Instance_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickObject += Player_OnPickObject;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        TrashContainer.OnTrash += TrashContainer_OnTrash;
    }

    private void TrashContainer_OnTrash(object sender, System.EventArgs e) {
        TrashContainer container = sender as TrashContainer;
        PlaySound(soundsCollection.trash, container.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaced(object sender, System.EventArgs e) {
        BaseCounter counter = sender as BaseCounter;
        PlaySound(soundsCollection.objectPickup, counter.transform.position);
    }

    private void Player_OnPickObject(object sender, System.EventArgs e) {
        PlaySound(soundsCollection.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
        CuttingCounter counter = sender as CuttingCounter;
        PlaySound(soundsCollection.chop, counter.transform.position);
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(soundsCollection.deliveryFail, deliveryCounter.transform.position);
    }

    private void Instance_OnRecipeSuccess(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(soundsCollection.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootsteps(Vector3 position, float volume) {
        PlaySound(soundsCollection.footstep, position, volume);
    }
}
