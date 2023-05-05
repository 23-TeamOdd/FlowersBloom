/*
*������� ���������� �����ϴ� ��ũ��Ʈ
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

    public static Audio_Manager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
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
        if (SceneManager.GetActiveScene().name != "Play" && SceneManager.GetActiveScene().name != "InGame-RN")
        {
            Destroy(gameObject);
        }

        AudioSource ass = gameObject.GetComponent<AudioSource>();
        ass.volume = UniteData.BGM;
    }
}
