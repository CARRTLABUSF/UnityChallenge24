using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void ChangeScene(string sceneName){
        if (sceneName == "BetterGame"){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        SceneManager.LoadScene(sceneName);
    }

    public void OnExit(){
        Application.Quit();
    }
}
