using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ���� ����� �� �ʱ� ���·� �ǵ����� ��ũ��Ʈ
public class ReStartGame : MonoBehaviour
{
    private GameClear s_gc;
    private NoteController s_nc;
    private PlayerController s_pc;
    private RepeatBG s_sb;
    private RepeatMonster s_rm;
    private BtnStop s_bs;

    // Start is called before the first frame update
    void Start()
    {
        s_gc= FindObjectOfType<GameClear>();
        s_nc = FindObjectOfType<NoteController>();
        s_pc = FindObjectOfType<PlayerController>();
        s_sb = FindObjectOfType<RepeatBG>();
        s_rm = FindObjectOfType<RepeatMonster>();
        s_bs = FindObjectOfType<BtnStop>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public void GameReSettings()
    {
        if (UniteData.ReStart)
        {
            Debug.Log("GameSetting");
            s_gc.Initialized();
            s_nc.Initialized();
            s_pc.Initialized();
            s_rm.Initialized();
            s_bs.Initialized();

            UniteData.ReStart = false;
        }

        s_sb.Initialized(); // �ƾ����� ���ƿ��� ��쿡�� ȣ��
    }
}
