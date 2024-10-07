using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //Destroy(this.gameObject);
        }
    }
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public CharacterControl Player;
    public GameObject Oriparticle;
    public GameObject OriDieparticle;
    public GameObject UImng;

    public bool Key = false;
    public int PlayerHP;
    public enum E_Color { Blue, Yellow, Sky, Red, Purple, Green, Null };
    //[HideInInspector]
    public List<GameManager.E_Color> PColors;
    //[HideInInspector]
    public List<int> PColorsCount;
    public int Stage = 1;

    public bool CheatF4;

    public void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.F1))//스테이지 1
        {
            instance.Stage = 1;
            SceneManager.LoadScene(instance.Stage);
        }
        if (Input.GetKeyDown(KeyCode.F2))//스테이지 2
        {
            instance.Stage = 2;
            SceneManager.LoadScene(instance.Stage);
        }
        if (Input.GetKeyDown(KeyCode.F3))//스테이지 3
        {
            instance.Stage = 3;
            SceneManager.LoadScene(instance.Stage);
        }
        if (Input.GetKeyDown(KeyCode.F4))//무적
        {
            if(instance.CheatF4)
            {
                instance.CheatF4 = false;
            }
            else
            {
                instance.CheatF4 = true;
            }
        }
    }
    void Start()
    {
        instance.PlayerHP = 3;
        if(instance.PColors.Count == 0)
        {
            instance.PColors.Add(E_Color.Null);
            instance.PColors.Add(E_Color.Null);
            instance.PColors.Add(E_Color.Null);
            instance.PColorsCount.Add(0);
            instance.PColorsCount.Add(0);
            instance.PColorsCount.Add(0);
        }
        instance.Key = false;
        for (int i = 0; i < 3; i++)
        {
            instance.PColors[i] = E_Color.Null;
            instance.PColorsCount[i] = 0;
        }

        instance.Player = GameObject.Find("Character").GetComponent<CharacterControl>();
        instance.Oriparticle = GameObject.Find("Particle attractor");
        instance.UImng = GameObject.Find("UIMng");
        instance.Stage = SceneManager.GetActiveScene().buildIndex;
    }
    private void Update()
    {
        Cheat();
        ClearStage();
    }

    public void NextStage()
    {
        instance.Stage++;
        instance.Key = false;
        for(int i = 0; i < 3; i++)
        {
            instance.PColors[i] = E_Color.Null;
            instance.PColorsCount[i] = 0;
        }

    }
    public int Count = 0;
    public void ClearStage()//클리어 조건 확인
    {
        switch (instance.Stage)
        {
            case 1:
                Count = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (instance.PColors[i] == E_Color.Null)
                    {
                        break;
                    }
                    if (instance.PColors[i] == E_Color.Blue)
                    {
                        Count += instance.PColorsCount[i];
                    }
                    else if (instance.PColors[i] == E_Color.Red)
                    {
                        Count += instance.PColorsCount[i];
                    }
                    else if (instance.PColors[i] == E_Color.Green)
                    {
                        Count += instance.PColorsCount[i];
                    }
                }
                if(Count == 3)
                {
                    instance.Key = true;
                }
                else
                {
                    instance.Key = false;
                }
                break;
            case 2:
                Count = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (instance.PColors[i] == E_Color.Null)
                    {
                        break;
                    }
                    if (instance.PColors[i] == E_Color.Red)
                    {
                        Count += instance.PColorsCount[i];
                    }
                    else if (instance.PColors[i] == E_Color.Yellow)
                    {
                        Count += instance.PColorsCount[i];
                    }
                    else if (instance.PColors[i] == E_Color.Sky)
                    {
                        Count += instance.PColorsCount[i];
                    }
                }
                if (Count == 6)
                {
                    instance.Key = true;
                }
                else
                {
                    instance.Key = false;
                }
                break;
            case 3:
                for (int i = 0; i < 3; i++)
                {
                    if (instance.PColors[i] == E_Color.Null)
                    {
                        break;
                    }
                    if (instance.PColors[i] == E_Color.Red)
                    {
                        Count += instance.PColorsCount[i];
                    }
                    else if (instance.PColors[i] == E_Color.Purple)
                    {
                        Count += instance.PColorsCount[i];
                    }
                    else if (instance.PColors[i] == E_Color.Yellow)
                    {
                        Count += instance.PColorsCount[i];
                    }
                }
                if (Count == 3)
                {
                    instance.Key = true;
                }
                else
                {
                    instance.Key = false;
                }
                break;
            default :
                break;
        }
    }
}
