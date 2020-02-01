using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Character : MonoBehaviour
{
    public float baseSpeed = 20.0f;

    public float boostSpeed = 200.0f;

    public float rotationSpeed = 100.0f;

    public float boostCooldown = 2.0f;

    public float boostDuration = 0.5f;

    private PlayerInput _Input;

    private float _boostTime;

    private float _currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _Input = GetComponent<PlayerInput>();
        _boostTime = 0.0f;
        _currentSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (_Input.GetPlayerButton("Boost") && Time.time >= _boostTime + boostDuration + boostCooldown)
        {
            _boostTime = Time.time;
            _currentSpeed = boostSpeed;
        }

        if (Time.time > _boostTime + boostDuration)
        {
            _currentSpeed = baseSpeed;
        }

        Vector3 verticalAxis = new Vector3(0, 0, 1) * _Input.GetPlayerAxis("Vertical");
        Vector3 horizontalAxis = new Vector3(1, 0, 0) * _Input.GetPlayerAxis("Horizontal");

        Vector3 translation = verticalAxis + horizontalAxis;
        translation *= _currentSpeed * Time.deltaTime;

        transform.Translate(translation, Space.World);

        if (translation.magnitude != 0)
            transform.forward = translation;
    }
}
