using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestOpenSequence : MonoBehaviour
{
    public KeyTracker keyTracker;
    public Animator chestAnimator;
    public float numberOfKeysNeeded = 4f;
    public GameObject timerBar;
    public GameObject player;
    public AudioSource chestsound;
    public TextMeshProUGUI textdefin;

    [Header("Cameras")]
    public GameObject mainCam;
    public GameObject newParent;

    private void Start()
    {
        textdefin.text = "Congratulation on finishing the game 100%\n\nThere are no more secrets for you to find.\n\nExcept MAYBE the little green guy";
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && keyTracker._numberOfKeys == numberOfKeysNeeded && keyTracker._hasSpecialKey) {
            chestAnimator.SetBool("hasSpecialKey", true);
            timerBar.SetActive(false);
            chestsound.Play();
            mainCam.transform.parent = newParent.transform;
            mainCam.transform.localPosition = Vector3.zero;
            mainCam.transform.localRotation = Quaternion.identity;
            player.SetActive(false);
            if (keyTracker.numberOfRubis == 100)
            {
                textdefin.text = "Congratulation on finishing the game 100%\n\nThere are no more secrets for you to find.\n\nExcept MAYBE the little green guy";
            }
            if (keyTracker.numberOfRubis < 100)
            {
                textdefin.text = "Congratulation on finishing the game\n\nand finding the hidden key now all that's left";
            }
        }
        if (other.gameObject.tag == "Player" && keyTracker._numberOfKeys == numberOfKeysNeeded && !keyTracker._hasSpecialKey)
        {
            chestAnimator.SetBool("hasFullKey", true);
            timerBar.SetActive(false);
            chestsound.Play();
            mainCam.transform.parent = newParent.transform;
            mainCam.transform.localPosition = Vector3.zero;
            mainCam.transform.localRotation = Quaternion.identity;
            player.SetActive(false);
        }
    }
}
