using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests", order = 0)]
public class Quest : ScriptableObject {

    // [HideInInspector]
    public QuestHolder Holder;

    public string Name;
    public string Description;

    [Header("Textes de présentations de quête")]
    public List<string> PresentationTexts;
    [Header("Textes dits par le PNJ si le joueur lui reparle sans avoir terminé.")]
    public List<string> ProgressTexts;
    [Header("Textes de fin de quête")]
    public List<string> FinishedTexts;

    [SerializeField]
    public List<Quest> NeededToUnlock = new List<Quest>();
    public Quest NextQuestAfterFinished;

    public Action m_Action;
    
    public int m_RequieredAmount;
    public int m_Amount = 0;
    
    [Header("Position to reach (only if Action.Type == Reach)")]
    public Vector3 positionToReach;

    public GameObject CheckpointPrefab;
    public float CheckpointRadius;
    
    [HideInInspector] public GameObject Checkpoint;

    public enum States
    {
        Available,
        Unavailable,
        Started,
        Finished,
        FinishedPending,
        Retry
    };
    public States State = States.Available;

    public bool StartQuest(){
        m_Amount = 0;
        if(m_Action.m_Type == Action.Actions.Reach){
            Checkpoint = Instantiate(CheckpointPrefab, positionToReach, Quaternion.identity);
            Checkpoint.transform.localScale = new Vector3(CheckpointRadius, Checkpoint.transform.localScale.y, CheckpointRadius);
            Checkpoint.GetComponent<ActionTrigger>().SetAction(m_Action);
            Checkpoint.name = Name;
        }
        return true;
    }

    public void CheckNeededQuests(){
        if(State == States.Unavailable){
            foreach(Quest _quest in NeededToUnlock){
                if(_quest.State != States.Finished){
                    State = States.Unavailable;
                    return;
                }
            }
            State = States.Available;
        }
    }

    
    public bool HasRequieredAmount(){
        if(m_Amount == m_RequieredAmount){
            return true;
        }
        return false;
    }

    public void GiveRecompense(){
        State = States.Finished;
        Debug.Log("Recompense given");
    }

    public void End(){
        Destroy(Checkpoint);
        State = States.FinishedPending;
        // Holder.SetFinished();
    }
}
