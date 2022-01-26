using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();//エスケープボタンが押されたらアプリが終わる
            Debug.Log("アプリが終了しました");
        }
    }
}
