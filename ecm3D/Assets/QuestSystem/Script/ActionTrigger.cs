using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
    private Action m_Action;

    public void SetAction(Action _action){
        m_Action = _action;
    }

    public void onTriggered(){
        if(m_Action != null){
            ActionHandler.Trigger(m_Action);
        }

    }
}
