using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //declare public variables
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    Vector3 currentRotation;


    // Update is called once per frame
    void Update()
    {


        //local X is sideways/turning
        //local Z is forward Backward

        if (Input.GetKey(KeyCode.A)) {
            currentRotation = Vector3.down;
            print("A");
        } 
        else if (Input.GetKey(KeyCode.D)) currentRotation = Vector3.up;
        else currentRotation = Vector3.zero;

        transform.Translate(0f, 0f, -speed * Input.GetAxis("Vertical") * Time.deltaTime);

        transform.Rotate(currentRotation * turnSpeed * Time.deltaTime);



        //-speed * Input.GetAxis("Vertical") * Time.deltaTime



    }
}
