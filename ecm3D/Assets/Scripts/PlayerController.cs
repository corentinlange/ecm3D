using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;

    public Vector3 height;

    public CharacterController controller;

    public float gravity = 9.8f;
    private float vSpeed = 0; // current vertical velocity

    public float jumpHeight;

    void Start()
    {
        mainCamera = Camera.main;
        controller = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {   
        Quest _quest = QuestsManager.singleton.activeQuests.Find(x => x.Name == other.name);
        if(_quest != null){
            QuestsManager.singleton.EndQuest(_quest);
        }
    }

    void FixedUpdate()
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

        RaycastHit hit;

        Vector3 raycastDir = Quaternion.AngleAxis(-20, mainCamera.transform.TransformDirection(Vector3.right)) * mainCamera.transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position + height, raycastDir, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position + height, raycastDir * hit.distance, Color.yellow);
            
            QuestHolder questHolder = hit.transform.GetComponent<QuestHolder>();
            if(questHolder != null){
                if(Input.GetMouseButtonDown(0)){
                    questHolder.Talk();
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position + height,raycastDir * 1000, Color.white);
        }
    }
}
