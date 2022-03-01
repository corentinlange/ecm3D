using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsManager : MonoBehaviour 
{
    public static QuestsManager singleton;

    public List<Quest> activeQuests;
    private GameObject Player;

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

    private void Start() {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    public void SetActiveQuest(Quest quest){
        activeQuests.Add(quest);
        quest.StartQuest();
    }

    public void EndQuest(Quest _quest){
        activeQuests.Find(x => x.Name == _quest.Name).End();
        activeQuests.Remove(_quest);
    }

    public void ActionTriggered(Action _action){
        List<Quest> toRemove = new List<Quest>();

        foreach(Quest _quest in activeQuests){
            if (_quest.m_Action.m_Type == _action.m_Type){
                if(_action.m_Type == Action.Actions.Collect)
                {
                    Debug.Log(_action.m_ObjectName);
                    Debug.Log(_quest.m_ToCollect);
                    
                    if(_action.m_ObjectName != "" && _action.m_ObjectName == _quest.m_ToCollect)
                    {
                        _quest.m_Amount++;
                        if(_quest.HasRequieredAmount())
                        {
                            toRemove.Add(_quest);
                        }
                    }
                }
                else{
                    _quest.m_Amount++;
                    if(_quest.HasRequieredAmount())
                    {
                        toRemove.Add(_quest);
                    }
                }
            }
        }

        foreach(Quest _quest in toRemove)
        {
            EndQuest(_quest);
        }
    }
}
