using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Ben Waters
//Follow camera that has 2 modes
//Mode 1 is simple follow from behind the car
//Mode 2 is freelook with the mouse but still following the car


    //changes:

    /*
     * 3/12/2021:   -Updated Follow camera script to have a mouse orbiting mode
     *              -Needs work
     * 
     * 
     * 
     */

public class FollowCameraWithMouse : MonoBehaviour
{
    [SerializeField] private Vector3 defaultOffset;
    [SerializeField] private Transform target; //specifys the object to follow

    private Vector3 camOffest;


    //changes in these variables change the snapiness of the camera
    [SerializeField] private float translateSpeed; //controls speed that camera catches up to target
    [SerializeField] private float rotateSpeed; //stors speed of camera rotation

    //input variables
    private float mouseX;
    private float mouseY;
    private bool rightCLick;
    private int timer;
    

    //Physics update function -----------------UPDATE FUNCTION---------------
    private void FixedUpdate()
    {
        getInput();
        handleMode();
        handleTranslation();
        handleRotation();

    }//----------------------------------------------------------------------

    //get inputs
    void getInput()
    {
        rightCLick = Input.GetMouseButton(1);
        if (rightCLick)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
        }
    }

    //sets the mode the camera is in
    void handleMode()
    {
        if (rightCLick){
            Quaternion camTurnAngle = Quaternion.AngleAxis(mouseX * rotateSpeed, Vector3.up);

            camOffest = camTurnAngle * camOffest;
        }
        else
        {
            camOffest = defaultOffset;
        }
    }

    //Handles the translation of the camera (The follow Part)
    private void handleTranslation()
    {
        //figures out the line to translate on and linearly interpolates over time
        var targetPos = target.TransformPoint(camOffest);
        transform.position = Vector3.Slerp(transform.position, targetPos, translateSpeed * Time.deltaTime);
    }
    
    //handle the rotation around the car for default 
    private void handleRotation()
    {
        //same as translation except for rotation
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }



    

    
}
