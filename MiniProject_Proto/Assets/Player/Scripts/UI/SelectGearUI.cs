using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectGearUI : MonoBehaviour
{
    public static SelectGearUI instance; //단독 인스턴스화

    public void Awake()
    {
        SelectGearUI.instance = this;
    } //단독

    #region 주무기 선택
    public void selectMainHG() 
    {
        Gun.selectedMain = Gun.SelectedMain.HG;
    }

    public void selectMainSMG()
    {
        Gun.selectedMain = Gun.SelectedMain.SMG;
    }

    public void selectMainAR()
    {
        Gun.selectedMain = Gun.SelectedMain.AR;
    }

    public void selectMainSR()
    {
        Gun.selectedMain = Gun.SelectedMain.SR;
    }

    #endregion

    #region 부무기 선택
    public void selectSubBL()
    {
        Gun.selectedSub = Gun.SelectedSub.BL;
    }

    public void selectSubSG()
    {
        Gun.selectedSub = Gun.SelectedSub.SG;
    }

    public void selectSubGL()
    {
        Gun.selectedSub = Gun.SelectedSub.GL;
    }

    public void selectSubRL()
    {
        Gun.selectedSub = Gun.SelectedSub.RL;
    }

    #endregion

    #region 회피형태 선택

    public void selectDodgeSPR()
    {
        Player.selectDodge = Player.SelectDodge.SPR;
    }

    public void selectDodgeSLD()
    {
        Player.selectDodge = Player.SelectDodge.SLD;
    }

    public void selectDodgeBLK()
    {
        Player.selectDodge = Player.SelectDodge.BLK;
    }

    #endregion

    #region 보조장비 선택
    public void selectSptAtk() //공격 부스트
    {
        Gun.selectSpt = Gun.SelectSpt.ATK;
        Player.moveSpt = Player.MoveSpt.NON;
    }

    public void selectSptMag() //총탄량 부스트
    {
        Gun.selectSpt = Gun.SelectSpt.MAG;
        Player.moveSpt = Player.MoveSpt.NON;
    }

    public void selectSptRel() //재장전 부스트
    {
        Gun.selectSpt = Gun.SelectSpt.REL;
        Player.moveSpt = Player.MoveSpt.NON;
    }

    public void selectSptSpd() //이동 부스트
    {
        Gun.selectSpt = Gun.SelectSpt.NON;
        Player.moveSpt = Player.MoveSpt.SPD;
    }
    #endregion

    #region 게임 시스템
    public void BacktoLobby() 
    {
        SceneManager.LoadSceneAsync(0);
        //처음화면으로 돌아가는 메소드
    }

    public void StartMission() 
    {
        SceneManager.LoadSceneAsync(2);
        //게임화면으로 이동하는 메소드
    }
    #endregion

    private void Start()
    {
    }
}
