using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{


    private Animator anim;
    private float xVal;
    private bool isWalking;

    private Vector2 boxSize = new Vector2(0.1f, 1f);

    private Transform Playertransform;
    private Rigidbody2D PlayerRigidbody2D;
    private BoxCollider2D PlayerCollider2D;
    private BoxCollider2D AttackCollider2D;
    private SpriteRenderer PlayerRenderer;

    public float Movespeed;
    public float JumpPorce;
    public Transform Staff;
    public Transform Staff2;

    //public Vector3 PlayerPos;

    [HideInInspector]
    public bool bIsGrounded;
    public bool bJumping;
    public bool bDuck;
    public bool bAttack;
    public bool bAbsord;
    public bool bDamage;
    public bool bJumpStart;
    public bool bPower;
    public bool bDie;

    public LayerMask GroundLayer;

    public int CColorInt;

    float CurHorizontal;

    public AudioClip absorbS;
    public AudioClip HitS;
    public AudioClip AtkS;
    public AudioClip DeathS;
    public AudioClip WalkS;

    AudioSource audioSource;


    [SerializeField] ScreenShake cameraS;


    private void Start()
    {
        Playertransform = GetComponent<Transform>();
        PlayerRigidbody2D = GetComponent<Rigidbody2D>();
        PlayerCollider2D = GetComponent<BoxCollider2D>();
        AttackCollider2D = transform.GetChild(3).GetComponent<BoxCollider2D>();
        PlayerRenderer = GetComponent<SpriteRenderer>();
        
        audioSource = GetComponent<AudioSource>();

        bDamage = false;
        bIsGrounded = false;
        bPower = false;
        bDie = false;
        anim = GetComponent<Animator>();
        AttackCollider2D.enabled = false;
        //PlayerPos = transform.position + new Vector3(0, 1.6f, 0);
    }

    void Update()
    {
        if (!bDie)
        {
            //PlayerPos = Playertransform.position;// + new Vector3(0, 1.6f, 0);
            if (!bAttack && !bDamage && !bAbsord && !bJumpStart)
            {
                CurHorizontal = Input.GetAxisRaw("Horizontal");
                Playertransform.Translate(Movespeed * CurHorizontal * Time.deltaTime, 0, 0);
            }

            IsGround();
            if (CurHorizontal > 0)
            {
                PlayerRenderer.flipX = true;
                anim.SetBool("bMove", true);
                Staff.localPosition = new Vector3(3.12f, 5.8f, 0);
                Staff2.localPosition = new Vector3(-2.29f, 4.64f, 0);
                AttackCollider2D.transform.localPosition = new Vector3(0.64f, 0, 0);

            }
            else if (CurHorizontal < 0)
            {
                PlayerRenderer.flipX = false;
                anim.SetBool("bMove", true);
                Staff.localPosition = new Vector3(-3.12f, 5.8f, 0);
                Staff2.localPosition = new Vector3(2.29f, 4.64f, 0);
                AttackCollider2D.transform.localPosition = new Vector3(-6.64f, 0, 0);

            }
            else
            {
                anim.SetBool("bMove", false);
            }
            if (Input.GetKeyDown(KeyCode.Z))//��ȣ�ۿ�
            {
                if (!anim.GetBool("bAbsord") && CurHorizontal == 0)
                {
                    anim.SetBool("bAbsord", true);
                    bAbsord = true;
                }
            }

            if (Input.GetKey(KeyCode.DownArrow))//���̱�
            {
                if (!anim.GetBool("bDuck"))
                {
                    Movespeed /= 2f;
                    PlayerCollider2D.offset = new Vector2(PlayerCollider2D.offset.x, 2.34f);
                    PlayerCollider2D.size = new Vector2(PlayerCollider2D.size.x, 4.24f);
                    bDuck = true;
                }
            }
            else
            {
                if (anim.GetBool("bDuck"))
                {
                    Movespeed *= 2f;
                    PlayerCollider2D.offset = new Vector2(PlayerCollider2D.offset.x, 3.40f);
                    PlayerCollider2D.size = new Vector2(PlayerCollider2D.size.x, 6.34f);
                    bJumping = true;
                    bDuck = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))//����
            {
                if (bIsGrounded && !anim.GetBool("bDuck"))
                {
                    if (!anim.GetBool("bJumpUp"))
                    {
                        anim.SetBool("bJumpUp", true);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.X))//����
            {
                if (bIsGrounded && !anim.GetBool("bDuck"))
                {
                    if (!anim.GetBool("bAttack"))
                    {
                        anim.SetBool("bAttack", true);
                        bAttack = true;
                    }
                }
            }
            AnimatorUpdate();
        }
    }

    public void IsGround()
    {
        if(IsPlatform())
        {
            if(bIsGrounded == false)
            {
                bIsGrounded = true;
                anim.SetBool("bIsGrounded", true);
                anim.SetBool("bJumpUp", false);
                anim.SetBool("bJumpDown", false);
            }
        }
        else
        {
            bIsGrounded = false;
            anim.SetBool("bIsGrounded", false);
        }
    }
    public void JumpOn()
    {
        PlayerRigidbody2D.AddForce(Vector2.up * JumpPorce, ForceMode2D.Impulse);
    }

    public bool IsPlatform()
    {
        RaycastHit2D hit = Physics2D.BoxCast(PlayerCollider2D.bounds.center, PlayerCollider2D.bounds.size, 0, Vector2.down, 0.1f, GroundLayer);

        if(hit.collider != null && hit.collider.CompareTag("MovingGround"))
        {
            Playertransform.parent = hit.transform;
        }
        else
        {
            Playertransform.parent = null;
        }
            return hit.collider != null;
    }
    private void AnimatorUpdate()
    {
        anim.SetFloat("fHor", CurHorizontal);


        if (!bIsGrounded)//���࿡ ���� ������� ���� ��
        {
            if (PlayerRigidbody2D.velocity.y < 0f)//���� �����Ӷ� �� �̵��Ұ��� ���ص� ���� ��
            {
                if (anim.GetBool("bJumpUp"))
                {
                    anim.SetBool("bJumpUp", false);
                    anim.SetBool("bJumpDown", true);
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.DownArrow))//���̱�
            {
                if (!anim.GetBool("bDuck"))
                {
                    anim.SetBool("bDuck", true);
                }
            }
            else
            {
                if (anim.GetBool("bDuck"))
                {
                    anim.SetBool("bDuck", false);
                }
            }
        }


    }

    public void StopMove()
    {
        bJumpStart = true;
    }
    public void StartMove()
    {
        bJumpStart = false;
    }

    public void AtkSound()
    {
        audioSource.PlayOneShot(AtkS, 0.7f);
    }

    public void WalkSound()
    {
        audioSource.PlayOneShot(WalkS, 0.7f);
    }

    public void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.IsInteractable())
                {
                    rc.Interact();
                    audioSource.PlayOneShot(absorbS, 0.7f);

                    return;
                }
            }
        }
    }

    public void AniEndAbsord()
    {
        anim.SetBool("bAbsord", false);
        bAbsord = false;
    }

    public void OffDamage()
    {
        bDamage = false;
        anim.SetBool("bDamage", false);
    }

    public void EndAttack()
    {
        bAttack = false;
        anim.SetBool("bAttack", false);
        AttackCollider2D.enabled = false;
    }

    public void Attack()
    {
        AttackCollider2D.enabled = true;
    }

    public void HitPlayer()
    {
        if (!bDamage && !GameManager.Instance.CheatF4)//�������� �԰��ֳ� üũ && ������ �����ֳ� üũ
        {//���԰��ִ�
            if (GameManager.Instance.PlayerHP > 1)//�ǰ� ������
            {
                bDamage = true;//�������� �԰��ִ�
                anim.SetBool("bDamage", true);//�ǰݾִϸ��̼� ����
                GameManager.Instance.PlayerHP--;//�Ǹ� ����
                bAttack = false;//�� ����� �ϰ��־������� ����� �� ����.
                bAbsord = false;
                bJumpStart = false;
                bDuck = false;
                anim.SetBool("bAttack", false);
                anim.SetBool("bAbsord", false);
                anim.SetBool("bJumpUp", false);
                anim.SetBool("bDuck", false);
                bPower = true;//������ ���ش�
                StartCoroutine(Blink(0.1f));//������
                StartCoroutine(cameraS.StartShake());
                audioSource.PlayOneShot(HitS, 0.7f);
                if(Movespeed <= 4)
                {
                    Movespeed *= 2f;
                }
            }
            else//�׾�����
            {
                Die();
            }
        }
    }

    public void Die()
    {
        bDie = true;
        anim.SetTrigger("tDie");
        GameManager.Instance.PlayerHP--;//�Ǹ�����
        StartCoroutine(GameManager.Instance.UImng.GetComponent<UIMng>().PlayerDieUI());
        audioSource.PlayOneShot(DeathS, 0.7f);
    }

    IEnumerator Blink(float waitTime)//������ 
    {
        int Count = 5;//��� ��������
        for (int i = 0; i < Count; i++)
        {
            PlayerRenderer.color = new Color(255, 255, 255, 0.1f);
            yield return new WaitForSeconds(waitTime);
            PlayerRenderer.color = new Color(255, 255, 255, 1f);
            yield return new WaitForSeconds(waitTime);
        }
        bPower = false;
    }
}