using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region 주무장 변수
    public enum SelectedMain { HG, SMG, AR, SR }; //주 무장 종류
    public static SelectedMain selectedMain; //현재 자신이 사용하는 

    public Transform muzzle; //발사 위치
    public Projectile projectile; //총알

    public Snipe snipe; //저격총용 총알

    public float msBetweenShots = 100; //연사 간격
    public float muzzleVelocity = 35; //총알 속도
    public int maxBullet; //잔탄수
    public float reloadTime; //재장전 시간

    float nextShotTime;

    bool canShoot; //발사 가능?
    int leftBullet; //현재 잔탄수
    public bool isreload; //재장전 여부

    #endregion
    #region 주무장 메소드
    public void Shoot() //사격
    {
        if (!isreload && Time.time > nextShotTime && leftBullet > 0)
        {
            leftBullet--; //탄환 소모
            nextShotTime = Time.time + msBetweenShots / 1000; //다음 탄환 간격


            if (selectedMain == SelectedMain.SR) //저격총용 탄환
            {
                Snipe newSnipe = Instantiate(snipe, muzzle.position, muzzle.rotation); //발사체 위치 설정
                newSnipe.SetSpeed(muzzleVelocity);
            }
            else //일반 탄환
            {
                Projectile newprojectile = Instantiate(projectile, muzzle.position, muzzle.rotation); //발사체 위치 설정
                newprojectile.SetSpeed(muzzleVelocity); //발사체 속도 설정
            }
        }
    }

    public void Reload() //재장전
    {
        if (!isreload && leftBullet != maxBullet)
        {            
            StartCoroutine(ReloadMove());
        }
    }

    IEnumerator ReloadMove() //재장전 매커니즘;
    {
        isreload = true;      
        yield return new WaitForSeconds(reloadTime);

        isreload = false;
        leftBullet = maxBullet;
    }
    #endregion


    #region 부무장 변수

    public enum SelectedSub { BL, SG, GL, RL }; //보조무장 종류

    public static SelectedSub selectedSub; //현재 자신이 사용하는 보조무장

    private bool can_Sub = true; //발사 가능 여부
    public float subDelay = 3.0f; //1번 발사하고 딜레이

    public int subStock; //현재 스톡수
    public int maxStock = 3; //최대 스톡수
    public float chargeTime = 5.0f; //스톡 충전 시간

    #region 산탄총
    public Bayonet bayonet;
    #endregion

    #region 산탄총
    public Shotgun shotgun;
    #endregion

    #region 수류탄
    public Granade granade;
    public float range = 10f;
    #endregion

    #region 미사일
    public Missile missile;
    public float disMiss = 30f; //미사일 유효사거리
    #endregion


    #endregion
    #region 부무장 메소드
   
    public void SubShoot() //부 무장 발사
    {
        if (subStock > 0 && can_Sub == true) //여유 스톡이 있을때만 사용가능
        {
            WeaponUI.instance.DELAY = subDelay;
            subStock--;

            //기능 수행

            if (selectedSub == SelectedSub.BL) 
            {
                Bayonet newBayonet = Instantiate(bayonet, muzzle.position, muzzle.rotation);
                newBayonet.pushBack();
            }
            else if (selectedSub == SelectedSub.SG)
            {
                Shotgun newShotgun = Instantiate(shotgun, muzzle.position, muzzle.rotation);
                newShotgun.SgShot();
            }
            else if (selectedSub == SelectedSub.GL) //유탄 선택시
            {
                Granade newGranade = Instantiate(granade, muzzle.position, muzzle.rotation);
                newGranade.GetComponent<Rigidbody>().AddForce(muzzle.forward * range, ForceMode.Impulse);
            }
            else if (selectedSub == SelectedSub.RL) //미사일 선택시
            {
                Missile newMissile = Instantiate(missile, muzzle.position, muzzle.rotation);
                newMissile.SetSpeed(muzzleVelocity);
            }

            StartCoroutine(SubDelay());
        }
    }

    IEnumerator SubDelay()
    {
        can_Sub = false;

        yield return new WaitForSeconds(subDelay);

        can_Sub = true;
    }
    #endregion

    #region 무기 보조
    public enum SelectSpt { NON, ATK, MAG, REL };
    public static SelectSpt selectSpt;
    #endregion

    public void Start()
    { 
        switch (selectedMain)
        {
            case SelectedMain.HG:
                maxBullet = 8;
                reloadTime = 1.2f;
                msBetweenShots = 150;
                projectile.damage = 3.0f;
                break;
            case SelectedMain.SMG:
                maxBullet = 25;
                reloadTime = 1.5f;
                msBetweenShots = 100;
                projectile.damage = 1.0f;
                break;
            case SelectedMain.AR:
                maxBullet = 40;
                reloadTime = 1.8f;
                msBetweenShots = 200;
                projectile.damage = 2.0f;
                break;
            case SelectedMain.SR:
                maxBullet = 6;
                reloadTime = 2f;
                msBetweenShots = 400;
                projectile.damage = 5.0f;

                break;
        }

        switch (selectSpt) //보조아이템 장착시 수정사항
        {
            case SelectSpt.ATK:
                projectile.damage *= 1.2f;  //공격력 10% 증가
                break;
            case SelectSpt.MAG: //장탄수 20%, 부무장 스택 + 2
                maxBullet = (int)(maxBullet * 1.2f);
                maxStock += 2;
                break;
            case SelectSpt.REL: //재장전 속도, 부무장 재충전속도 80%
                reloadTime *= 0.8f;
                chargeTime *= 0.8f;
                break;
        }

        leftBullet = maxBullet;//시작시 주무기 최대 장전
        subStock = maxStock; //시작시 보조무기 최대 충전

        StartCoroutine(SubCharge());
        WeaponUI.instance.CHARGE = chargeTime;
    }

    private void LateUpdate()
    {
        if (!isreload && leftBullet == 0)
        {
            Reload();
        }

    }

    public void Update()
    {
        //주 무기
        WeaponUI.instance.REMAINMAIN = leftBullet;
        WeaponUI.instance.MAXMAIN = maxBullet;
        WeaponUI.instance.ISRELOAD = isreload;

        WeaponUI.instance.RELOAD = reloadTime;

        switch (selectedMain) 
        {
            case SelectedMain.HG:
                WeaponUI.instance.HG.gameObject.SetActive(true);
                break;
            case SelectedMain.SMG:
                WeaponUI.instance.SMG.gameObject.SetActive(true);
                break;
            case SelectedMain.AR:
                WeaponUI.instance.AR.gameObject.SetActive(true);
                break;
            case SelectedMain.SR:
                WeaponUI.instance.SR.gameObject.SetActive(true);
                break;
        }

        //보조 무기
        WeaponUI.instance.REMAINSUB = subStock;
        WeaponUI.instance.SUBMAX = maxStock;

        switch (selectedSub)
        {
            case SelectedSub.BL:
                WeaponUI.instance.BL.gameObject.SetActive(true);
                break;
            case SelectedSub.SG:
                WeaponUI.instance.SG.gameObject.SetActive(true);
                break;
            case SelectedSub.GL:
                WeaponUI.instance.GL.gameObject.SetActive(true);
                break;
            case SelectedSub.RL:
                WeaponUI.instance.RL.gameObject.SetActive(true);
                break;
        }
    }

    IEnumerator SubCharge()
    {
        if (subStock != maxStock)
        {
            yield return new WaitForSeconds(chargeTime); //조건 만족시 시간차
            subStock++;

            if (subStock > maxStock)
            {
                subStock = maxStock;
            }

            StartCoroutine(nameof(SubCharge), chargeTime);
        }
        else
        {
            yield return new WaitForSeconds(0); //조건 불만족시 연속

            StartCoroutine(nameof(SubCharge)); //조건을 만족 안하면 딜레이 없이 계속 실행
        }
    }
}

