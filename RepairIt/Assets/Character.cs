using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Character : MonoBehaviour
{
    public float speed = 10.0f;
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
        float translation = _Input.GetPlayerAxis("Vertical") * speed;
        float rotation = _Input.GetPlayerAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        if (_Input.GetPlayerButton("Fire1"))
        {
            Debug.Log("Riz publicité");
            Debug.DrawLine(
                transform.position,
                transform.position + 30.0f * transform.forward,
                Color.white,
                5.0f
            );
        }
    }
}
