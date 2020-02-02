using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(CharacterController), typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public float baseSpeed = 20.0f;

    public float boostSpeed = 90.0f;

    public float rotationSpeed = 100.0f;

    public float boostCooldown = 2.0f;

    public float boostDuration = 0.3f;

    public float pushPower = 2f;

    private bool _freezed = false;
    public bool Freezed { get { return _freezed; } set { _freezed = value; if (_freezed) { _currentSpeed = 0; } } }

    [SerializeField]
    Transform spawn;
    bool active = false;

    private PlayerInput _input;

    private CharacterController _controller;
    private Rigidbody _rb;
    private Animator _anim;

    private float _boostTime;

    private float _currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _controller = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        _boostTime = 0.0f;
        _anim = GetComponentInChildren<Animator>();
        _anim.SetBool("Grounded", true);
    }

    private void Update()
    {
        _anim.SetFloat("MoveSpeed", _currentSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!active && spawn != null)
        {
            bool moved = _input.GetPlayerButtonDown("Fire1");
            moved = moved || _input.GetPlayerButtonDown("Boost");
            moved = moved || _input.GetPlayerAxis("Horizontal") != 0;
            moved = moved || _input.GetPlayerAxis("Vertical") != 0;
            if (moved)
            {
                gameObject.transform.position = spawn.transform.position;
                active = true;
            }
            return;
        }

        if (Freezed) return;

        if (_input.GetPlayerButton("Boost") && CanBoost())
        {
            _boostTime = Time.time;
        }

        float maxSpeed;
        if (IsBoosting())
        {
            maxSpeed = boostSpeed;
        }
        else
        {
            maxSpeed = baseSpeed;
        }

        Vector3 verticalAxis = new Vector3(0, 0, 1) * _input.GetPlayerAxis("Vertical");
        Vector3 horizontalAxis = new Vector3(1, 0, 0) * _input.GetPlayerAxis("Horizontal");

        Vector3 translation = verticalAxis + horizontalAxis;
        translation *= maxSpeed;
        _currentSpeed = translation.magnitude;
        translation *= Time.fixedDeltaTime;

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

    private bool IsBoosting()
    {
        return Time.time <= _boostTime + boostDuration;
    }

    private bool CanBoost()
    {
        return Time.time >= _boostTime + boostDuration + boostCooldown;
    }
}
