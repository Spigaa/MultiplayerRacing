using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Fusion.Sockets.NetBitBuffer;

public class Player_Camera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;

    [Header("Settings")]
    [SerializeField] private Vector3 positionOffset = new Vector3(0, 5, -10);
    [SerializeField] private Vector3 rotationOffset = new Vector3(0, 5, -10);
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float rotationSmoothSpeed = 5f;

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;

    private void Awake() {
        transform.SetParent(null);
    }

    private void HandleCameraMovement() {
        desiredPosition = target.position - target.forward * positionOffset.z + Vector3.up * positionOffset.y;
        desiredRotation = Quaternion.LookRotation(target.transform.forward + rotationOffset);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
    }

    void LateUpdate() {
        HandleCameraMovement();
    }
}
