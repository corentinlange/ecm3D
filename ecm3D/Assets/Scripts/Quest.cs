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

    public enum Objectives
    {
        Checkpoint,
        Collect,
        Bring
    };
    public Objectives Objective;

    [Header("Target selection (null if Checkpoint selected)")]
    [Tooltip("Select objects to collect/bring")]
    public GameObject Target;

    
    [Header("Position to reach (only if Checkpoint selected)")]
    [Tooltip("Select objects to collect/bring")]
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
        if(!CheckParametersAndValidate()){
            return false;
        }

        if(Objective == Objectives.Checkpoint){
            Checkpoint = Instantiate(CheckpointPrefab, positionToReach, Quaternion.identity);
            Checkpoint.transform.localScale = new Vector3(CheckpointRadius, Checkpoint.transform.localScale.y, CheckpointRadius);
            Checkpoint.name = Name;
        }
        return true;
    }

    ///<summary>
    /// Called to start quest, checks if objective are set right, either quest won't start.
    ///</summary>
    private bool CheckParametersAndValidate(){
        // Target musn't be null and position must be null
        if(Objective == Objectives.Collect || Objective == Objectives.Bring){
            if(positionToReach != null || Target == null){
                return false;
            }
        //position musn't be null and Target must be null
        }else{
            if(Target != null || positionToReach == null){
                return false;
            }
        }
        return true;
    }

    public void CheckNeededQuests(){
        foreach(Quest _quest in NeededToUnlock){
            if(_quest.State != States.Finished){
                return;
            }
        }
        State = States.Available;
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
