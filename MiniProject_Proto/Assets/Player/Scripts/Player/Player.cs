using System.Collections;
using UnityEngine;

//�ڵ� �߰�
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{
    //���� �ӵ� ����(Unity ������ ���� public).
    private float normalspeed; //ĳ���� �ӵ�
    public float moveSpeed = 5; //�⺻ �ӵ�

    Camera viewCamera;

    PlayerController controller;
    GunController gunController;

    public Animator animator;

    bool isPause;

    #region Ư�� �⵿
    //Ư�� �⵿ ���� ����
    public enum SelectDodge { SPR, SLD, BLK };
    public static SelectDodge selectDodge;

    //���� �̵�
    public float blinkDis = 10f; //���� �̵� �Ÿ�
    private bool can_blink = true; //���� �̵� ���� ����
    public float blinkcooldown = 5.0f;

    //�޸���
    public float sprintSpeed = 7f; //�޸��� �ӵ�
    public float stamina; //���׹̳�(�ִ� �޸��� ���� �ð�)
    public float staminaMax = 5f;

    //�����̵� ����
    public Rigidbody player; //�����̵� �� ��ü
    public float slidedis = 7f; //�����̵� �� �̵��ϴ� �Ÿ� 
    public float slidecooldown = 5.0f; //�����̵� ���� ��� �ð�
    private bool can_Slide = true; //�����̵� Ȱ��ȭ ����
    #endregion

    #region �̵� ����Ʈ
    public enum MoveSpt { NON, SPD };
    public static MoveSpt moveSpt;
    #endregion

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;

        Vector3 moveInput = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //�Է�

        stamina = staminaMax; //�ִ� ���׹̳� ����
        player = GetComponent<Rigidbody>(); //�÷��̾� �� ��������

        if (moveSpt == MoveSpt.SPD) //���ӱ� ���ý� �⺻ �̵�, Ư�� �⵿ 1.2��
        {
            normalspeed = moveSpeed * 1.2f;
            sprintSpeed *= 1.2f;
            slidedis *= 1.2f;
            blinkDis *= 1.2f;
        }
        else if (moveSpt == MoveSpt.NON)
        {
            normalspeed = moveSpeed;
        }

        switch (selectDodge)
        {
            case SelectDodge.SPR:
                break;
            case SelectDodge.SLD:
                break;
            case SelectDodge.BLK:
                break;
        }

        
        if (selectDodge == SelectDodge.SPR) //�޸��� ���ý�
        {
            SteminaUI.instance.STEMINA = stamina;
            SteminaUI.instance.MAXSTEMINA = staminaMax;

            CoolDownUI.instance.coolDownUi.enabled = false;
        }

        if ((selectDodge == SelectDodge.SLD) || (selectDodge == SelectDodge.BLK))
        {
            SteminaUI.instance.SteminaGUI.gameObject.SetActive(false);
        }

       

        isPause = false;        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * normalspeed;

        animator.SetFloat("h", Input.GetAxis("Horizontal"));
        animator.SetFloat("v", Input.GetAxis("Vertical"));

        //ȭ���� ���콺 ��ġ ��ȯ
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (dead == false && isPause == false) 
        {
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                controller.LookAt(point);
            }

            controller.Move(moveVelocity);

            //��� ���
            if (Input.GetMouseButton(0)) //�� Ŭ����
            {
                gunController.Shoot();
            }

            //Ư�� ����
            if (Input.GetMouseButtonDown(1))  //�� Ŭ����
            {
                gunController.SubShoot();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                gunController.Reload();
                animator.SetTrigger("Reload");
            }

            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                if (isPause == false)
                {
                    isPause = true;
                    Time.timeScale = 0; //Za Warudo!
                    GameManager.instance.PauseUI.SetActive(true);//GUI ����
                }
                else if (isPause == true) 
                {
                    isPause = false;
                    Time.timeScale = 1; //�ð��� �ٽ� �帣�� �����Ѵ�.
                    GameManager.instance.PauseUI.SetActive(true);//GUI ��.
                }

            }


            //Ư�� �⵿

            if (selectDodge == SelectDodge.SPR) //�޸��� ���ý�
            {
                SteminaUI.instance.STEMINA = stamina;
                SteminaUI.instance.MAXSTEMINA = staminaMax;

                CoolDownUI.instance.slideIcon.enabled = false;
                CoolDownUI.instance.blinkIcon.enabled = false;

                if ((Input.GetKey(KeyCode.Space) && stamina > 0) && moveInput != Vector3.zero) //
                {
                    animator.SetFloat("h", Input.GetAxis("Horizontal") * 2);
                    animator.SetFloat("v", Input.GetAxis("Vertical") * 2);
                    sprint(); //�޸��� Ȱ��ȭ
                }
                else if ((Input.GetKey(KeyCode.Space) && stamina < 0) || !Input.GetKey(KeyCode.Space))
                {
                    normalspeed = moveSpeed;//���� �ӵ����
                }
                if (!Input.GetKey(KeyCode.Space))
                {
                    recoverStamina();
                }

            }


            if ((selectDodge == SelectDodge.SLD) || (selectDodge == SelectDodge.BLK))
            {

                SteminaUI.instance.SteminaGUI.enabled = false;

                switch (selectDodge)
                {
                    case SelectDodge.SLD: //�����̵� ���ý�
                        CoolDownUI.instance.blinkIcon.enabled = false;
                        if (Input.GetKeyDown(KeyCode.Space) && can_Slide == true)
                        {
                            slideAct();
                        }
                        break;

                    case SelectDodge.BLK: //�����̵� ���ý�
                        CoolDownUI.instance.slideIcon.enabled = false;
                        if (Input.GetKeyDown(KeyCode.Space) && can_blink == true)
                        {
                            blinkAct();
                        }
                        break;
                }

            }
        }
        else if (dead == true) 
        {
            animator.SetTrigger("Dead");

            

            GameManager.instance.gameOverUI.SetActive(true);
            GameManager.instance.PlayGUI.SetActive(false);
        }
    }

    #region �����̵�

    private void blinkAct()
    {
        CoolDownUI.instance.COOLDOWN = blinkcooldown; //��Ÿ�� ���� ����

        can_blink = false;

        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        transform.position += dir * blinkDis; //���� ��ġ �̵���(���� �̵�)

        if (dir == Vector3.zero)
        {
            transform.position += player.transform.forward * blinkDis; //Ű���� �Է��� ���ٸ� ���콺�� �ٶ󺸴� �������� �̵�
        }

        StartCoroutine(CoolDownBlink());
    }

    IEnumerator CoolDownBlink()
    {
        CoolDownUI.instance.StartCoroutine("CoolDownBlink", blinkcooldown);
        
        yield return new WaitForSeconds(blinkcooldown); //5���� ���� ���� ����;

        can_blink = true;
        Debug.Log("���� �̵� �غ� �Ϸ�!");
    }
    #endregion

    #region �޸���


    //�޸��� ����
    private void sprint()
    {
        SteminaUI.instance.STEMINA = stamina;
        SteminaUI.instance.MAXSTEMINA = staminaMax;

        normalspeed = sprintSpeed;
        stamina -= Time.deltaTime; //������ �ִ� ���� ���� �Ҹ�
    }

    private void recoverStamina() //���׹̳� 
    {
        stamina += Time.deltaTime; //õõ�� ȸ��

        if (stamina > staminaMax)
        {
            stamina = staminaMax; //�ִ� ���׹̳��� ���� �ʵ��� ����
        }
    }
    #endregion

    #region �����̵�
    //�����̵� ����

    private void slideAct()
    {
        CoolDownUI.instance.COOLDOWN = slidecooldown; //��Ÿ�� ���� ����

        can_Slide = false;

        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        player.AddForce(dir * slidedis, ForceMode.Impulse); //�о���� �̵�

        if (dir == Vector3.zero)
        {
            player.AddForce(player.transform.forward * slidedis, ForceMode.Impulse); //Ű���� �Է��� ���ٸ� ���콺�� �ٶ󺸴� �������� �̵�
        }

        StartCoroutine(CoolDownSlide()); //�ڷ�ƾ�� �̿��� ��Ÿ�� ����
    }


    IEnumerator CoolDownSlide()
    {
        CoolDownUI.instance.StartCoroutine("CoolDownSlide", slidecooldown);

        yield return new WaitForSeconds(slidecooldown); 
        
        can_Slide = true;
        Debug.Log("�����̵� �غ� �Ϸ�!");
    }
    #endregion


    
}