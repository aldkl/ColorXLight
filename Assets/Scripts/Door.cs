using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    public override void Interact()
    {
        if(GameManager.Instance.Key)
        {
            GameManager.Instance.NextStage();

            SceneManager.LoadScene(GameManager.Instance.Stage);
        }

    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
