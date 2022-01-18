using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentBrakeForce;
    private float steeringAngle;

    private bool isBreaking;

    private Rigidbody carRB;
    private AudioSource carAudio;

    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float stabilizer;

    [SerializeField] private Transform frontLeftTrans;
    [SerializeField] private Transform frontRightTrans;
    [SerializeField] private Transform rearLeftTrans;
    [SerializeField] private Transform rearRightTrans;

    [SerializeField] private WheelCollider frontLeftColl;
    [SerializeField] private WheelCollider frontRightColl;
    [SerializeField] private WheelCollider rearLeftColl;
    [SerializeField] private WheelCollider rearRightColl;

    [SerializeField] private AudioClip motorAccelerating;
    [SerializeField] private AudioClip motorIdle;
    // Brake Sound Clip

    // Tire Particals

    private void Start()
    {
        carRB = GetComponent<Rigidbody>();
        carAudio = GetComponent<AudioSource>();
        carRB.centerOfMass = new Vector3(0, stabilizer, 0);
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(horizontal);
        verticalInput = Input.GetAxis(vertical);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftColl.motorTorque = verticalInput * motorForce;
        frontRightColl.motorTorque = verticalInput * motorForce;
        if (!carAudio.isPlaying && verticalInput != 0)
        {
            carAudio.PlayOneShot(motorAccelerating);
        } 
        if (isBreaking)
        {
            currentBrakeForce = brakeForce;
            ApplyBrakes();
        }
        else
        {
            currentBrakeForce = 0f;
            ApplyBrakes();
        }
    }

    private void ApplyBrakes()
    {
        frontLeftColl.brakeTorque = currentBrakeForce;
        frontRightColl.brakeTorque = currentBrakeForce;
        rearLeftColl.brakeTorque = currentBrakeForce;
        rearRightColl.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        steeringAngle = maxSteeringAngle * horizontalInput;
        frontLeftColl.steerAngle = steeringAngle;
        frontRightColl.steerAngle = steeringAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftColl, frontLeftTrans);
        UpdateSingleWheel(frontRightColl, frontRightTrans);
        UpdateSingleWheel(rearLeftColl, rearLeftTrans);
        UpdateSingleWheel(rearRightColl, rearRightTrans);
    }

    private void UpdateSingleWheel(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        transform.rotation = rot;
        transform.position = pos;
    }
}
