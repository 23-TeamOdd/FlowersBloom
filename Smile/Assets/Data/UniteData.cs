using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniteData
{
    //���� ���� ������
    public static string GameMode = "None"; //���� ���� ��� ["None", Menu, Play, CutScene, setting]
    public static int Difficulty = 2; //���� ���̵�
    public static int notePoint = 2; // ��ȸ ����Ʈ
    public static int lifePoint = 3; // ��� ����Ʈ

    public static int Node_LifePoint = 2; //��� ���
    public static int Node_Click_Counter = 0; //��� Ŭ�� Ƚ��

    public static bool Move_Progress = true; //Play������ ������/���� ����
    public static Vector2 Player_Location_Past= Vector2.zero;

    public static string Selected_Character = "Dandelion"; //������ ������ ĳ����
    public static string Closed_Monster = "Rose"; //�ֱ��� ����

    //���� ���� ������
    public static bool[] GameClear = { false, false, false}; // ���̵��� Ŭ���� ����

    //���� ���� ������
    public static float BGM = 1f; //������� ����
    public static float Effect = 1f; //ȿ���� ����

    //���� ���� ���� ������

}
