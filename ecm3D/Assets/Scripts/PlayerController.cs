using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private QuestsManager QuestsManager;
    private Camera mainCamera;

    public Vector3 height;

    void Start()
    {
        mainCamera = Camera.main;
        QuestsManager = GameObject.FindGameObjectsWithTag("QuestsManager")[0].GetComponent<QuestsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(QuestsManager.activeQuest){
            if(QuestsManager.activeQuest.Checkpoint == other.gameObject){
                QuestsManager.EndQuest();
            }
        }
    }

    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        // int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        // layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Vector3 raycastDir = Quaternion.AngleAxis(-20, mainCamera.transform.TransformDirection(Vector3.right)) * mainCamera.transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position + height, raycastDir, out hit, Mathf.Infinity/*, layerMask*/))
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
