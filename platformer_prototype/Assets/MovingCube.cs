using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public BasicPlayerController bpc;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position+ Vector3.back * Time.fixedDeltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "PlayerCharacter")
        {
            bpc.KnockBack(-1*collision.GetContact(0).normal);
        }
    }
}
