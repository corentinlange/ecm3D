using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;

    private ThirdPersonCamera cam;

    public Vector3 height;
    public Transform Aim;
    public CharacterController controller;

    public float moveSpeed;
    public float gravity = 9.8f;
    private float vSpeed = 0; // current vertical velocity

    public float jumpHeight;

    public LayerMask layerMask;

    void Start()
    {
        mainCamera = Camera.main;
        controller = GetComponent<CharacterController>();

        cam = GetComponent<ThirdPersonCamera>();
    }

    void Update()
    {
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

        Vector3 pos = new Vector3((Screen.width / 2), (Screen.height / 2), 0);
        
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(pos);     

        Vector3 raycastDir = Quaternion.AngleAxis(-20, mainCamera.transform.TransformDirection(Vector3.right)) * mainCamera.transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Aim.position = hit.point;
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
            Aim.position = ray.origin + ray.direction * 10f;
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow);
        }
    }
}
