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
using UnityEngine.UI;

public class Select_Supervisor : MonoBehaviour
{
    //public GameObject Select_Icon;
    public GameObject[] Card;

    // Start is called before the first frame update
    void Start()
    {
        //���� ���� ĳ���� Ȱ��ȭ
        foreach(GameObject gm in Card)
        {
            CheckList(gm);
        }
    }

    private void Update()
    {
        //���õ� ĳ���� ���� üũ �� ����
        //foreach (GameObject Cname in Card)
        //{
        //    if (Cname.name == UniteData.Selected_Character)
        //    {
        //        Select_Icon.transform.position = Cname.transform.position;
        //    }
        //}
    }

    private void CheckList(GameObject character_card)
    {
        Card_Status CS = character_card.GetComponent<Card_Status>();

        //Select �̹��� Ȱ��ȭ
        if(UniteData.Selected_Character == character_card.name) character_card.transform.GetChild(0).gameObject.SetActive(true);

        //��� ���� ����
        if(character_card.name == "Tulip") unlock_Tulip(CS, character_card);
        if(character_card.name == "ForgetMeNot") unlock_ForgetMeNot(CS, character_card);

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
            //Ȱ�� Select Card ����
            foreach (GameObject Cname in Card)
            {
                if (Cname.name == UniteData.Selected_Character)
                {
                    Cname.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            character_card.transform.GetChild(0).gameObject.SetActive(true);
            //���ð����ϰ� �Ѵ�
            UniteData.Selected_Character = character_card.name;
            Debug.Log(UniteData.Selected_Character+"���� �����߽��ϴ�.");
        }
        else
        {
            Debug.Log("��~ ���� �ȵ�~");
        }

        UniteData.SaveUserData();
    }

    private void unlock_Tulip(Card_Status CS, GameObject Card)
    {
        //Easy ��带 Ŭ���� ���� ��
        if (CS.isEasyClear == (UniteData.GameClear[0] == 1) && CS.isEasyClear) 
        {
            //��� ����
            CS.unlocked = true;

            Image image = Card.GetComponent<Image>();
            image.sprite = CS.Unlock_image;
        }
    }

    private void unlock_ForgetMeNot(Card_Status CS, GameObject Card)
    {
        //Normal ��带 Ŭ���� ���� ��
        if (CS.isNormalClear == (UniteData.GameClear[1] == 1) && CS.isNormalClear)
        {
            //��� ����
            CS.unlocked = true;

            Image image = Card.GetComponent<Image>();
            image.sprite = CS.Unlock_image;
        }
    }

}
