using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

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
    public static ThirdPersonCamera singleton;
    public bool isUIopen;

    [SerializeField]
    public CinemachineFreeLook vcam;
    public CharacterController controller;
    public Transform cam;
    
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float speed = 6f;
    
    private void Awake() {
        // if the singleton hasn't been initialized yet
        if (singleton != null && singleton != this)
        {
        Destroy(this.gameObject);
        return;//Avoid doing anything else
        }
    
        singleton = this;
        DontDestroyOnLoad( this.gameObject );
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void onUIopenTrigger(){
        isUIopen = !isUIopen;
        vcam.enabled = !isUIopen;
    }
    void Update()
    {
        if(!isUIopen){
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
    }
}
