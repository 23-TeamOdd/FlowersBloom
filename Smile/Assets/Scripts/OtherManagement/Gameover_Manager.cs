/*
*���ӿ��� ���� ���������� �����ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*-ȭ�� Ŭ�� �� �� ��ȯ
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover_Manager : MonoBehaviour
{
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
