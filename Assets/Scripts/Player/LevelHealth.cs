using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHealth : MonoBehaviour
{
    private bool flag = (true);
    public int levelHealth = 100;
    //[Header("Индикатор здоровья")] public Text text;

    private void FixedUpdate()
    {
        
        if (flag)
        {
            //text.GetComponent<Text>().text = "Уровень здоровья" + levelHealth + "%";
            if(levelHealth >= 100)
            {
                levelHealth = 100;
            }
            if(levelHealth <= 0)
            {
                // text.GetComponent<Text>().text = "Вы умерли";
                //Destroy(gameObject);
                
                flag = false;
            }
        }
    }
    
    


}
