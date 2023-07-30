using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour {
    [SerializeField] StoveCounter stoveCounter;
    private AudioSource audioSource;

    private AudioSource audioSourceWarning;
    private bool playWarningSound;
    private float warningSoundTimer;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStoveStateChange += StoveCounter_OnStoveStateChange;
        stoveCounter.OnProgressAction += StoveCounter_OnProgressAction;
    }

    private void StoveCounter_OnProgressAction(object sender, IHasProses.OnProgressActionEventArgs e) {
        float burnShowProgressAmount = .5f;
        playWarningSound = stoveCounter.IsFried() && e.progress >= burnShowProgressAmount;
    }

    private void StoveCounter_OnStoveStateChange(object sender, StoveCounter.StoveStateChangeArgs e) {
        bool playSound = e.newState == StoveCounter.State.Frying;
        if (playSound) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }
    }

    private void Update() {
        if (playWarningSound) {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f) {
                float warningSoundtimerMax = .2f;
                warningSoundTimer = warningSoundtimerMax;

                SoundManager.instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
