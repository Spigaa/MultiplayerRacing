using Fusion;
using Fusion.Addons.SimpleKCC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : NetworkBehaviour
{
    [Header("Settings")]
    //[SerializeField] private SimpleKCC kcc;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float brakeForce = 50f;
    [SerializeField] private float reverseSpeed = 5f;

    [Networked] private NetworkButtons prevButtons { get; set; } = new NetworkButtons();

    private Rigidbody rb;
    //private float moveInput;
    //private float turnInput;
    private float forwardInput;
    
    void Awake() {
        InitializeReferences();
    }

    private void InitializeReferences() {
        rb = GetComponent<Rigidbody>();
    }

    public override void Spawned() {
        base.Spawned();

        //kcc.SetGravity(Physics.gravity.y * 2f);
    }

    public override void FixedUpdateNetwork() {
        base.FixedUpdateNetwork();

        if (GetInput(out NetInput input)) {
            //Vector3 dir = kcc.TransformRotation * new Vector3(input.Direction.x, 0, input.Direction.y);
            //kcc.Move(dir.normalized * speed);

            if (rb.velocity.magnitude > 0.1f) transform.Rotate(0, input.Direction.x * turnSpeed * Time.deltaTime, 0);
            rb.drag = input.Buttons.WasPressed(prevButtons, InputButton.Brake) ? brakeForce : 1;

            if (input.Direction.y > 0) forwardInput = speed;
            else if (input.Direction.y < 0) forwardInput = reverseSpeed;
            else forwardInput = 0;

            rb.velocity = transform.forward * forwardInput * input.Direction.y;

            prevButtons = input.Buttons;
        }
    }

    private void HandleInputMovement() {
        /*moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        if (rb.velocity.magnitude > 0.1f) transform.Rotate(0, turnInput * turnSpeed * Time.deltaTime, 0);
        rb.drag = Input.GetKey(KeyCode.Space) ? brakeForce : 1;*/
    }

    private void HandlePhysics() {
        /*if (moveInput > 0) forwardInput = speed;        
        else if (moveInput < 0) forwardInput = reverseSpeed;
        else forwardInput = 0;

        rb.velocity = transform.forward * forwardInput * moveInput;*/
    }

    private void FixedUpdate() {
        //HandlePhysics();
    }

    void Update() {
        //HandleInputMovement();
    }
}
