using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto_RestartGame : MonoBehaviour
{
    private Tuto_GameManager t_gm; // GameClear
    private Tuto_NoteController t_nc;
    private Tuto_PlayerController t_pc;
    private RepeatBG s_sb;
    private Tuto_MonsterManager t_mm; // RepeatMonster
    private BtnStop s_bs;


    // Start is called before the first frame update
    void Start()
    {
        t_gm = FindAnyObjectByType<Tuto_GameManager>();
        t_nc = FindAnyObjectByType<Tuto_NoteController>();
        t_pc = FindObjectOfType<Tuto_PlayerController>();
        s_sb = FindObjectOfType<RepeatBG>();
        t_mm = FindAnyObjectByType<Tuto_MonsterManager>();
        s_bs = FindObjectOfType<BtnStop>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public void GameReSettings()
    {
        if (UniteData.ReStart)
        {
            Debug.Log("GameSetting");
            UniteData.Difficulty = 0; // ���Ŀ� �ʿ��� �����ϸ� �̰� ������ ����

            t_gm.Initialized();
            t_nc.Initialized();
            t_pc.Initialized();
            s_bs.Initialized();
            t_mm.Initialized();

            UniteData.ReStart = false;
        }

        s_sb.Initialized(); // �ƾ����� ���ƿ��� ��쿡�� ȣ��
    }
}
