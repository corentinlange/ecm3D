using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Action m_Action;

    private void Start() {
        m_Action = Instantiate(m_Action);
        m_Action.m_ObjectName = transform.name;
    }

    public void Interact(){
        if(m_Action != null){
            ActionHandler.Trigger(m_Action);
            if(m_Action.m_Type == Action.Actions.Collect)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
