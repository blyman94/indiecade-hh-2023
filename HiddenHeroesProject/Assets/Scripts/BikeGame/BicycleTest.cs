using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicycleTest : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Player movement speed
    [SerializeField] private float turnSpeed = 1f; // Player turning speed
    [SerializeField] private float maxSteerAngle = 45f; // Maximum steering angle
    [SerializeField] private CircleCollider2D frontWheelCollider; // Reference to front wheel collider
    [SerializeField] private CircleCollider2D rearWheelCollider; // Reference to rear wheel collider
    [SerializeField] private float rotationSpeed = 10f; // Speed at which player rotates to face motion direction

    private float verticalInput; // Player's vertical input
    private float currentSteerAngle; // Current steering angle
    private float currentRotationSpeed; // Current rotation speed, adjusted for input
    private float targetSteerAngle; // Target steering angle, used for smoothing

    void Update()
    {
        // Get player input
        verticalInput = Input.GetAxis("Vertical");

        // Calculate current steering angle
        targetSteerAngle = maxSteerAngle * verticalInput;
        currentSteerAngle = Mathf.Lerp(currentSteerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);

        // Apply steering to front wheel
        frontWheelCollider.transform.localRotation = Quaternion.Euler(0f, 0f, currentSteerAngle);

        // Calculate player movement
        float horizontalMovement = speed * Time.deltaTime;
        float verticalMovement = 0f;
        if (currentSteerAngle != 0f)
        {
            verticalMovement = horizontalMovement * Mathf.Tan(Mathf.Deg2Rad * currentSteerAngle);
        }

        // Apply movement to player
        //transform.position += new Vector3(horizontalMovement, verticalMovement, 0f);

        // Rotate player based on motion direction
        if (verticalMovement != 0f)
        {
            Vector3 targetDirection = new Vector3(-verticalMovement, horizontalMovement, 0f).normalized;
            float angle = Vector3.SignedAngle(transform.up, targetDirection, Vector3.forward);
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, rotationSpeed * Mathf.Abs(angle) / 180f, Time.deltaTime * turnSpeed);
            transform.up = Vector3.RotateTowards(transform.up, targetDirection, Mathf.Deg2Rad * currentRotationSpeed * Time.deltaTime, 0f);
        }
        else if (currentSteerAngle == 0f)
        {
            transform.up = Vector3.RotateTowards(transform.up, Vector3.up, Mathf.Deg2Rad * rotationSpeed * Time.deltaTime, 0f);
        }
    }
}
