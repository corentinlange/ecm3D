using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action", menuName = "Action", order = 0)]
public class Action : ScriptableObject
{
    public enum Actions {
        Reach,
        Collect,
        GiveItem
    }

    public Actions m_Type;

    [HideInInspector]
    public string m_ObjectName = "";

    public bool m_QuestTriggerer;
}
