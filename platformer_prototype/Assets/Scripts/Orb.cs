using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField] private float addTimeValue = 2f;

    [SerializeField] private SwitchWorld _switchWorld;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            _switchWorld._timeLeft += addTimeValue;
            Destroy(this.gameObject);
        }
    }
}
