using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingBarUI : MonoBehaviour {
    [SerializeField] private StoveCounter stoveCounter;
    private Animator animator;
    private string IS_FLASHING = "IsFlashing";

    private void Start() {
        animator = GetComponent<Animator>();
        stoveCounter.OnProgressAction += StoveCounter_OnProgressAction;
    }

    private void StoveCounter_OnProgressAction(object sender, IHasProses.OnProgressActionEventArgs e) {
        float burnShowProgressAmount = .5f;
        bool isFlashing = stoveCounter.IsFried() && e.progress >= burnShowProgressAmount;

        if (isFlashing) {
            animator.SetBool(IS_FLASHING, true);
        } else {
            animator.SetBool(IS_FLASHING, false);
        }
    }
}
