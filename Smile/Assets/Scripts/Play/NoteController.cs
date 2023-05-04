using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    [Header("�����ϴ� ��Ʈ �̹���")] public Sprite[] noteSprite;
    [Header("����� ��� ��Ʈ UI ������Ʈ")] public GameObject[] note;
    private int noteLength; // ���̵��� ���� ��Ʈ ���� ����
    private int[] noteNums;
    private bool meetMonster = false;
    private int noteIndex = 0;  // ���� �������� ��Ʈ�� �ڸ�

    [Header("fade out�� ���� ������Ʈ")] public GameObject target;
    [Header("������ ��Ʈ ���")] public GameObject Note_Bg;

    public bool noteSuccess; // ��Ʈ ����

    // Start is called before the first frame update
    void Start()
    {
        Initialized();
        //���� ���Ӹ�� ����
        UniteData.GameMode = "Play";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialized()
    {
        noteIndex = 0;
        meetMonster = false;
        noteSuccess = false;
        DoBgShow(false); // ������ ���� ��� ��Ʈ UI ��Ȱ��ȭ
    }

    // Show Note
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Meet");
            Set_Note_Count(); // ���� ���� Ȯ��
            NoteSetting();
            DoBgShow(true); // ��� ��Ʈ UI Ȱ��ȭ

            meetMonster = true;
        }
    }

    private void NoteSetting()
    {
        noteSuccess = false;
        noteNums = new int[noteLength];

        // �������� ��Ʈ ����
        for (int i = 0; i < noteLength; i++)
        {
            noteNums[i] = Random.Range(0, 4);
            note[i].GetComponent<Image>().sprite = noteSprite[noteNums[i]];
        }

        Set_Note();
    }

    public void NoteDisabled()
    {
        // ��Ʈ ȸ������ �����
        Image image = note[noteIndex].GetComponent<Image>();
        image.color = new Color(128/ 255f, 128/ 255f, 128 / 255f, 255/ 255f);
    }

    public void NoteAbled()
    {
        // ��Ʈ ���������� �����
        for (int i = 0; i < noteLength; i++)
        {
            Image image = note[i].GetComponent<Image>();
            image.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        }
    }

    public void touchClickLeftUp()
    {
        Debug.Log("touchClickLeftUp");
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 0)
                NoteSuccess();
        }
            
    }

    public void touchClickLeftDown()
    {
        Debug.Log("touchClickLeftDown");
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 1)
                NoteSuccess();
        }
            
    }

    public void touchClickRightUp()
    {
        Debug.Log("touchClickRightUp");
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 2)
                NoteSuccess();
        }
            
    }

    public void touchClickRightDown()
    {
        Debug.Log("touchClickRightDown");
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 3)
                NoteSuccess();
        }
    }

    private void NoteSuccess()
    {
        Debug.Log("Note Success");
        NoteDisabled();
        noteIndex++;

        if (noteIndex == noteLength)
        {
            // ��� ������ ���
            Debug.Log("All Success");
            meetMonster = false;
            noteSuccess = true;
            MonsterDie();
            DoBgShow(false); // ��� ��Ʈ UI ��Ȱ��ȭ
            returnNote();
        }
    }

    private void DoBgShow(bool check)
    {
        Note_Bg.SetActive(check); // Note_Bg
    }

    // ���� �ױ�
    private void MonsterDie()
    {
        StartCoroutine("MonsterFadeOut");
    }

    // ���� ���̵� �ƿ� ó��
    IEnumerator MonsterFadeOut()
    {
        int i = 10;
        while(i > 0)
        {
            i -= 1;
            float f = i / 10.0f;
            Color c = target.GetComponent<SpriteRenderer>().color;
            c.a = f;
            target.GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(0.02f);
        }

        target.gameObject.SetActive(false);
    }

    // ��Ʈ�� ó�� ���·� �ǵ�����
    void returnNote()
    {
        noteIndex = 0;
        NoteAbled();
    }

    private void Set_Note_Count()
    {
        switch(UniteData.Closed_Monster)
        {
            case "Rose":
                noteLength = 3;
                break;
            case "Kosmos":
                noteLength = 5;
                break;
            case "MorningGlory":
                noteLength = 6;
                break;
            default:
                noteLength = 3;
                break;
        }
        Debug.Log("noteLength : " + noteLength);
    }

    private void Set_Note()
    {
        // �ڽ� ��Ʈ�� ��� ��Ȱ��ȭ
        for (int i = 0; i < 6; i++)
        {
            Note_Bg.transform.GetChild(i).gameObject.SetActive(false);
        }

        // ���Ϳ� �ش��ϴ� ����ŭ Ȱ��ȭ
        for (int i = 0; i < noteLength; i++)
        {
            Note_Bg.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
