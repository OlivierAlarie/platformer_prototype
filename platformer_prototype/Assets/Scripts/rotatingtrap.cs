using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingtrap : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(90 * Time.deltaTime, 0, 0, Space.Self);
    }
}
