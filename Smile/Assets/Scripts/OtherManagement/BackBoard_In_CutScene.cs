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

    public void Start()
    {
        BackBoardRenderer_Top.sprite = BackBoard[Find_Index(UniteData.Closed_Monster)];
        BackBoardRenderer_Bottom.sprite = BackBoard[Find_Index(UniteData.Selected_Character)];
    }

    private int Find_Index(string name)// �ʹ� ���� ���� �ۼ��ߴ�... [HACK]
    {
        if(name== "Dandelion")
        {
            return 0;
        }
        else if(name== "Tulip")
        {
            return 1;
        }

        else if(name=="Rose")
        {
            return 2;
        }

        else if(name=="Cosmos")
        {
            return 3;
        }

        return 0;
    }
}
