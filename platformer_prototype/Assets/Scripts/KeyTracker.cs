using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTracker : MonoBehaviour
{
    [SerializeField] private bool _hasHalfKey = false;
    [SerializeField] private bool _hasFullKey = false;
    [SerializeField] private bool _hasSpecialKey = false;
    [SerializeField] private int _numberOfKeys = 0;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Key") {
            _numberOfKeys++;
            if (_numberOfKeys == 1) {
                _hasHalfKey = true;
            }
            if (_numberOfKeys == 2) {
                _hasFullKey = true;
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "SpecialKey") {
            _hasSpecialKey = true;
            Destroy(other.gameObject);
        }
    }
}
