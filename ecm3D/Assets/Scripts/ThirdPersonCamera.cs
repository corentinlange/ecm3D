using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
Table du ru,
bancs normaux,
poubelles,
mobiliers rue haute,
table pic nic,
lampadaires,
arbres,
*/
public class ThirdPersonCamera : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public float gravity = 9.8f;
    private float vSpeed = 0; // current vertical velocity

    public float jumpHeight;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(Cursor.lockState == CursorLockMode.Locked){
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if(direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate() {
        if (controller.isGrounded && vSpeed < 0)
        {
            vSpeed = 0f;
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            Debug.Log("jump");
            vSpeed += Mathf.Sqrt(jumpHeight * 3.0f * gravity);
        }

        if(!controller.isGrounded){
            vSpeed -= gravity * Time.deltaTime;
        }

        controller.Move(vSpeed * new Vector3(0,1,0));
    }

}
