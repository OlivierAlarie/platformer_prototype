using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchWorld : MonoBehaviour
{
    //[SerializeField] private bool _isSwitchingWorld = true;
    [SerializeField] private bool _isInNormalWorld;
    [SerializeField] private bool _isInDarkWorld;

    [SerializeField] public GameObject DarkWorldStuff;
    [SerializeField] public GameObject NormalWorldStuff;

    [SerializeField] public Light mainLight;

    //TIMER IN DARK WORLD
    [SerializeField] public Image _timerBar;
    [SerializeField] public float maxTime = 10f;
    [SerializeField] public float refreshRate = 2.0f;

    [Header("PAS TOUCHE")]
    [SerializeField] public float _timeLeft;


    void Awake()
    {
        _isInNormalWorld = true;
        _isInDarkWorld = false;
        _timeLeft = maxTime;
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
            if (_timeLeft > 0) {
                _timeLeft -= Time.deltaTime;
                _timerBar.fillAmount = _timeLeft / maxTime;
            }
        }
        if (_isInNormalWorld) {
            NormalWorld();
            if (_timeLeft < maxTime) {
                _timeLeft += refreshRate * Time.deltaTime;
                _timerBar.fillAmount = _timeLeft / maxTime;
            }
            
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
