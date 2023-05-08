using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicsource;

    public void Start()
    {
        DontDestroyOnLoad(musicsource);
    }

    public void SetMusicVolume(float volume)
    {
        musicsource.volume = volume;
    }

    /* 
    public GameObject BackgroundMusic;
    public void Awake()
     {
         BackgroundMusic = GameObject.Find("Sweet Dreams My Dear Instrumental");
         musicsource = BackgroundMusic.GetComponent<AudioSource>(); //������� �����ص�
         if (musicsource.isPlaying) return; //��������� ����ǰ� �ִٸ� �н�
         else
         {
             musicsource.Play();
             DontDestroyOnLoad(BackgroundMusic); //������� ��� ����ϰ�(���� ��ư�Ŵ������� ����)

         }
     } */
}