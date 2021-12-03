using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Ben Waters
 * 
 * Follow Camera from car tutorial:
 * URL: https://www.youtube.com/watch?v=Z4HA8zJhGEk
 */


public class FollowCamera : MonoBehaviour
{

    [SerializeField] private Vector3 offset;    
    [SerializeField] private Transform target; //specifys the object to follow


    //changes in these variables change the snapiness of the camera
    [SerializeField] private float translateSpeed; //controls speed that camera catches up to target
    [SerializeField] private float rotateSpeed; //stors speed of camera rotation


    //Physics update function
    private void FixedUpdate()
    {
        handleTranslation();
        handleRotation();
    }



    private void handleTranslation()
    {
        //figures out the line to translate on and linearly interpolates over time
        var targetPos = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPos, translateSpeed * Time.deltaTime);
    }


    private void handleRotation()
    {
        //same as translation except for rotation
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }
}
