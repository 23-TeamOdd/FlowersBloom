/*
*CutScene ���� �� �麸��(��׶���)�� �̹����� �����ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*-�� ���� �� ��Ȳ�� ���� �̹��� ������ ���������� ����
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBoard_In_CutScene : MonoBehaviour
{
    public Sprite[] BackBoard;
    public SpriteRenderer BackBoardRenderer_Top;
    public SpriteRenderer BackBoardRenderer_Bottom;

    private enum Banner
    {
        Dandelion,
        Tulip,
        Rose,
        Cosmos,
        MorningGlory,
        Poppy
    }

    private void Start()
    {
        Set_Image();
    }

    private void Update()
    {
        Set_Image();
    }

    private int Find_Index(string name)
    {
        Banner banner;
        if (!System.Enum.TryParse(name, out banner))
        {
            Debug.LogWarning("�ش� �̸��� ���� �̹��� ������ ������� �ʾҽ��ϴ�.\nBackBoard ������Ʈ���� Ȯ���ϼ���!");
            return -1;  // ���ڷ� ���޵� ���ڿ��� enum�� ���ǵǾ� ���� ���� ��� -1 ��ȯ
        }

        switch (banner)
        {
            case Banner.Dandelion:
                return 0;
            case Banner.Tulip:
                return 1;
            case Banner.Rose:
                return 2;
            case Banner.Cosmos:
                return 3;
            case Banner.MorningGlory:
                return 4;
            case Banner.Poppy:
                return 5;
            default:
                Debug.LogWarning("�ش� �̸��� ���� �̹��� ������ ������� �ʾҽ��ϴ�.\nBackBoard ������Ʈ���� Ȯ���ϼ���!");
                return -1;
        }
    }

    private void Set_Image()
    {
        BackBoardRenderer_Top.sprite = BackBoard[Find_Index(UniteData.Closed_Monster)];
        BackBoardRenderer_Bottom.sprite = BackBoard[Find_Index(UniteData.Selected_Character)];
    }
}
