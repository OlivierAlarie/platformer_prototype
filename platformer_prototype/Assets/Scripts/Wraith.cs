using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wraith : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minDistance = 10f;

    private Vector3 _direction;

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= minDistance) {
            _direction = transform.position - player.transform.position;
            _direction = _direction.normalized;
            transform.position -= _direction * speed * Time.deltaTime;
        }
    }
}
