using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Character : MonoBehaviour
{
    public float speed = 20.0f;
    public float rotationSpeed = 100.0f;

    private PlayerInput _Input;

    // Start is called before the first frame update
    void Start()
    {
        _Input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = _Input.GetPlayerAxis("Horizontal") * rotationSpeed;

        Vector3 verticalAxis = new Vector3(0, 0, 1) * _Input.GetPlayerAxis("Vertical");
        Vector3 horizontalAxis = new Vector3(1, 0, 0) * _Input.GetPlayerAxis("Horizontal");

        // rotation *= Time.deltaTime;

        Vector3 translation = verticalAxis + horizontalAxis;
        translation *= speed * Time.deltaTime;

        if (_Input.GetPlayerButton("Boost"))
        {
            translation += translation * 5;
        }

        transform.Translate(translation, Space.World);

        if (translation.magnitude != 0)
            transform.forward = translation;
    }
}
