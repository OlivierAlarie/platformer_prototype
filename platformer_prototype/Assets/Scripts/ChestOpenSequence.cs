using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestOpenSequence : MonoBehaviour
{
    public KeyTracker keyTracker;
    public Animator chestAnimator;
    public float numberOfKeysNeeded = 4f;
    public GameObject timerBar;
    public GameObject player;

    [Header("Cameras")]
    public GameObject mainCam;
    public GameObject newParent;


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && keyTracker._numberOfKeys == numberOfKeysNeeded && !keyTracker._hasSpecialKey) {
            chestAnimator.SetBool("hasFullKey", true);
            timerBar.SetActive(false);
            mainCam.transform.parent = newParent.transform;
            mainCam.transform.localPosition = Vector3.zero;
            player.SetActive(false);
        }
        if (other.gameObject.tag == "Player" && keyTracker._numberOfKeys == numberOfKeysNeeded && keyTracker._hasSpecialKey) {
            chestAnimator.SetBool("hasSpecialKey", true);
            timerBar.SetActive(false);
            mainCam.transform.parent = newParent.transform;
            mainCam.transform.localPosition = Vector3.zero;
            player.SetActive(false);
        }
    }
}
