using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    public Sprite[] noteSprite;
    public GameObject[] note;
    public int[] noteNums;
    public bool meetMonster = false;
    private int noteIndex = 0;  // ���� �������� ��Ʈ�� �ڸ�

    public GameObject MonsterParent;

    // Start is called before the first frame update
    void Start()
    {
        noteIndex = 0;
        meetMonster = false;
        DoBgShow(false); // ������ ���� ��� ��Ʈ UI ��Ȱ��ȭ
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Show Note
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Meet");
            NoteSetting();
            DoBgShow(true); // ��� ��Ʈ UI Ȱ��ȭ
            meetMonster = true;
        }
    }

    private void NoteSetting()
    {
        noteNums = new int[note.Length];

        // �������� ��Ʈ ����
        for (int i = 0; i < note.Length; i++)
        {
            noteNums[i] = Random.Range(0, 4);
            note[i].GetComponent<Image>().sprite = noteSprite[noteNums[i]];
        }
    }

    public void NoteDisabled()
    {// ��Ʈ ���������� �����
        //note[noteIndex].SetActive(false);
        Image image = note[noteIndex].GetComponent<Image>();
        image.color = new Color(128/ 255f, 128/ 255f, 128 / 255f, 255/ 255f);
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

        if (noteIndex == note.Length)
        {
            // ��� ������ ���
            Debug.Log("All Success");
            meetMonster = false;
            MonsterParent.SetActive(false);
            DoBgShow(false); // ��� ��Ʈ UI ��Ȱ��ȭ
        }
    }

    private void DoBgShow(bool check)
    {
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(check); // Note_Bg
    }
}
