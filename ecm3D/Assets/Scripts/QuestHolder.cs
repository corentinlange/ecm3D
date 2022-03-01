using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class QuestHolder : MonoBehaviour
{
    [Header("Textes de présentation du PNJ")]
    public List<string> WaitingTexts;

    public List<Quest> Quests;
    
    private int questIndex;
    public enum States
    {
        Waiting,
        Presenting,
        Progress,
        FinishedPending,
        Finished
    };
    public States State
    {
        get{return m_State;}
        set{
            m_State = value;
            if(value == States.Waiting)
            {
                Destroy(m_StateObject);
                m_StateObject = Instantiate(m_WaitingPrefab, Vector3.zero, Quaternion.identity);
                m_StateObject.transform.parent = transform;
                m_StateObject.transform.position = transform.position + new Vector3(0, 1f, 0);
            }
        }
    }
    private States m_State;

    private GameObject TextPopup;

    private Quest toStart;
    private Quest toEnd;
    private List<Quest> startedQuests = new List<Quest>();

    public GameObject m_WaitingPrefab;

    private GameObject m_StateObject;
    
    void Start()
    {
        if(Quests.Count != 0){
            for(int i = 0; i<Quests.Count; i++){
                Quests[i] = Instantiate(Quests[i]);
                Quests[i].Holder = this;
                Quests[i].CheckNeededQuests();
            }
        }

        State = States.Waiting;
    }

    private void LoadPopup(){
        CheckQuestsStatus();
        GameObject _popup = Resources.Load<GameObject>("PopupAsset");
        TextPopup = Instantiate(_popup, new Vector3(0, 0, 0), Quaternion.identity);
        TextPopup.transform.SetParent(GameObject.FindGameObjectsWithTag("Canvas")[0].transform, false);
    }

    private void CheckQuestsStatus(){
        if(Quests.Count != 0){
            for(int i = 0; i<Quests.Count; i++){
                Quests[i].CheckNeededQuests();
            }
        }
    }

    public void UnloadPopup(bool accepted = false){
        switch(State)
        {
        case States.Waiting:
            // Todo : phrase d'au revoir
            break;
        case States.Presenting:
            State = States.Waiting;
            if(accepted){
                QuestsManager.singleton.SetActiveQuest(toStart); 
                startedQuests.Add(toStart);
                toStart.State = Quest.States.Started;
            }
            toStart = null;
            break;
        case States.Progress:
            // Rien pour l'instant, la quête continue.
            // Todo : faire en sorte de pouvoir annuler la quête ?
            break;
        case States.FinishedPending:
            break;
        case States.Finished:
            if(toEnd != null){
                toEnd.GiveRecompense();
                State = States.Waiting;
            }
            break;
        }

        PlayerController.singleton.onUIopenTrigger();
    }

    public void FinishQuest(Quest _quest){
        if(Quests.Find(x => x.Name == _quest.Name)){
            TextPopup.GetComponent<PopupManager>().SetNewTexts(_quest.FinishedTexts);
            toEnd = _quest;
            State = States.Finished;
        }
    }

    public void Talk(){
        if(TextPopup == null){
            
            LoadPopup();
            switch(State)
            {
            case States.Waiting:
                TextPopup.GetComponent<PopupManager>().InitiatePopup(WaitingTexts, this);
                break;
            case States.Presenting:
                TextPopup.GetComponent<PopupManager>().InitiatePopup(Quests[questIndex].PresentationTexts, this);
                break;
            case States.Progress:
                TextPopup.GetComponent<PopupManager>().InitiatePopup(Quests[questIndex].ProgressTexts, this);
                break;
            case States.Finished:
                TextPopup.GetComponent<PopupManager>().InitiatePopup(Quests[questIndex].FinishedTexts, this);
                break;
            }
            
        }
    }

    public void PresentQuest(Quest quest){
        toStart = Quests.Find(x => x.Name == quest.Name);
        if(toStart != null){
            State = States.Presenting;
            TextPopup.GetComponent<PopupManager>().SetNewTexts(toStart.PresentationTexts);
            // à la dernière étape il y a une option pour refuser la quête et une pour l'accepter
        }else{
            Debug.Log("Quest not found");
        }
    }

}
