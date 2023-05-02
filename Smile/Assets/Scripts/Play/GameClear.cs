using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    public GameObject bgGroup;
    public GameObject player;

    private float xScreenHalfSize;
    private bool b_playerMove = false; // �÷��̾� �̵� �Ǵ�

    [SerializeField] float moveSpeed; // Floor2�� RepeatBG Speed�� �Ȱ��� �ӵ��� ����

    // Start is called before the first frame update
    void Start()
    {
        b_playerMove = false;
        xScreenHalfSize = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(b_playerMove)
        {
            if(player.transform.position.x < xScreenHalfSize * 2)
            {
                PlayerMove();
            }
            
        }
    }

    public void ClearGame()
    {
        bgStop();
        b_playerMove = true;
    }

    private void bgStop()
    {
        for (int i = 0; i < bgGroup.transform.childCount; i++)
        {
            bgGroup.transform.GetChild(i).GetComponent<RepeatBG>().setGameClearTrue();
        }
    }

    private void PlayerMove()
    {
        player.transform.position = player.transform.position + (player.transform.right * moveSpeed * Time.deltaTime);
    }
}
