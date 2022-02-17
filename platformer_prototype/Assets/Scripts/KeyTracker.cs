using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTracker : MonoBehaviour
{
    [SerializeField] public bool _hasSpecialKey = false;
    [SerializeField] public int _numberOfKeys = 0;
    [SerializeField] public int numberOfRubis = 0;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Key") {
            _numberOfKeys++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "SpecialKey") {
            _hasSpecialKey = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Rubis") {
            numberOfRubis++;
            Destroy(other.gameObject);
        }

    }
}
