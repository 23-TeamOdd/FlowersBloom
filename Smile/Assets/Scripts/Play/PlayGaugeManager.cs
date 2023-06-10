/*
*play ������ ������ ��ô���� ǥ���ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*�� �÷��̾��� ���� ������ ����
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGaugeManager : MonoBehaviour
{
    public Slider slider;

    private float maxTime;

    // Start is called before the first frame update
    void Start()
    {
        //access deny about slider controll by player
        slider.interactable = false;

        //���̵��� ���� �ִ� �ð� ����
        switch (UniteData.Difficulty)
        {
            case 1:
                maxTime = 71f;
                break;
            case 2:
                maxTime = 90f;
                break;
            case 3:
                maxTime = 88f;
                break;
            default:
                maxTime = 71f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(UniteData.Play_Scene_Time);
        slider.value = UniteData.Play_Scene_Time / maxTime;
    }
}
