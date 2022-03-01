using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{

    public Color32 AvailableColor;
    public Color32 UnavailableColor;
    public Color32 StartedColor;
    public Color32 FinishedColor;
    public Color32 RetryColor;

    private Dictionary<string, Color32> Palette;

    public Quest m_Quest;

    public Text Name;
    public Text Description;

    public Button QuestButton;
    public GameObject m_FinishQuestButton;

    private Color Background;

    public void InitiateUI(Quest _quest)
    {   
        m_Quest = _quest;

        Palette = new Dictionary<string, Color32>(){
            {"Available",AvailableColor},
            {"Unavailable",UnavailableColor},
            {"Started",StartedColor},
            {"Finished",FinishedColor},
            {"FinishedPending",FinishedColor},
            {"Retry",RetryColor}
        };

        Name.text = m_Quest.Name;
        Description.text = m_Quest.Description;

        transform.GetComponent<Image>().color = Palette[m_Quest.State.ToString("g")];

        AdaptUI();
    }

    public void onClickQuestButton(){
        switch(m_Quest.State){
            case Quest.States.Started:
                break;
            case Quest.States.Finished:
                break;
            case Quest.States.FinishedPending:
                break;
            case Quest.States.Unavailable:
                break;
            case Quest.States.Available:
                m_Quest.Holder.PresentQuest(m_Quest);
                break;
        }
    }

    private void AdaptUI(){
        switch(m_Quest.State){
            case Quest.States.Started:
                QuestButton.interactable = false;
                break;
            case Quest.States.FinishedPending:
                ShowHideQuestButton(false);
                ShowHideFinishQuestButton(true);
                break;
            case Quest.States.Finished:
                ShowHideQuestButton(false);
                ShowHideFinishQuestButton(false);
                break;
            case Quest.States.Unavailable:
                QuestButton.interactable = false;
                break;
        }
    }

    private void ShowHideFinishQuestButton(bool value){
        m_FinishQuestButton.SetActive(value);
    }
    private void ShowHideQuestButton(bool value){
        QuestButton.gameObject.SetActive(value);
    }

    public void onClickFinishQuestButton(){
        m_Quest.Holder.FinishQuest(m_Quest);
    }
}

