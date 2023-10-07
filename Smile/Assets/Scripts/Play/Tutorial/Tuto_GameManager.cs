using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto_GameManager : MonoBehaviour
{
    static public GameObject player;
    private bool b_playerMove; // �÷��̾� �̵� �Ǵ� - ���� ����۽� �ʱ�ȭ

    public GameObject Player_prefab;

    private float playerStartPositionX;
    private float xScreenSize;
    private float playerMaxMovePosX; // �÷��̾� �ִ� �̵� �Ÿ�

    public Sprite lifepoint_images; // ������ ��� ����Ʈ �̹���
    public GameObject[] lifepoints; // �������Ʈ ������Ʈ

    [SerializeField] float moveSpeed; // Floor2�� RepeatBG Speed�� �Ȱ��� �ӵ��� ����

    private void Awake()
    {
        UniteData.finishGame = false;

        Player_prefab.SetActive(true);
        player = Player_prefab;

        for (int i = 0; i < lifepoints.Length; i++)
        {
            Image img = lifepoints[i].GetComponent<Image>();
            img.sprite = lifepoint_images;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialized();
        xScreenSize = Camera.main.orthographicSize * Camera.main.aspect * 2;
        playerStartPositionX = player.transform.position.x;
        playerMaxMovePosX = playerStartPositionX + xScreenSize;
    }

    public void Initialized()
    {
        b_playerMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (b_playerMove)
        {
            if (player.transform.position.x < playerMaxMovePosX)
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
        //storyScriptGroup.SetActive(true);

        UniteData.ReStart = true;
        UniteData.Move_Progress = false;
        //UniteData.GameClear[UniteData.Difficulty - 1] = 1; // Ʃ�丮�� ���� Ŭ���� �����ؾ��ϳ�??
        //UniteData.SaveUserData();
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
}
