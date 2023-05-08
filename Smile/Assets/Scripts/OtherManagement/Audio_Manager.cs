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
    public static Audio_Manager instance = null;

    public AudioClip[] audioList;
    private AudioSource ass;
    public static bool play_Audio = true;

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

        if(play_Audio)
        {
            ass.pitch = 1f;
        }
        else
        {
            ass.pitch = 0f;
        }
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
        if (scenename == "Play" || scenename == "InGame-RN")
        {
            return 1;
        }
        //����ȭ�� / �� ���� / �������� ���� �� BGM�� ����
        else if (scenename == "Main Manu" ||
                scenename == "Character Menu" ||
                scenename == "MapManu/Map Menu" ||
                scenename == "MapManu/Easy Map" ||
                scenename == "MapManu/Nomal Map")
        {
            return 0;
        }
        else
        {
            return 0;
        }
    }
}
