/*
*BGM ���� ������� ���������� �����ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*-�̱������� �����Ͽ� ��𼭵� ���� �����ϰ� �����
*-BGM�� ��� ���� �ٲ� ������ �ʰ� ��� ����ǰ� �����
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio_Manager : MonoBehaviour
{
    private int MAIN_SONG = 0;
    private int PLAY_EASY = 1;
    private int PLAY_NORMAL = 2;
    private int GAMEOVER = 3;
    private int GAMECLEAR = 4;
    private int PLAY_HARD = 5;
    private int PLAY_EASY_WORLD2 = 6;

    public static Audio_Manager instance = null;

    public AudioClip[] audioList;
    private AudioSource ass;
    public static bool play_Audio = true;

    public bool doLoop = true;

    private int sceneGroup = -1;


    public static Audio_Manager Instance
    {
        get { return instance; }
    }

    void ���ٲ����ѹ�������()
    {
        if(sceneGroup != getSceneGroup(SceneManager.GetActiveScene().name))
        {
            sceneGroup = getSceneGroup(SceneManager.GetActiveScene().name);
            //Debug.Log("NAME: " + SceneManager.GetActiveScene().name+" GR: "+ getSceneGroup(SceneManager.GetActiveScene().name));
            BGM_Change(sceneGroup);

            if(sceneGroup==GAMECLEAR || sceneGroup==GAMEOVER){ doLoop = false; }
            else { doLoop = true; }
        }
        return;
    }

    void Awake()
    {
        ass = gameObject.GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        ���ٲ����ѹ�������();

        ass.volume = UniteData.BGM;

        if(play_Audio){ ass.pitch = 1f; }
        else{ ass.pitch = 0f; }

        if (doLoop) { ass.loop = true;}
        else {  ass.loop = false; }
    }

    private void BGM_Change(int index)
    {
        ass.Stop();
        ass.clip = audioList[index];
        ass.Play();
    }

    private int getSceneGroup(string scenename)
    {
        //play / cut �������� BGM�� ����
        if (scenename == "Play" || 
            scenename == "InGame-RN")
        {     
            switch(UniteData.Difficulty)
            {
                case 1:
                    return PLAY_EASY;
                case 2:
                    return PLAY_NORMAL;
                case 3:
                    return PLAY_HARD;
                case 4:
                    return PLAY_EASY_WORLD2;
                default:
                    return PLAY_EASY;
            }
        }
        //����ȭ�� / �� ���� / �������� ���� �� BGM�� ����
        else if (scenename == "Main Manu" ||
                scenename == "Character Menu" ||
                scenename == "MapManu/Map Menu" ||
                scenename == "MapManu/Easy Map" ||
                scenename == "MapManu/Nomal Map")
        {
            return MAIN_SONG;
        }
        //���ӿ��� �������� BGM�� ����
        else if (scenename == "Gameover")
        {
            return GAMEOVER;
        }
        //����Ŭ���� �������� BGM�� ����
        else if (scenename == "Gameclear")
        {
            return GAMECLEAR;
        }
        else
        {
            return MAIN_SONG;
        }
    }
}
