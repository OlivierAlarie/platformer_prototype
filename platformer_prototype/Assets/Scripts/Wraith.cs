using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wraith : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] public BasicPlayerController player;
    [SerializeField] public GameObject centerPoint;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minDistance = 10f;

    private Vector3 _direction;


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPos = player.transform.position + Vector3.up;
        float distance = Vector3.Distance(playerPos, centerPoint.transform.position);


        //moves towards player
        if (distance <= minDistance) {
            _direction = playerPos - rb.position;
            _direction = _direction.normalized;
            rb.velocity = _direction * speed;
        } 
        //returns to origin point
        if (distance > minDistance) {
            _direction = centerPoint.transform.position - rb.position;
            _direction = _direction.normalized;
            rb.velocity = _direction * speed;
            float distanceWraithToCenter = Vector3.Distance(transform.position, centerPoint.transform.position);
            if (distanceWraithToCenter < 0.1f) {
                rb.velocity = Vector3.zero;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.name == "PlayerCharacter")
        {
            player.KnockBack(-1 * collision.GetContact(0).normal);
        }
    }

}
