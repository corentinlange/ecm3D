using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActionTrigger))]
public class InteractableObject : MonoBehaviour
{
    public Action m_Action;
    ActionTrigger m_Trigger;
    void Start(){
        m_Trigger = GetComponent<ActionTrigger>();
        if(m_Action != null){
            m_Trigger.SetAction(m_Action);
        }
    }

    public void Interact(){
        GetComponent<ActionTrigger>().onTriggered();
    }
}
