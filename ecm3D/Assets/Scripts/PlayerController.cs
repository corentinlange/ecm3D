using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;

    private ThirdPersonCamera cam;

    public Vector3 height;
    public CharacterController controller;

    public float moveSpeed;
    public float rSpeed;
    public float gravity = 9.8f;
    private float vSpeed = 0; // current vertical velocity

    public float jumpHeight;

    public LayerMask layerMask;
    
    public bool isUIopen;

    public Animator animator;

    [SerializeField]
    public CinemachineFreeLook vcam;

    public static PlayerController singleton; 
    void Start()
    {
        controller = GetComponent<CharacterController>();

        cam = GetComponent<ThirdPersonCamera>();

        if(!isUIopen){
            Cursor.lockState = CursorLockMode.Locked;
        }

        singleton = this;
    }

    void Update()
    {
        Vector3 move = Vector3.zero;

        if (controller.isGrounded && vSpeed < 0)
        {
            vSpeed = 0f;
        }

        if(!isUIopen)
        {

            if(Input.GetKey(KeyCode.Q)){
                transform.Rotate(new Vector3(0, -rSpeed * Time.deltaTime, 0));
            }
            else if(Input.GetKey(KeyCode.D)){
                transform.Rotate(new Vector3(0, rSpeed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.Z))
            {
                if(!controller.isGrounded)
                    animator.SetBool("Run", true);
                    
                move += transform.forward * moveSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("Run", false);
                move -= transform.forward * moveSpeed * Time.deltaTime;
            }else{
                animator.SetBool("Run", false);
            }
            // if (Input.GetButtonDown("Jump") && controller.isGrounded)
            // {
            //     animator.SetBool("Jump", true);
            //     vSpeed += Mathf.Sqrt(jumpHeight * 3.0f * gravity);
            // }
        }
        if(!controller.isGrounded){
            vSpeed -= gravity * Time.deltaTime;
            // animator.SetBool("Jump", false);
        }else{
        }
        controller.Move(vSpeed * new Vector3(0,1,0) + move);

        Vector3 pos = new Vector3((Screen.width / 2), (Screen.height / 2), 0);
        
        if(!isUIopen)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(pos);     

            Vector3 raycastDir = Quaternion.AngleAxis(-20, mainCamera.transform.TransformDirection(Vector3.right)) * mainCamera.transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow);
                QuestHolder questHolder = hit.transform.GetComponent<QuestHolder>();
                if(questHolder != null){
                    if(Input.GetMouseButtonDown(0)){
                        questHolder.Talk();
                    }
                }

                InteractableObject IO = hit.transform.GetComponent<InteractableObject>();
                if(IO != null){
                    if(Input.GetMouseButtonDown(0)){
                        IO.Interact();
                    }
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow);
            }
        }
    }

    public void onUIopenTrigger(){
        isUIopen = !isUIopen;
        vcam.enabled = !isUIopen;
        Debug.Log(isUIopen);
        Debug.Log(vcam.enabled);
    }
}
