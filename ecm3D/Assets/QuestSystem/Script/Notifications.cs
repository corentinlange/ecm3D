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

    public void ShowNotification(string _name)
    {
        foreach(Notification notification in m_Notifications)
        {
            if(notification.m_Name == _name)
            {
                GameObject GO = Instantiate(m_NotificationPrefab, m_NotificationsContainer);
                GO.GetComponentInChildren<Text>().text = notification.m_Content;
            }
        }
    }
}
