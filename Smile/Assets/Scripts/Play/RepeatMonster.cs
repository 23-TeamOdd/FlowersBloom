using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMonster : MonoBehaviour
{
    public GameObject monster;
    public int monsterCount; // ���� ���� Ƚ�� (���̵��� ���� ����)

    public GameClear s_gameclear;

    // Start is called before the first frame update
    void Start()
    {
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialized()
    {
        switch (UniteData.Difficulty)
        {
            case 1 :
                monsterCount = 6;
                break;

            case 2 :
                monsterCount = 7;
                break;

            case 3 :
                monsterCount = 10;
                break;

            default : 
                monsterCount = 6;
                break;
        }
        
    }

    public void MonsterColorOrigin()
    {
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
