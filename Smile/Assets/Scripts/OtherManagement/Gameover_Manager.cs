/*
*게임오버 씬을 통합적으로 관리하는 스크립트
*
*구현 목표
*-화면 클릭 시 씬 전환
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Gameover_Manager : MonoBehaviour
{
    void Start()
    {
        //전역변수 초기화
        UniteData.notePoint = 2; // 기회 포인트
        UniteData.lifePoint = 3; // 목숨 포인트
    }
    public void GotoMain()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RetryGame()
    {
        UniteData.ReStart = true;
        SceneManager.LoadScene("Play");
    }
}
