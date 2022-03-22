using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    [SerializeField]
    Camera m_StartCamera;
    
    [SerializeField]
    Camera m_MainCamera;

    CanvasGroup m_Panel;
    float m_StartYRot;

    private bool m_HasStarted = false;

    private void Start() {
        m_Panel = GetComponent<CanvasGroup>();
    }

    private void Update() {
        if(!m_HasStarted){
            // Todo : rotation caméra sur l'axe y 
        }
    }
    
    public void OnPlayClicked()
    {
        // StartCoroutine(ChangeScene("Home",0,"Main",1));
        m_HasStarted = true;
        m_MainCamera.depth = 2;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerController.singleton.isUIopen = false;
        m_Panel.alpha = 0;
    }

    public void OnHelpClicked()
    {
        
    }

    public void OnLeaveClicked()
    {
        Application.Quit();
    }

    IEnumerator ChangeScene(string previousSceneName, int previousScene,string newSceneName , int newScene) { 
        SceneManager.LoadScene(newScene);
        var scene = SceneManager.GetSceneByName(newSceneName);
        Debug.Log(scene);
        while (!scene.isLoaded) {
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newSceneName));
        var oldScene = SceneManager.GetSceneByName(previousSceneName);
        if (oldScene.IsValid()) {
            yield return SceneManager.UnloadSceneAsync(previousScene);
        }
    }

    // IEnumerator FadeIn(){
    //     FadeAnimator.SetTrigger("FadeIn");
    //     yield return new WaitForSeconds(1f);
    // }
}
