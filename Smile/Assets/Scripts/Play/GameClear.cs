using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameClear : MonoBehaviour
{
    public GameObject bgGroup;
    public GameObject player;

    private float playerStartPositionX;

    private float xScreenSize;
    private float playerMaxMovePosX; // �÷��̾� �ִ� �̵� �Ÿ�

    private bool b_playerMove; // �÷��̾� �̵� �Ǵ� - ���� ����۽� �ʱ�ȭ

    [SerializeField] float moveSpeed; // Floor2�� RepeatBG Speed�� �Ȱ��� �ӵ��� ����

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
        if(b_playerMove)
        {
            if(player.transform.position.x < playerMaxMovePosX)
            {
                PlayerMove();
            }

            // ȭ�� �ۿ� �����ϸ� ���̵� �ƿ� �ִϸ��̼� �� �� ��ȯ
            else
            {
                Animator fadeAnimator = GameObject.Find("FadeOut_Clear").GetComponent<Animator>();
                
                fadeAnimator.SetBool("IsStartFade", true);
            }
            
        }
    }

    public void ClearGame()
    {
        bgStop();
        UniteData.Move_Progress = false;
        UniteData.GameClear[UniteData.Difficulty] = true;
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
