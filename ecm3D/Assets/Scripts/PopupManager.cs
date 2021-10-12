using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour {
    public Text text;
    public Button buttonNext;

    public QuestHolder Holder;
    public List<string> PresentationTexts;
    private int presentationTextsNumber;
    private int step;

    public void InitiatePopup(List<string> texts, QuestHolder _holder){
        Cursor.lockState = CursorLockMode.Confined;
        Holder = _holder;
        PresentationTexts = texts;
        presentationTextsNumber = PresentationTexts.Count;
        UpdateContent();
    }

    private void UpdateContent(){
        SetText(PresentationTexts[step]);
    }

    public void ButtonClicked(){
        if(step != presentationTextsNumber - 1){
            step++;
            UpdateContent();
        }else{
            Confirm();
            Holder.StartQuest();
        }
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
        Destroy(gameObject);
    }
}
