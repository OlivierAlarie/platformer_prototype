using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWorld : MonoBehaviour
{
    //[SerializeField] private bool _isSwitchingWorld = true;
    [SerializeField] private bool _isInNormalWorld;
    [SerializeField] private bool _isInDarkWorld;

    [SerializeField] public GameObject DarkWorldStuff;
    [SerializeField] public GameObject NormalWorldStuff;

    [SerializeField] public Light mainLight;

    void Awake()
    {
        _isInNormalWorld = true;
        _isInDarkWorld = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && _isInNormalWorld) {
            _isInDarkWorld = true;
            _isInNormalWorld = false;
            }
        else if (Input.GetKeyDown(KeyCode.Q) && _isInDarkWorld) {
            _isInNormalWorld = true;
            _isInDarkWorld = false;
            }
        if (_isInDarkWorld) {
            DarkWorld();
        }
        if (_isInNormalWorld) {
            NormalWorld();
        }
    }

    private void DarkWorld() {
        DarkWorldStuff.SetActive(true);
        NormalWorldStuff.SetActive(false);
        mainLight.color = Color.black;
    }
    private void NormalWorld() {
        NormalWorldStuff.SetActive(true);
        DarkWorldStuff.SetActive(false);
        mainLight.color = Color.white;
    }
}
