/*
*����ȭ�� ���� �� ĳ���Ϳ� ���� UI ������ �����ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*-ĳ���Ϳ� ���� UI�� �����Ѵ�
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUISceneInitSet : MonoBehaviour
{
    public GameObject MainUI_Character;

    public Sprite �ε鷹_����;
    public Sprite ƫ��_����;

    void Awake()
    {
        if(UniteData.Selected_Character=="Dandelion")
        {
            MainUI_Character.GetComponent<Image>().sprite = �ε鷹_����;
        }
        else if(UniteData.Selected_Character=="Tulip")
        {
            MainUI_Character.GetComponent<Image>().sprite = ƫ��_����;
        }
    }
}
