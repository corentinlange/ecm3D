using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSMenuScript : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public CanvasGroup gpsMenuUI;
   
    private GameObject navMeshPlayer;
    private NavMeshController navMeshController;

    [SerializeField]
    private Transform[] destinations;

    private void Awake()
    {
        navMeshPlayer = GameObject.FindGameObjectWithTag("NavPlayer");
        navMeshController = navMeshPlayer.GetComponent<NavMeshController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            };
        }
    }

    public void Resume()
    {
        gpsMenuUI.alpha = 0;
        gpsMenuUI.blocksRaycasts = false;
        gpsMenuUI.interactable = false;
        GameIsPaused = false;
        Time.timeScale = 1f;
        PlayerController.singleton.isUIopen = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Pause()
    {
        gpsMenuUI.alpha = 1;
        gpsMenuUI.blocksRaycasts = true;
        gpsMenuUI.interactable = true;
        GameIsPaused = true;
        Time.timeScale = 0f;
        PlayerController.singleton.isUIopen = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void DirectionButton(Transform objectToNav)
    {
        Resume();

        navMeshController.SetDestination(objectToNav.transform.position);
        Debug.Log("Help menu...");


    }
}
