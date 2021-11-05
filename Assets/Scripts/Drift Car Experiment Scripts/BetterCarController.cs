using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ben Waters
/*
 * Created by following a tutorial to learn, will not be in final game in full      
 * URL: https://www.youtube.com/watch?v=Z4HA8zJhGEk
 */


public class BetterCarController : MonoBehaviour
{

    //variable for controlling the car

    //input variables --------------------------------------------------------------------------------
    private const string HORIZ = "Horizontal";
    private const string VERT = "Vertical";

    private float horizInput; //A/D input
    private float vertInput;  //W/S input
    private bool isBraking;  //spacebar input

    //Wheel Collider Variables -----------------------------------------------------------------------
    //  -made serializable so they can be dragged and dropped in the editor
    //  -these are used for physics calculations
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;

    //Motor Force Variables --------------------------------------------------------------------------

    //  - Defines torque on wheels
    //  - in a later version our game will apply force to the back of the car instead of using a motor
    [SerializeField] private float motorForce;

    //brake variables
    private float currentBrakeForce;
    [SerializeField] private float brakeForce;

    //steering variables -----------------------------------------------------------------------------
    [SerializeField] private float maxSteerAngle;
    private float currSteerAngle;

    //Meshes variables / holds the transforms of the visual meshes
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;

    //using fixed update since its a physics car
    private void FixedUpdate()
    {
        getInput();
        handleMotor();
        handleTurning();
        updateWheels();
    }

    //this function gets user controls
    private void getInput()
    {
        horizInput = Input.GetAxis(HORIZ);
        vertInput = Input.GetAxis(VERT);
        isBraking = Input.GetKey(KeyCode.Space);
    }

    //controls motor of the car (may not be neccesary for a game / at least for turning wheels)
    // it will still need a driving force 
    private void handleMotor()
    {
        //This makes the car front wheel drive (good for drifting)
        frontLeftWheelCollider.motorTorque = vertInput * motorForce;
        frontRightWheelCollider.motorTorque = vertInput * motorForce;
        //calculate braking force

        currentBrakeForce = isBraking ? brakeForce : 0f;
        applyBrakes();
        
    }

    //helper function for motor control function to apply breaks to all 4 wheels
    private void applyBrakes()
    {
        //disabled for drifting
        //frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        //frontRightWheelCollider.brakeTorque = currentBrakeForce;
        backLeftWheelCollider.brakeTorque = currentBrakeForce;
        backRightWheelCollider.brakeTorque = currentBrakeForce;

    }

    //This fucntion handles the steering of the vehichle by modifying the wheel colliders based on player input
    private void handleTurning()
    {
        currSteerAngle = maxSteerAngle * horizInput;
        frontLeftWheelCollider.steerAngle = currSteerAngle;
        frontRightWheelCollider.steerAngle = currSteerAngle;
    }

    //This function updates the meshes to look like what the physics objects are doing
    private void updateWheels()
    {
        updateIndividualWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        updateIndividualWheel(frontRightWheelCollider, frontRightWheelTransform);
        updateIndividualWheel(backLeftWheelCollider, backLeftWheelTransform);
        updateIndividualWheel(backRightWheelCollider, backRightWheelTransform);
    }

    //helper function for update wheels / gets the position and rotation of wheel colliders and maps
    //the changes onto the the transforms of the wheel meshes
    private void updateIndividualWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.rotation = rotation;
        wheelTransform.position = position;

    }
}
