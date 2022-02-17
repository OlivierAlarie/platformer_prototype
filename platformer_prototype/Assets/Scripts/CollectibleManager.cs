using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    public TextMeshProUGUI rubisCount;
    public KeyTracker keyTracker;

    void Update()
    {
        rubisCount.text = keyTracker.numberOfRubis.ToString("0");
    }
}
