/*
*ĳ���� ����â�� �����ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*-ĳ���� ��ݿ��� �ο�
*-ī�� Ŭ�� �� ����
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Supervisor : MonoBehaviour
{
    public enum Character
    {
        Dandelion,
        Tulip,
        etc1,
        etc2,
        etc3,
        etc4,
        etc5,
        etc6
    }

    public GameObject[] Card;

    // Start is called before the first frame update
    void Start()
    {
        //���� ���� ĳ���� Ȱ��ȭ
        foreach(GameObject gm in Card)
        {
            CheckList(gm);
        }

        //���õ� ĳ���� ���� üũ �� ����
        foreach(GameObject Cname in Card)
        {
            if(Cname.name == UniteData.Selected_Character)
            {
                Debug.Log(Cname.name + "�� �����մϴ�");
            }
        }

    }

    private void CheckList(GameObject character_card)
    {
        Card_Status CS = character_card.GetComponent<Card_Status>();

        //����� Ǯ�� ��Ȳ�̸�
        if (CS.unlocked)
        {
            Debug.Log(character_card.name + " Ȱ��ȭ");
        }
    }

    //ī�带 Ŭ������ �� ó�� 
    public void pressCharacterCard(GameObject character_card)
    {
        Card_Status CS=character_card.GetComponent<Card_Status>();

        //����� Ǯ�� ��Ȳ�̸�
        if(CS.unlocked)
        {
            //���ð����ϰ� �Ѵ�
            UniteData.Selected_Character = character_card.name;
            Debug.Log(UniteData.Selected_Character+"���� �����߽��ϴ�.");
        }
        else
        {
            Debug.Log("��~ ���� �ȵ�~");
        }
    }

}
