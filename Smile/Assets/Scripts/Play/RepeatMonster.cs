using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMonster : MonoBehaviour
{
    public GameObject[] monsters;
    private GameObject monster;
    static public int monsterCount; // ���� ���� Ƚ�� (���̵��� ���� ����)

    public GameClear s_gameclear;

    // Start is called before the first frame update
    void Start()
    {
        if(UniteData.ReStart)
            Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialized()
    {
        Debug.Log("���� ������ �ʱ�ȭ");
        switch (UniteData.Difficulty)
        {
            case 1 :
                monsterCount = 6;
                monster = monsters[0];
                break;

            case 2 :
                monsterCount = 7;
                monster = monsters[1];
                break;

            case 3 :
                monsterCount = 10;
                //monster = monsters[2];
                break;

            default : 
                monsterCount = 6;
                monster = monsters[0];
                break;
        }
        UniteData.ReStart = false;
    }

    public void MonsterColorOrigin()
    {
        Debug.Log("monsterCount" + monsterCount);
        Debug.Log("MonsterColorOrigin");
        monster.SetActive(true);

        // �ٿ����� a�� �ٽ� ���� ����
        Color c = monster.GetComponent<SpriteRenderer>().color;
        c.a = 225f;
        monster.GetComponent<SpriteRenderer>().color = c;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteManager"))
        {
            if (monsterCount > 1)
            {
                monsterCount--;
                MonsterColorOrigin();
            }
            else if (monsterCount == 1)
            {
                Debug.Log("Game Clear");
                s_gameclear.ClearGame();
            }
        }
    }

}
