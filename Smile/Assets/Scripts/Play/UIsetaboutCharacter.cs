/*
*play ���� �� ĳ���Ϳ� ���� UI ������ �����ϴ� ��ũ��Ʈ
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

public class UIsetaboutCharacter : MonoBehaviour
{
    public GameObject[] Life_UI_Object;

    public Sprite �ε鷹_�̹���;
    public Sprite ƫ��_�̹���;

    // Start is called before the first frame update
    void Awake()
    {
        if(UniteData.Selected_Character=="Dandelion")
        {
            foreach(GameObject ui in Life_UI_Object)
            {
                ui.GetComponent<Image>().sprite = �ε鷹_�̹���;
            }
        }
        else if(UniteData.Selected_Character=="Tulip")
        {
            foreach (GameObject ui in Life_UI_Object)
            {
                ui.GetComponent<Image>().sprite = ƫ��_�̹���;
            }
        }
    }
}
