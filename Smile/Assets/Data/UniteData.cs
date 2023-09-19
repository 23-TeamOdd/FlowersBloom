#define RELEASE_D
using System.Collections.Generic;
using UnityEngine;

public class UniteData
{

    //���� ���� ������
    public static string GameMode = "None"; //���� ���� ��� ["None", Menu, Play, Scripting]
    public static int Difficulty = 2; //���� ���̵� easy = 1 / normal = 2 / hard = 3 / world 2 easy = 4 ...
    public static int notePoint = 2; // ��ȸ ����Ʈ
    public static int lifePoint = 3; // ��� ����Ʈ

    public static int Node_LifePoint = 2; //��� ���
    public static int Node_Click_Counter = 0; //��� Ŭ�� Ƚ��

    public static bool Move_Progress = true; //Play������ ������/���� ����
    public static Vector2 Player_Location_Past = new Vector2(-4955f, 0); //�÷��̾ ������ �ִ� ��ġ
    public static float Play_Scene_Time = 0f; //�÷��� �������� �帥 �ð�

    public static string Selected_Character = "ForgetMeNot"; //������ ������ ĳ���� ["Dandelion", "Tulip", "ForgetMeNot"]
    public static string Closed_Monster = "Rose"; //�ֱ��� ���� ["Rose", "Cosmos", "MorningGlory", "Poppy", "Pasque"]

    public static bool ReStart = true; // ������ ����� Ȥ�� ������ ��ư�� ���� ���

    public static bool NoteSuccess = false; // ���� ��� ��Ʈ ���� ����
    public static bool oneNoteSuccess = false;
    public static bool NoteSet = false;
    public static int lastNoteIndex = -1; // ���� �ε��� ����(�Ⱦ��������;-;)
    public static int[] noteNums;

    public static bool finishGame = false; //���� ���� ����

    public static List<Dictionary<string, object>> data;
    public static int mon_num = 1; // ���� ���� ������

    public static int noteIndex = 0;  // ���� �������� ��Ʈ�� �ڸ�

    //���� ���� ������
    public static int[] GameClear = {
        PlayerPrefs.GetInt("GameClear-Easy", 0), //EASY
        PlayerPrefs.GetInt("GameClear-Normal", 0), //NORMAL
        PlayerPrefs.GetInt("GameClear-Hard", 0), //HARD
        PlayerPrefs.GetInt("GameClear-W2-Easy", 0), //W2E
        PlayerPrefs.GetInt("GameClear-W2-Normal", 0), //W2N
        PlayerPrefs.GetInt("GameClear-W2-Hard", 0) //W2H
}; // ���̵��� Ŭ���� ���� 0: false, 1: true


    //���丮 ���� ���� ������
#if RELEASE_D
    public static int[] StoryClear = {
        PlayerPrefs.GetInt("story-Easy", 0), //EASY
        PlayerPrefs.GetInt("story-Normal", 0), //NORMAL
        PlayerPrefs.GetInt("story-Hard", 0), //HARD
        PlayerPrefs.GetInt("story-W2-Easy", 0), //W2E
        PlayerPrefs.GetInt("story-W2-Normal", 0), //W2N
        PlayerPrefs.GetInt("story-W2-Hard", 0) //W2H
}; // ��ũ��Ʈ ���� ���� 0: �� / 1: Prestart�� / 2: Finish����
#else

    public static int[] StoryClear = new int[] { 0, 0, 0, 0, 0, 0 };
#endif

//���� ���� ������
public static float BGM = PlayerPrefs.GetFloat("BGM", 0.7f); //������� ����
    public static float Effect = PlayerPrefs.GetFloat("Effect", 0.7f); //ȿ���� ����

    //���� ���� ������
    //private static byte[] KEY= System.Text.Encoding.UTF8.GetBytes("68656c6c6f2045666e6920426573746f6d706174657221"); //��ȣȭ Ű


    //���� ���� ���� ������ 
    //key-md5 / value-ARIA
    public static void SaveUserData()
    {
        PlayerPrefs.SetFloat("BGM", BGM);
        PlayerPrefs.SetFloat("Effect", Effect);
        PlayerPrefs.SetInt("GameClear-Easy", GameClear[0]);
        PlayerPrefs.SetInt("GameClear-Normal", GameClear[1]);
        PlayerPrefs.SetInt("GameClear-Hard", GameClear[2]);
        PlayerPrefs.SetInt("GameClear-W2-Easy", GameClear[3]);
        PlayerPrefs.SetInt("GameClear-W2-Normal", GameClear[4]);
        PlayerPrefs.SetInt("GameClear-W2-Hard", GameClear[5]);

        PlayerPrefs.SetInt("story-Easy", StoryClear[0]);
        PlayerPrefs.SetInt("story-Normal", StoryClear[1]);
        PlayerPrefs.SetInt("story-Hard", StoryClear[2]);
        PlayerPrefs.SetInt("story-W2-Easy", StoryClear[3]);
        PlayerPrefs.SetInt("story-W2-Normal", StoryClear[4]);
        PlayerPrefs.SetInt("story-W2-Hard", StoryClear[5]);

        PlayerPrefs.SetString("Selected_Character", Selected_Character);
        PlayerPrefs.Save();
    }
    public static void ResetUserData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("BGM", BGM);
        PlayerPrefs.SetFloat("Effect", Effect);

        Selected_Character = "Dandelion";
        GameClear = new int[] { 0, 0, 0, 0, 0, 0 };
        StoryClear = new int[] { 0, 0, 0, 0, 0, 0 };

        SaveUserData();
        Debug.Log("���� ������ �ʱ�ȭ �Ϸ�");
    }
}
