using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class QuestHolder : MonoBehaviour
{
    // TODO : Possibilité d'avoir plusieurs quêtes sur un même Holder
    // TODO : Revenir au holder après avoir confirmé la quête pour avoir du texte entre les quêtes
    public Quest Quest;
    public QuestsManager QuestsManager;
    
    private bool started;
    private bool finished;

    private GameObject TextPopup;
    
    void Start()
    {
        Quest.Holder = this;
    }

    void Update()
    {
        
    }

    private async Task LoadPopup(){
        GameObject _popup = Resources.Load<GameObject>("PopupAsset");
        TextPopup = Instantiate(_popup, new Vector3(0, 0, 0), Quaternion.identity);
        TextPopup.transform.SetParent(GameObject.FindGameObjectsWithTag("Canvas")[0].transform, false);
        await Task.Yield();
    }

    public void SetFinished(){
        finished = true;
        started = false;
    }

    public async void Talk(){
        if(TextPopup == null){
            await LoadPopup();
            TextPopup.GetComponent<PopupManager>().InitiatePopup(Quest.Presentation, this);
        }
    }

    public void StartQuest(){
        if(!started && !finished){
            Debug.Log("Quest started : ");
            started = true;
            QuestsManager.SetActiveQuest(Quest);
        }
    }

}
