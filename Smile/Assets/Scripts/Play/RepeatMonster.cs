using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMonster : MonoBehaviour
{
    public GameObject[] monsters;
    static private GameObject monster;

    public Sprite[] monster_images;
    static private SpriteRenderer monster_image;

    private string[] monster_name = { "Rose", "Cosmos", "MorningGlory" };

    static public int monsterCount; // ���� ���� Ƚ�� (���̵��� ���� ����)

    public GameClear s_gameclear;

    public NoteController[] noteControllers;
    private NoteController noteController;

    // ���� ���� ��ġ
    private static int ran_mon = 0;//HACK

    // ���߿� ���������� �����ؾ���
    private List<Dictionary<string, object>> data;

    // Start is called before the first frame update
    void Start()
    {

        data = CSVReader.Read("World1_easy");

        switch (UniteData.Difficulty)
        {
            case 1:
                monster = monsters[0];
                noteController = noteControllers[0];
                break;

            case 2:
                monster = monsters[1];
                noteController = noteControllers[1];
                break;

            case 3:
                //monster = monsters[2];
                //noteController = noteControllers[2];
                break;

            default:
                monster = monsters[0];
                noteController = noteControllers[0];
                break;
        }

        monster_image = monster.GetComponent<SpriteRenderer>();

        if (UniteData.ReStart)
            Initialized();

        monster_image.sprite = monster_images[ran_mon];
    }

    // Update is called once per frame
    void Update()
    {
        //HACK
        //monster_image.sprite = monster_images[ran_mon]; 
        //Debug.Log("monster_image" + monster_image.sprite);
    }

    public void Initialized()
    {
        Debug.Log("���� ������ �ʱ�ȭ");
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

        // CSV�� �о ������ ������ ����
        monsterCount = int.Parse(data[0]["num"].ToString());
        Debug.Log("monsterCountNew : "+  monsterCount);
        MonsterColorOrigin();
        UniteData.ReStart = false;


    }

    public void MonsterColorOrigin()
    {
        Debug.Log("monsterCount" + monsterCount);
        Debug.Log("MonsterColorOrigin");
        monster.SetActive(true);

        // ��Ʈ �ʱ�ȭ
        noteController.returnNote();

        /*// ���� ���� ��ġ
        int ran_mon = 0;//HACK*/

        if(UniteData.Difficulty == 2)
        {
            // �븻 ��忡���� �ڽ��� ������
            ran_mon = Random.Range(0, 2);
        }

        Debug.Log("ran_mon : " + ran_mon);
        if(monsterCount == 2 && UniteData.Difficulty == 2)
        {
            ran_mon = 1;
        }

#if true
        UniteData.Closed_Monster = monster_name[ran_mon];
        monster_image.sprite = monster_images[ran_mon];
        Debug.Log("monster_image" + monster_image.sprite);

        // �ٿ����� a�� �ٽ� ���� ����
        Color c = monster.GetComponent<SpriteRenderer>().color;
        c.a = 225f;
        monster.GetComponent<SpriteRenderer>().color = c;
#endif
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
