using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
public class Character : MonoBehaviour
{
    public float baseSpeed = 20.0f;

    public float boostSpeed = 50.0f;

    public float rotationSpeed = 100.0f;

    public float boostCooldown = 2.0f;

    public float boostDuration = 0.2f;

    private PlayerInput _input;

    private CharacterController _controller;

    private float _boostTime;

    private float _currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _controller = GetComponent<CharacterController>();
        _boostTime = 0.0f;
        _currentSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.GetPlayerButton("Boost") && Time.time >= _boostTime + boostDuration + boostCooldown)
        {
            _boostTime = Time.time;
            _currentSpeed = boostSpeed;
        }

        if (Time.time > _boostTime + boostDuration)
        {
            _currentSpeed = baseSpeed;
        } else 
        {
            Debug.Log("IN COOLDOWN");
        }

        if (_currentSpeed == boostSpeed)
        {
            Debug.Log("BOOSTED");
        }

        Vector3 verticalAxis = new Vector3(0, 0, 1) * _input.GetPlayerAxis("Vertical");
        Vector3 horizontalAxis = new Vector3(1, 0, 0) * _input.GetPlayerAxis("Horizontal");

        Vector3 translation = verticalAxis + horizontalAxis;
        translation *= _currentSpeed * Time.deltaTime;

        _controller.Move(translation);

        if (translation.magnitude != 0)
            transform.forward = translation;
    }
}
