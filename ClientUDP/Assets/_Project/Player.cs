using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {
    [SerializeField] float moveSpeed = 5f;
    Rigidbody rb;
    Vector3 velocity;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        CalculateVelocity();
    }

    void CalculateVelocity() {
        velocity = new(Input.GetAxis("Horizontal") * moveSpeed, rb.linearVelocity.y, Input.GetAxis("Vertical") * moveSpeed);
        rb.linearVelocity = velocity;
    }
}