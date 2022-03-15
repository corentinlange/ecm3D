using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommandPromptController : MonoBehaviour
{
    private CanvasGroup Group;
    public GameObject m_InputLinePrefab;

    [SerializeField]
    private GameObject m_CurrentLine;
    private string m_Command;
    public string m_Truncate;
    
    public KeyCode m_SendCommandInput;

    public KeyCode m_ShowInput;


    void Start()
    {
        Group = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(m_SendCommandInput)){
            m_Command = m_CurrentLine.GetComponent<TMP_InputField>().text;
            string _sub = m_Command.Remove(m_Truncate.Length);
            Debug.Log(m_Command);
        }
        if(Input.GetKeyUp(m_ShowInput)){
            if(Group.alpha == 0){
                Cursor.lockState = CursorLockMode.Confined;
                ThirdPersonCamera.singleton.onUIopenTrigger();
                Group.alpha = 1 - Group.alpha;
                Group.interactable = !Group.interactable;
                Group.blocksRaycasts = !Group.blocksRaycasts;
            }else{

            }
        }
    }

    public void ValueChanged(){
        m_Command = m_CurrentLine.GetComponent<TMP_InputField>().text;
        Debug.Log(m_Command.Length);
    }
}
