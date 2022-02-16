using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wraith : MonoBehaviour
{
    [SerializeField] public GameObject player;
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
            _direction = transform.position - playerPos;
            _direction = _direction.normalized;
            transform.position -= _direction * speed * Time.deltaTime;
        } 
        //returns to origin point
        if (distance > minDistance) {
            _direction = transform.position - centerPoint.transform.position;
            _direction = _direction.normalized;
            transform.position -= _direction * speed * Time.deltaTime;
            float distanceWraithToCenter = Vector3.Distance(transform.position, centerPoint.transform.position);
            if (distanceWraithToCenter < 0.1f) {
                transform.position = centerPoint.transform.position;
            }
        }
    }
}
