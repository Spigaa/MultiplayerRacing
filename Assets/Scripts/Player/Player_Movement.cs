using Fusion;
using Fusion.Addons.SimpleKCC;
using UnityEngine;

public class Player_Movement : NetworkBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float reverseSpeed = 5f;
    [Space(10)]
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float brakeForce = 50f;
    [Space(10)]
    [SerializeField] private float forwardSmoothness = 8f;
    [SerializeField] private float acceleration = 4f;
    [SerializeField] private float deceleration = 8f;

    [Header("References")]
    [SerializeField] private Player_Camera player_Camera;

    [Networked] private NetworkButtons prevButtons { get; set; } = new NetworkButtons();

    private SimpleKCC kcc;
    private Player_Visuals visuals;

    private float forwardInput;
    private float ts;

    private Vector3 moveDirection;

    void Awake() {
        InitializeReferences();
    }

    private void InitializeReferences() {
        kcc = GetComponent<SimpleKCC>();
        visuals = GetComponent<Player_Visuals>();
    }

    public override void Spawned() {
        base.Spawned();

        kcc.SetGravity(Physics.gravity.y * 2f);

        if (!HasInputAuthority)
            Destroy(player_Camera.gameObject);
    }

    public override void FixedUpdateNetwork() {
        base.FixedUpdateNetwork();

        if (GetInput(out NetInput input)) {            
            float result;
            float smoothness;

            if (input.Buttons.WasPressed(prevButtons, InputButton.Brake)) {
                result = 0;
                smoothness = deceleration;
            }
            else {
                if (input.Direction.y > 0) {
                    result = speed;
                    smoothness = acceleration;
                }
                else if (input.Direction.y < 0) {
                    result = -reverseSpeed;
                    smoothness = deceleration;
                }
                else {
                    result = 0;
                    smoothness = acceleration;
                }
            }            

            forwardInput = Mathf.Lerp(forwardInput, result, smoothness * Time.deltaTime);
            moveDirection = Vector3.Lerp(moveDirection, transform.forward * forwardInput, Time.deltaTime * forwardSmoothness);

            ts = Mathf.Lerp(ts, input.Direction.x * turnSpeed * Time.deltaTime, Time.deltaTime * 4.5f);
            ts *= Mathf.Clamp(kcc.RealSpeed, 0, 1);

            kcc.Move(moveDirection);
            kcc.AddLookRotation(new Vector2(0, ts));

            visuals.HandleWheelsRotation(input.Direction, kcc.RealSpeed);

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
