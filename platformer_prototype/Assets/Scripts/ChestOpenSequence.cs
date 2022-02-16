using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpenSequence : MonoBehaviour
{
    public KeyTracker keyTracker;
    public Camera mainCam;
    public Camera endSequenceCam;
    public Animator chestAnimator;
    public float numberOfKeysNeeded = 4f;
    

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && keyTracker._numberOfKeys == numberOfKeysNeeded && !keyTracker._hasSpecialKey) {
            //mainCam.enabled = false;
            //endSequenceCam.enabled = true;
            chestAnimator.SetBool("hasFullKey", true);
        }
        if (other.gameObject.tag == "Player" && keyTracker._numberOfKeys == numberOfKeysNeeded && keyTracker._hasSpecialKey) {
            //mainCam.enabled = false;
            //endSequenceCam.enabled = true;
            chestAnimator.SetBool("hasSpecialKey", true);
        }
    }
}
