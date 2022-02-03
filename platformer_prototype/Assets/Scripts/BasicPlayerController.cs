using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Transform _cameraRoot;
    [SerializeField]
    private float _cameraSensitivityX = 360;
    [SerializeField]
    private float _cameraSensitivityY = 360;
    [SerializeField]
    private float _maxDownwardAngle = 20;
    [SerializeField]
    private float _maxUpwardAngle = -60;


    private float _targetRotationH = 0;
    private float _targetRotationV = 0;

    [Header("Character")]
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private float _playerSpeed = 5;

    private Vector3 _targetMotion;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera.transform.LookAt(_cameraRoot);
    }

    // Update is called once per frame
    void Update()
    {
        InputsCheck();
        CameraCheck();
        MovePlayer();
    }

    void InputsCheck()
    {
        //Horizontal Movement
        _targetMotion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Vertical Movement
        if(_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    void CameraCheck()
    {
        //Calculate Camera Rotation based of mouse movement

        _targetRotationH += Input.GetAxis("Mouse X") * _cameraSensitivityX * Time.deltaTime;
        _targetRotationV += Input.GetAxis("Mouse Y") * _cameraSensitivityY * Time.deltaTime;

        //Clamp Vertical Rotation
        _targetRotationV = Mathf.Clamp(_targetRotationV, _maxUpwardAngle, _maxDownwardAngle);

        _cameraRoot.transform.rotation = Quaternion.Euler(_targetRotationV, _targetRotationH, 0.0f);
    }

    void MovePlayer()
    {
        //Use _cameraRoots rotation, making forward always in front of the camera
        _targetMotion = Quaternion.Euler(0.0f, _cameraRoot.rotation.eulerAngles.y, 0.0f) * _targetMotion;

        _characterController.Move(_targetMotion * _playerSpeed * Time.deltaTime);
    }
}
