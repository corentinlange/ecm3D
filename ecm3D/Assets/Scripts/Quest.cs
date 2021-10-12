using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests", order = 0)]
public class Quest : ScriptableObject {

    [HideInInspector] public QuestHolder Holder;

    public string Name;
    public string Description;
    public List<string> Presentation;
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

    public bool StartQuest(){
        if(!CheckParametersAndValidate()){
            return false;
        }

        if(Objective == Objectives.Checkpoint){
            Checkpoint = Instantiate(CheckpointPrefab, positionToReach, Quaternion.identity);
            Checkpoint.transform.localScale = new Vector3(CheckpointRadius, Checkpoint.transform.localScale.y, CheckpointRadius);
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

    public void End(){
        Destroy(Checkpoint);
        Holder.SetFinished();
        Debug.Log("Quest ended");
    }
}
