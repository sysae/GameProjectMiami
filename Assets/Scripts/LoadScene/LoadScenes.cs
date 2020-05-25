using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("SampleScene");

    }
    public void ExitPressed()
    {
        Application.Quit();
    }

    void Start()
    {
        Cursor.visible = true;
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("Сaptions");
    }
}
