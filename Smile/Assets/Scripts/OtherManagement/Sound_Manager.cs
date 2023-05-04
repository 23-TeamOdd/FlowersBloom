/*
*��� ���� �� ���带 �����ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*-������ ���� ������������ ������
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    public AudioSource sound;

    public enum SoundType
    {
        BGM,
        Effect
    }
    public SoundType Sound_type;

    private void Start()
    {
        GetSoundValue();
    }

    private void Update()
    {
        GetSoundValue();
    }

    private void GetSoundValue()
    {
        switch(Sound_type)
        {
            case SoundType.BGM:
                sound.volume = UniteData.BGM;
                break;
            case SoundType.Effect:
                sound.volume = UniteData.Effect;
                break;
            default:
                //Log�� ���� ����Ѵ�
                Debug.LogWarning("Sound_Manager class���� �����ϴ� SoundType�� �������� �ʾҽ��ϴ�. \n Audio�� �����ϴ� ������Ʈ�� Inspector���� ������ ���ֽñ� �ٶ��ϴ�.");
                break;
        }
    }
}
