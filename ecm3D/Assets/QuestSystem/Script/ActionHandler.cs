using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionHandler
{
    public static void Trigger(Action _action){
        if(_action.m_QuestTriggerer){
            QuestsManager.singleton.ActionTriggered(_action);
        }
    }
}
