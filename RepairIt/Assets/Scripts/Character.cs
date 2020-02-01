﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(CharacterController), typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public float baseSpeed = 20.0f;

    public float boostSpeed = 50.0f;

    public float rotationSpeed = 100.0f;

    public float boostCooldown = 2.0f;

    public float boostDuration = 0.2f;

    public float pushPower = 2f;

    private PlayerInput _input;

    private CharacterController _controller;
    private Rigidbody _rb;

    private float _boostTime;

    private float _currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _controller = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        _boostTime = 0.0f;
        _currentSpeed = baseSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_input.GetPlayerButton("Boost") && Time.time >= _boostTime + boostDuration + boostCooldown)
        {
            _boostTime = Time.time;
            _currentSpeed = boostSpeed;
        }

        if (Time.time > _boostTime + boostDuration)
        {
            _currentSpeed = baseSpeed;
        }

        Vector3 verticalAxis = new Vector3(0, 0, 1) * _input.GetPlayerAxis("Vertical");
        Vector3 horizontalAxis = new Vector3(1, 0, 0) * _input.GetPlayerAxis("Horizontal");

        Vector3 translation = verticalAxis + horizontalAxis;
        translation *= _currentSpeed * Time.deltaTime;

        _controller.Move(translation + Vector3.down);

        if (translation.magnitude != 0)
            transform.forward = translation;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic) { return; }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower / body.mass;
    }
}
