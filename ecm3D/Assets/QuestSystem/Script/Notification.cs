using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField]
    private float m_ShowTime;

    [SerializeField]
    private float m_Speed;

    private RectTransform rectTransform;
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        Invoke("DestroyDelayed", m_ShowTime);
    }

    private void DestroyDelayed()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position += new Vector3(0f, m_Speed, 0f);
    }
}
