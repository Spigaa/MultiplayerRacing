using Fusion;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Visuals : NetworkBehaviour
{
    /*public override void Spawned() {
        base.Spawned();

        kcc.SetGravity(Physics.gravity.y * 2f);

        if (!HasInputAuthority)
            Destroy(player_Camera.gameObject);
    }
    */

    [Header("Wheels")]
    [SerializeField] private Transform[] frontWheels;
    [SerializeField] private Transform[] rearWheels;

    [Header("Parameters")]
    [SerializeField] private float rotationAmount;

    private float xRot;
    private float yRot;

    public void HandleWheelsRotation(Vector2 input, float vel) {
        xRot += vel;
        yRot = Mathf.Lerp(yRot, input.x * rotationAmount, Time.deltaTime * 8f);

        foreach (var w in frontWheels) {
            w.transform.localRotation = Quaternion.Euler(xRot, yRot, 90f);
        }
        foreach (var w in rearWheels) {
            w.transform.localRotation = Quaternion.Euler(xRot, 0, 90f);
        }
    }
}
