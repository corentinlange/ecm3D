using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Notifications : MonoBehaviour
{
    
    [System.Serializable]
    public struct Notification {
        public string m_Name;
        public string m_Content;

    }
    [SerializeField]
    public List<Notification> m_Notifications = new List<Notification>();

    [SerializeField]
    private Transform m_NotificationsContainer;
    public GameObject m_NotificationPrefab;

    public static Notifications singleton; 

    private void Awake() {
        singleton = this;
    }

    public void ShowNotification(string _name, params string[] replacements)
    {
        foreach(Notification notification in m_Notifications)
        {
            if(notification.m_Name == _name)
            {
                GameObject GO = Instantiate(m_NotificationPrefab, m_NotificationsContainer);
                string content = notification.m_Content;
                for(int i = 0; i < replacements.Length; i++)
                {
                    content = content.Replace("{" + i.ToString() + "}", replacements[i]);
                }
                Debug.Log(content);
                GO.GetComponentInChildren<Text>().text = content;
            }
        }
    }
}
