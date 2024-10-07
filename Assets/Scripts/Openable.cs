using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Rendering.LWRP;   //OLD VERSIONS LIKE 2018
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

[RequireComponent(typeof(SpriteRenderer))]

public class Openable : Interactable
{

    GameObject Particle;
    GameObject BatDieParticle;
    GameObject Bat;

    public Sprite[] HasColorS = new Sprite[6];
    public Sprite[] nHasColorS = new Sprite[6];

    //    public enum E_Color { Blue, Yellow, Sky, Red, Purple, Green};
    public GameManager.E_Color ObjColor = GameManager.E_Color.Blue;
    public bool bHasColor;

    bool IsCount;
    private SpriteRenderer sRr;


    public override void Interact()
    {

        if (bHasColor)//������ ������
        {
            Particle = Instantiate(GameManager.Instance.Oriparticle, transform.position, Quaternion.identity);//��ƼŬ ����
            Particle.GetComponent<particleAttractorLinear>().target = GameManager.Instance.Player.Staff2;//��ƼŬ Ÿ�� ����
            Particle.GetComponent<ParticleSystem>().Play();//��ƼŬ �÷���
            bHasColor = false;//���� ����
            for (int i = 0; i < GameManager.Instance.PColors.Count; i++)//�÷��̾��� ���� �ִ� �� ī��Ʈ
            {
                if (!IsCount)//���������� ���� �߰����� �ʾ�����
                {
                    if (ObjColor == GameManager.Instance.PColors[i])//���࿡ �� ������Ʈ�� ���� ������ ����������
                    {
                        GameManager.Instance.PColorsCount[i]++;//�� �� �Ѱ� �߰�
                        IsCount = true;//���� �߰��ƴٰ� �˸�
                    }
                    else if (GameManager.Instance.PColors[i] == GameManager.E_Color.Null)//���� ������
                    {
                        GameManager.Instance.PColors[i] = ObjColor;
                        GameManager.Instance.PColorsCount[i]++;//�� �� �Ѱ� �߰�
                        IsCount = true;
                    }
                }
                else
                {
                    break;//������
                }
            }


            transform.GetChild(0).gameObject.SetActive(false);

            StartCoroutine(CreateBat());
            for (int i = 0; i < 6; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            IsCount = false;
            GameManager.Instance.UImng.GetComponent<UIMng>().ColorUIUpdate();
        }
        else
        {
            ColorOn();
            Particle = Instantiate(GameManager.Instance.Oriparticle, GameManager.Instance.Player.Staff.position, Quaternion.identity);
            Particle.GetComponent<particleAttractorLinear>().target = transform;
            Particle.GetComponent<ParticleSystem>().Play();
        }
    }

    public void ColorOn()
    {
        for (int i = 0; i < GameManager.Instance.PColors.Count; i++)//�÷��̾��� ���� �ִ� �� ī��Ʈ
        {
            if (!IsCount)//���������� ���� �߰����� �ʾ�����
            {
                if (ObjColor == GameManager.Instance.PColors[i])//���࿡ �÷��̾ ������Ʈ �÷��� ���� ������ ����������
                {
                    GameManager.Instance.PColorsCount[i]--;//�� �� �Ѱ� ��
                    Debug.Log(GameManager.Instance.PColorsCount[i] + "MI");
                    if (GameManager.Instance.PColorsCount[i] == 0)//���࿡ �� ��������
                    {
                        Debug.Log(GameManager.Instance.PColorsCount[i] + "sdfsdfsdof");
                        GameManager.Instance.PColors[i] = GameManager.E_Color.Null;//iĭ�� ���� Null�� �ٲ۴�
                    }
                    IsCount = true;//���� �����Ǿ��ٰ� �˸�
                }
            }
            else
            {
                bHasColor = true;
                Bat.SetActive(false);
                Bat.GetComponent<BatMove>().BatBox.enabled = false;
                transform.GetChild((int)ObjColor).gameObject.SetActive(true);
                IsCount = false;
                BatDieParticle = Instantiate(GameManager.Instance.OriDieparticle, Bat.transform.position, Quaternion.identity);
                GameManager.Instance.UImng.GetComponent<UIMng>().ColorUIUpdate();
                break;//������
            }
        }

    }

    private void Start()
    {
        IsCount = false;
        sRr = GetComponent<SpriteRenderer>();
        Bat = transform.GetChild(6).gameObject;
        switch (ObjColor)
        {
            case GameManager.E_Color.Blue:
                Bat.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
                break;
            case GameManager.E_Color.Yellow:
                Bat.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0);
                break;
            case GameManager.E_Color.Sky:
                Bat.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
                break;
            case GameManager.E_Color.Red:
                Bat.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                break;
            case GameManager.E_Color.Purple:
                Bat.GetComponent<SpriteRenderer>().color = new Color(255, 0, 255);
                break;
            case GameManager.E_Color.Green:
                Bat.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
                break;

        }
        for (int i = 0; i < 6; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild((int)ObjColor).gameObject.SetActive(true);
    }
    void Update()
    {
        if (bHasColor)
        {
            if (sRr.sprite != HasColorS[(int)ObjColor])
            {
                sRr.sprite = HasColorS[(int)ObjColor];

            }

        }
        else
        {
            if (sRr.sprite != nHasColorS[(int)ObjColor])
            {
                sRr.sprite = nHasColorS[(int)ObjColor];
            }
        }
    }

    void InitBat()
    {
        Bat.SetActive(true);
        Bat.transform.position = transform.position;
        Bat.GetComponent<BatMove>().follow = false;
        Bat.GetComponent<BatMove>().StartCoroutine(Bat.GetComponent<BatMove>().MoveOn());
    }
    IEnumerator CreateBat()
    {
        yield return new WaitForSeconds(1f);
        InitBat();
    }
}