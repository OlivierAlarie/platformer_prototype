using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wraith : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public float speed = 5f;

    private Vector3 _direction;

    // Update is called once per frame
    void FixedUpdate()
    {
        _direction = transform.position - player.transform.position;
        _direction = _direction.normalized;
        transform.position -= _direction * speed * Time.deltaTime;
    }
}
