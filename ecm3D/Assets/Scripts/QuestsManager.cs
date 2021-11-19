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
        Debug.Log(quest.Name);
        activeQuests.Add(quest);
        quest.StartQuest();
    }

    public void EndQuest(Quest _quest){
        activeQuests.Find(x => x.Name == _quest.Name).End();
        activeQuests.Remove(_quest);
    }
}
