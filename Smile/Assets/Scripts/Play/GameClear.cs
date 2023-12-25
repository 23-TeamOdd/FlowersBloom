using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameClear : MonoBehaviour
{
    //public GameObject bgGroup;
    static public GameObject player;

    public GameObject storyScriptGroup;

    public GameObject[] canPlay_prefab;
    public GameObject[] backgrounds;
    private static GameObject[] backgrounds_stt;

    private float playerStartPositionX;

    private float xScreenSize;
    private float playerMaxMovePosX; // �÷��̾� �ִ� �̵� �Ÿ�

    private bool b_playerMove; // �÷��̾� �̵� �Ǵ� - ���� ����۽� �ʱ�ȭ

    [SerializeField] float moveSpeed; // Floor2�� RepeatBG Speed�� �Ȱ��� �ӵ��� ����

    public Sprite[] lifepoint_images; // ������ ��� ����Ʈ �̹���
    public GameObject[] lifepoints; // �������Ʈ ������Ʈ

    private void Awake()
    {
        UniteData.finishGame = false;
        backgrounds_stt = backgrounds;
        //ĳ���� ����
        if (UniteData.Selected_Character== "Dandelion")
        {
            //�ش� ĳ���͸� �÷��̾�� ���� �� Ȱ��ȭ
            Debug.Log("Player : Dandelion");
            canPlay_prefab[0].SetActive(true);

            canPlay_prefab[1].SetActive(false);
            canPlay_prefab[2].SetActive(false);

            player = canPlay_prefab[0];
            for (int i = 0; i < lifepoints.Length; i++)
            {
                Image image = lifepoints[i].GetComponent<Image>();
                image.sprite = lifepoint_images[0];
            }
        }
        else if (UniteData.Selected_Character == "Tulip")
        {
            //�ش� ĳ���͸� �÷��̾�� ���� �� Ȱ��ȭ
            Debug.Log("Player : Tulip");


            canPlay_prefab[1].SetActive(true);
            canPlay_prefab[0].SetActive(false);
            canPlay_prefab[2].SetActive(false);

            player = canPlay_prefab[1];
            for (int i = 0; i < lifepoints.Length; i++)
            {
                Image img = lifepoints[i].GetComponent<Image>();
                img.sprite = lifepoint_images[1];
            }
        }
        else if (UniteData.Selected_Character == "ForgetMeNot")
        {
            //�ش� ĳ���͸� �÷��̾�� ���� �� Ȱ��ȭ
            Debug.Log("Player : ForgetMeNot");

            canPlay_prefab[2].SetActive(true);

            canPlay_prefab[0].SetActive(false);
            canPlay_prefab[1].SetActive(false);

            player = canPlay_prefab[2];
            for (int i = 0; i < lifepoints.Length; i++)
            {
                Image img = lifepoints[i].GetComponent<Image>();
                img.sprite = lifepoint_images[2];
            }
        }
        else
        {
            Debug.LogError("GameClear.cs ���Ͽ��� ĳ���� ���� ������ �߻��߽��ϴ� \n �Ƹ��� UniteData.Selected_Character ���� �����̰ų� �ش� ��ũ��Ʈ�� ���ǹ����� ������ �����ϼ���.");
        }


        backgroundSetting(UniteData.Difficulty);
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialized();

        xScreenSize = Camera.main.orthographicSize * Camera.main.aspect * 2;
        playerStartPositionX = player.transform.position.x;
        playerMaxMovePosX = playerStartPositionX + xScreenSize;


        //���丮 ��ũ��Ʈ ����
        storyScriptGroup.SetActive(true);
    }

    public void Initialized()
    {
        b_playerMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(b_playerMove)
        {
            if(player.transform.position.x < playerMaxMovePosX)
            {
                PlayerMove();
            }

            if (player.transform.position.x > playerMaxMovePosX - 1500)
            {
                Animator fadeAnimator = GameObject.Find("FadeOut_Clear").GetComponent<Animator>();

                fadeAnimator.SetBool("IsStartFade", true);
            }
        }
    }

    public void ClearGame()
    {
        UniteData.finishGame = true;
        bgStop();

        //���丮 ��ũ��Ʈ ����
        storyScriptGroup.SetActive(true);

        UniteData.ReStart = true;
        UniteData.Move_Progress = false;
        UniteData.GameClear[UniteData.Difficulty - 1] = 1;
        UniteData.SaveUserData();
        b_playerMove = true;
    }

    private void bgStop()
    {
        GameObject[] Background = GameObject.FindGameObjectsWithTag("Background");

        for (int i = 0; i < Background.Length; i++)
        {
            Background[i].GetComponent<RepeatBG>().SetGameClearTrue();
        }
    }

    private void PlayerMove()
    {
        player.transform.position = player.transform.position + (player.transform.right * moveSpeed * Time.deltaTime);
    }

    public static void backgroundSetting(int value)
    {
        // ó������ ��� ��� �ʱ�ȭ
        foreach (var background in backgrounds_stt)
        {
            background.SetActive(false);
        }

        // ��� ����
        switch (value)
        {
            case 1:
                backgrounds_stt[0].SetActive(true); // W1E
                break;
            case 2:
                backgrounds_stt[1].SetActive(true); // W1N
                break;
            case 3:
                backgrounds_stt[2].SetActive(true); // W1H
                break;
            case 4:
                backgrounds_stt[3].SetActive(true); // world2_easy
                break;
            case 5:
                backgrounds_stt[4].SetActive(true); // world2_normal
                break;
            case 6:
                backgrounds_stt[5].SetActive(true); // world2_hard
                break;
            case 7:
                backgrounds_stt[6].SetActive(true); // ending
                break;
            default:
                backgrounds_stt[0].SetActive(true);
                break;
        }
    }
}
