using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TMP_Dropdown mainSelect; // 드롭다운

    public void Select_Main()
    {
 
        switch (mainSelect.value) 
        {
            case 0:
                Gun.selectedMain = Gun.SelectedMain.HG;
                break;
            
            case 1:
                Gun.selectedMain = Gun.SelectedMain.SMG;
                break;

            case 2:
                Gun.selectedMain = Gun.SelectedMain.AR;
                break;

            case 3:
                Gun.selectedMain = Gun.SelectedMain.SR;
                break;
        }
    }

    #endregion

    #region 부무기 선택
    public TMP_Dropdown subSelect; // 드롭다운

    public void Select_Sub()
    {


        switch (subSelect.value)
        {
            case 0:
                Gun.selectedSub = Gun.SelectedSub.BL;
                break;

            case 1:
                Gun.selectedSub = Gun.SelectedSub.SG;
                break;

            case 2:
                Gun.selectedSub = Gun.SelectedSub.GL;
                break;

            case 3:
                Gun.selectedSub = Gun.SelectedSub.RL;
                break;
        }
    }

    #endregion

    #region 회피형태 선택

    public TMP_Dropdown dodgeSelect; // 드롭다운

    public void Select_dodge()
    {


        switch (dodgeSelect.value)
        {
            case 0:
                Player.selectDodge = Player.SelectDodge.SPR;
                break;

            case 1:
                Player.selectDodge = Player.SelectDodge.SLD;
                break;

            case 2:
                Player.selectDodge = Player.SelectDodge.BLK;
                break;
        }
    }

    #endregion

    #region 보조장비 선택

    public TMP_Dropdown supportSelect; // 드롭다운

    public void Select_Support()
    {


        switch (supportSelect.value)
        {
            case 0:
                Gun.selectSpt = Gun.SelectSpt.ATK;
                Player.moveSpt = Player.MoveSpt.NON;
                break;

            case 1:
                Gun.selectSpt = Gun.SelectSpt.MAG;
                Player.moveSpt = Player.MoveSpt.NON;
                break;

            case 2:
                Gun.selectSpt = Gun.SelectSpt.REL;
                Player.moveSpt = Player.MoveSpt.NON;
                break;
            
            case 3:
                Gun.selectSpt = Gun.SelectSpt.NON;
                Player.moveSpt = Player.MoveSpt.SPD;
                break;
        }
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
