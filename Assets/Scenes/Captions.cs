using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Captions : MonoBehaviour
{
    public Text Text;
    private bool EndGame;
    private float timer = 3;
   
    // Update is called once per frame
    void Update()
    {
        if (Text.rectTransform.anchoredPosition.y < 0)
        {
            Text.rectTransform.anchoredPosition = new Vector3(0, Text.rectTransform.anchoredPosition.y + 1f, 0);
        }
        else if (Text.rectTransform.anchoredPosition.y > 0)
        {
            Text.rectTransform.anchoredPosition = new Vector3(0, Text.rectTransform.anchoredPosition.y - 1f, 0);
        }
        else
        {
            if (EndGame == false)
            {
                timer -= Time.deltaTime;
                var seconds = timer % 60;
                if (timer < 0 )
                {
                    EndGame = true;
                }  
            }
        }
        if (EndGame == true && Text.color.a > 0)
        {
            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, Text.color.a - 0.01f);      
        }

        if (Text.color.a < 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
