using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMng : MonoBehaviour
{
    public GameObject HP;
    public GameObject Die;
    public GameObject Color;
    public GameObject Key;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HPUpdate();
        KeyCheck();
    }

    public void HPUpdate()
    {
        switch (GameManager.Instance.PlayerHP)
        {
            case 3:
                HP.transform.GetChild(0).gameObject.SetActive(true);
                HP.transform.GetChild(1).gameObject.SetActive(true);
                HP.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 2:
                HP.transform.GetChild(0).gameObject.SetActive(true);
                HP.transform.GetChild(1).gameObject.SetActive(true);
                HP.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                HP.transform.GetChild(0).gameObject.SetActive(true);
                HP.transform.GetChild(1).gameObject.SetActive(false);
                HP.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 0:
                HP.transform.GetChild(0).gameObject.SetActive(false);
                HP.transform.GetChild(1).gameObject.SetActive(false);
                HP.transform.GetChild(2).gameObject.SetActive(false);
                break;
        }
    }
    public IEnumerator PlayerDieUI()
    {
        yield return new WaitForSeconds(1);
        Die.SetActive(true);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(GameManager.Instance.Stage);
    }

    public void ColorUIUpdate()
    {
        for(int i = 0; i < 3; i++)
        {
            
            switch (GameManager.Instance.PColors[i])//Á¶°Ç
            {
                case GameManager.E_Color.Blue:
                    Colorswitch(new Color(0, 0, 255, 1), i);
                    break;
                case GameManager.E_Color.Yellow:
                    Colorswitch(new Color(255, 255, 0, 1), i);
                    break;
                case GameManager.E_Color.Sky:
                    Colorswitch(new Color(0, 255, 255, 1), i);
                    break;
                case GameManager.E_Color.Red:
                    Colorswitch(new Color(255, 0, 0, 1), i);
                    break;
                case GameManager.E_Color.Purple:
                    Colorswitch(new Color(255, 0, 255, 1), i);
                    break;
                case GameManager.E_Color.Green:
                    Colorswitch(new Color(0, 255, 0, 1), i);
                    break;
                case GameManager.E_Color.Null:
                    Colorswitch(new Color(0, 0, 0, 0), i);
                    break;
            }
        }
    }

    public void KeyCheck()
    {
        if(GameManager.Instance.Key)
        {
            Key.transform.GetChild(0).gameObject.SetActive(true);
            Key.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            Key.transform.GetChild(1).gameObject.SetActive(true);
            Key.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void Colorswitch(Color ChangeColor, int i)
    {
        if (GameManager.Instance.PColorsCount[i] == 1)
        {
            Color.transform.GetChild(i).GetComponent<Image>().color = ChangeColor;
            Color.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
            Color.transform.GetChild(i).GetChild(1).GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
        else if (GameManager.Instance.PColorsCount[i] == 2)
        {
            Color.transform.GetChild(i).GetComponent<Image>().color = ChangeColor;
            Color.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = ChangeColor;
            Color.transform.GetChild(i).GetChild(1).GetComponent<Image>().color = new Color(0,0,0,0);
        }
        else if (GameManager.Instance.PColorsCount[i] == 3)
        {
            Color.transform.GetChild(i).GetComponent<Image>().color = ChangeColor;
            Color.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = ChangeColor;
            Color.transform.GetChild(i).GetChild(1).GetComponent<Image>().color = ChangeColor;
        }
        else if (GameManager.Instance.PColorsCount[i] == 0)
        {
            Color.transform.GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, 0);
            Color.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
            Color.transform.GetChild(i).GetChild(1).GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }
}
