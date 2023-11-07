using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  ���̵�� UI + Ʃ�丮��� ��Ʈ ��Ʈ�ѷ�
// �⺻ NoteController���� Ʃ�丮�� �����ұ� ������ �Ⱦ��̴� �Լ��� �ʹ� ���Ƽ� ���� �ۼ���
public class Tuto_NoteController : MonoBehaviour
{
    [Header("�����ϴ� ��Ʈ �̹���")] public Sprite[] noteSprite;
    [Header("����� ��� ��Ʈ UI ������Ʈ")] public GameObject[] note;
    private int noteLength; // ��Ʈ ���� ����
    private int[] noteNums;
    private bool meetMonster = false;

    [Header("fade out�� ���� ������Ʈ")] public GameObject target;
    [Header("������ ��� ��Ʈ UI")] public GameObject Note_Bg;

    [Header("���̵� �ؽ�Ʈ �ڽ� UI")] public GameObject guideTextBox;
    [Header("���̵� �ؽ�Ʈ UI")] public Text guideText;
    [Header("���̵� �հ��� UI")] public GameObject guideFinger;

    // ��Ʈ ��ư ��ġ
    [SerializeField] private Transform[] noteBtn;
    Transform transFinger;

    // Start is called before the first frame update
    void Start()
    {
        Initialized();
    }

    public void Initialized()
    {
        UniteData.noteIndex = 0;
        meetMonster = false;
        UniteData.NoteSuccess = false;
        UniteData.oneNoteSuccess = false;
        DoBgShow(false); // ������ ���� ��� ��Ʈ UI ��Ȱ��ȭ

        // ���̵� �հ���, �ؽ�Ʈ�ڽ� ��Ȱ��ȭ
        guideTextBox.SetActive(false);
        guideFinger.SetActive(false);

        transFinger = guideFinger.GetComponent<Transform>();
    }

    public void DoBgShow(bool check)
    {
        Note_Bg.SetActive(check); // Note_Bg
    }

    private void Set_Note()
    {
        // �ڽ� ��Ʈ�� ��� ��Ȱ��ȭ
        for (int i = 0; i < Note_Bg.transform.childCount; i++)
        {
            Note_Bg.transform.GetChild(i).gameObject.SetActive(false);
        }

        // ���Ϳ� �ش��ϴ� ����ŭ Ȱ��ȭ
        for (int i = 0; i < noteLength; i++)
        {
            Note_Bg.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    // Show Note
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !UniteData.ReStart)
        {
            //#if true
            Debug.Log("Player Meet");
            Set_Note_Count(); // ��Ʈ ���� Ȯ��
            Set_Note();
            NoteSetting();
            DoBgShow(true); // ��� ��Ʈ UI Ȱ��ȭ

            meetMonster = true;

            if (UniteData.mon_num == 1)
                GuideTextSet();
            //#endif
        }
    }

    public void GuideTextSet()
    {
        // �ؽ�Ʈ �ڽ� ǥ��
        guideTextBox.SetActive(true);
        guideText.text = "��Ʈ�ڽ��� ǥ�õǴ� ���� �������\r\n��, ���� ��Ʈ�� ��ġ�ϼ���.";

        // ���̵� �հ��� ǥ��
        GuideFingerSet();

        // �̵� ���߱�
        UniteData.Move_Progress = false;
    }


    private void GuideTextOff()
    {
        guideTextBox.SetActive(false);
    }

    private void GuideFingerSet()
    {
        guideFinger.SetActive(true);
        transFinger.position = noteBtn[noteNums[UniteData.noteIndex]-1].position;
    }

    private void GuideFingerOff()
    {
        guideFinger.SetActive(false);
    }


    private void NoteSetting()
    {
        UniteData.NoteSuccess = false;
        noteNums = new int[noteLength];

        UniteData.oneNoteSuccess = false;

        // ��Ʈ ����
        for (int i = 0; i < noteLength; i++)
        {
            string columnName = $"noteNums{i}";
            noteNums[i] = int.Parse(UniteData.data[UniteData.mon_num][columnName].ToString());
            Debug.Log("noteNums[" + i + "] : " + noteNums[i]);
            note[i].GetComponent<Image>().sprite = noteSprite[noteNums[i]-1];
        }
        UniteData.noteNums = noteNums;
        UniteData.NoteSet = true;
    }

    // ��Ʈ�� ó�� ���·� �ǵ�����
    public void returnNote()
    {
        UniteData.noteIndex = 0;
        NoteAbled();
    }

    private void NoteSuccess()
    {
        Debug.Log("Note Success");
        NoteDisabled();
        UniteData.oneNoteSuccess = true;
        UniteData.lastNoteIndex = UniteData.noteIndex;
        UniteData.noteIndex++;

        Debug.Log("noteindex : " + UniteData.noteIndex);
        if (UniteData.noteIndex == noteLength)
        {
            // ��� ������ ���
            Debug.Log("All Success");
            meetMonster = false;
            UniteData.NoteSuccess = true;
            UniteData.Move_Progress = true;
            MonsterDie();
            DoBgShow(false); // ��� ��Ʈ UI ��Ȱ��ȭ
            returnNote();

            target.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

            // Ʃ�丮�� ���̵�, �հ��� off
            GuideTextOff();
            GuideFingerOff();

            UniteData.tuto_meetMonster = false;
        }

        else
        {
            // ���� ������
            StartCoroutine(MonsterBlink());

            if (UniteData.mon_num <= 1 || UniteData.tuto_meetMonster)
            {
                // �հ��� ���̵� �����̱�
                GuideFingerSet();
            }
        }
    }

    public void touchClick(int i)
    {
        if (Input.touchCount > 1) return; // ��Ƽ ��ġ �ȵǰ�
        if (meetMonster)
        {
            if (noteNums[UniteData.noteIndex] == i + 1)
                NoteSuccess();
        }
        Debug.Log("touchClick : " + i);
    }
    public void NoteDisabled()
    {
        // ��Ʈ ȸ������ �����
        Image image = note[UniteData.noteIndex].GetComponent<Image>();
        image.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255 / 255f);
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

    // ���� �ױ�
    private void MonsterDie()
    {
        StartCoroutine("MonsterFadeOut");
    }

    // ���� ���̵� �ƿ� ó��
    IEnumerator MonsterFadeOut()
    {
        int i = 10;
        while (i > 0)
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

    private void Set_Note_Count()
    {
        noteLength = int.Parse(UniteData.data[UniteData.mon_num]["noteLength"].ToString());
        Debug.Log("noteLength : " + noteLength);
    }
    IEnumerator MonsterBlink()
    {
        int i = 0;
        while (i < 4) // �� ���� �����̴� Ƚ�� * 2
        {
            if (i % 2 == 0)
                target.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 120);
            else target.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

            yield return new WaitForSeconds(0.1f); // �����̴� �ֱ�
            i++;
        }
        target.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        yield return null;

    }
}
