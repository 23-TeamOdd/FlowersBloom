using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlayerImageChange : MonoBehaviour
{
    public GameObject[] playerPrefab;
    static public GameObject player;

    [Header("������ ĳ���� �̹��� �� ������Ʈ")] public GameObject changePlayer;
    [Header("������ ĳ���� �̹���_�ε鷹")] public Sprite[] changePlayerImg_D;
    [Header("������ ĳ���� �̹���_ƫ��")] public Sprite[] changePlayerImg_T;
    [Header("������ ĳ���� �̹���_������")] public Sprite[] changePlayerImg_F;

    private SpriteRenderer[] spriteRenderers;
    private SpriteRenderer sr_changePlayer;

    private int[] noteNums;
    private Sprite[] changePlayerImg;

    private void Awake()
    {
        if (UniteData.Difficulty != 0)
        {
            //ĳ���� ����
            if (UniteData.Selected_Character == "Dandelion")
            {
                player = playerPrefab[0];
                changePlayerImg = changePlayerImg_D;
            }
            else if (UniteData.Selected_Character == "Tulip")
            {
                player = playerPrefab[1];
                changePlayerImg = changePlayerImg_T;
            }
            else if (UniteData.Selected_Character == "ForgetMeNot")
            {
                player = playerPrefab[2];
                changePlayerImg = changePlayerImg_F;
            }
            else
            {
                Debug.LogError("GameClear.cs ���Ͽ��� ĳ���� ���� ������ �߻��߽��ϴ� \n �Ƹ��� UniteData.Selected_Character ���� �����̰ų� �ش� ��ũ��Ʈ�� ���ǹ����� ������ �����ϼ���.");
            }
        }
        else
        {
            player = playerPrefab[0];
            changePlayerImg = changePlayerImg_D;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderers = player.GetComponentsInChildren<SpriteRenderer>()
            .Where(spriteRenderer => spriteRenderer.CompareTag("PlayerSprite"))
            .ToArray();
        sr_changePlayer = changePlayer.GetComponent<SpriteRenderer>();

        Color color_changePlayer = sr_changePlayer.color;
        color_changePlayer.a = 0f;
        sr_changePlayer.color = color_changePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        // ��� ��Ʈ ���� ������
        if(UniteData.NoteSet)
        {
            setNoteNum();
        }

        // ��Ʈ ����������
        if (UniteData.oneNoteSuccess)
        {
            StartCoroutine(CharacterImageChange());
        }
    }

    private void setNoteNum()
    {
        UniteData.NoteSet = false;
        noteNums = UniteData.noteNums;

        //for(int i = 0; i<noteNums.Length; i++)
        //{
        //    Debug.Log("ImageChange NoteNums[" + i + "] = " + noteNums[i]);
        //}
    }

    IEnumerator CharacterImageChange()
    {
        UniteData.oneNoteSuccess = false;

        Color color_changePlayer = sr_changePlayer.color;
        color_changePlayer.a = 255f;
        sr_changePlayer.color = color_changePlayer;
        if (noteNums[UniteData.lastNoteIndex] == 0)
        {
            sr_changePlayer.sprite = changePlayerImg[noteNums[UniteData.lastNoteIndex - 1] - 1];
            Debug.Log("noteIndex : " + (UniteData.lastNoteIndex - 1) + ", noteNums : " + (noteNums[UniteData.lastNoteIndex - 1] - 1));
        }
        else
        {
            sr_changePlayer.sprite = changePlayerImg[noteNums[UniteData.lastNoteIndex] - 1];
            Debug.Log("noteIndex : " + (UniteData.lastNoteIndex) + ", noteNums : " + (noteNums[UniteData.lastNoteIndex] - 1));
        }
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = 0f;
            spriteRenderer.color = color;
        }

        yield return new WaitForSeconds(0.4f); // ������� �ð�

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = 255f;
            spriteRenderer.color = color;
        }

        color_changePlayer.a = 0f;
        sr_changePlayer.color = color_changePlayer;
    }
}
