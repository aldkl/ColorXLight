using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuUIMng : MonoBehaviour
{

    public GameObject Title;
    public GameObject Start;
    public GameObject Quit;
    public GameObject Option;

    void Update()
    {
        
    }
    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitBtn()
    {
        Application.Quit();
    }

}
