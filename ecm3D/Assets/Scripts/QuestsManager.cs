using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsManager : MonoBehaviour {

    public Quest activeQuest;
    private GameObject Player;

    private void Start() {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    public void SetActiveQuest(Quest quest){
        Debug.Log(quest.Name);
        activeQuest = quest;
        quest.StartQuest();
    }

    public void EndQuest(){
        activeQuest.End();
        if(activeQuest.NextQuestAfterFinished != null){
            SetActiveQuest(activeQuest.NextQuestAfterFinished)
        }else{
            activeQuest = null;
        }
    }
}
