using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour {
    public Text text;
    public Text m_QuestName;
    public Text m_QuestDescription;
    public Button buttonNext;

    public Transform QuestsContainer;

    public QuestHolder Holder;
    public List<string> Texts;
    private int textsNumber;
    private int step;
    public CanvasGroup m_Popup;

    public void InitiatePopup(List<string> texts, QuestHolder _holder){
        
        Cursor.lockState = CursorLockMode.Confined;
        PlayerController.singleton.onUIopenTrigger();

        if(_holder != null){
            Holder = _holder;
            
            for(int i = 0; i < Holder.Quests.Count; i++){
                LoadQuestUI(Holder.Quests[i]);
            }
        }
        SetNewTexts(texts);
    }

    public void SetNewTexts(List<string> newTexts, string questName = "Nom de la quête", string questDescription = "Description"){
        Texts = newTexts;
        textsNumber = Texts.Count;
        step = 0;

        m_QuestName.text = questName;
        m_QuestDescription.text = questDescription;
        UpdateContent();
    }

    private void LoadQuestUI(Quest _quest){
        GameObject _questUI = Resources.Load<GameObject>("QuestUI");
        _questUI = Instantiate(_questUI, new Vector3(0, 0, 0), Quaternion.identity);
        _questUI.transform.SetParent(QuestsContainer, false);

        _questUI.GetComponent<QuestUIManager>().InitiateUI(_quest);

        GameObject separator = Resources.Load<GameObject>("Separator");
        Instantiate(separator, new Vector3(0, 0, 0), Quaternion.identity);
        _questUI.transform.SetParent(QuestsContainer, false);
    }

    private void UpdateContent(){
        SetText(Texts[step]);
    }

    public void ButtonClicked(){
        if(step != textsNumber - 1){
            step++;
            UpdateContent();
        }else{
            Confirm();
            Holder.UnloadPopup(true);
        }
    }

    public void ExitButtonClicked()
    {
        Confirm();
        Holder.UnloadPopup(true);
    }

    ///<summary>
    /// Change le message du popup
    ///</summary>   
    ///<param name="content">Nouveau message</param>
    public void SetText(string content)
    {
        text.text = content;
    }


    ///<summary>
    /// Appelé lorsque le bouton de confirmation est cliqué, détruit le popup
    ///</summary>
    public void Confirm()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ShowHide();
    }

    public void ShowHide(bool show = false)
    {
        if(show)
        {
            m_Popup.alpha = 1f;
            m_Popup.interactable = true;
            m_Popup.blocksRaycasts = true;
        }
        else{
            m_Popup.alpha = 0f;
            m_Popup.interactable = false;
            m_Popup.blocksRaycasts = false;
            foreach(Transform child in QuestsContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
