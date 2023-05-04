using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBG : MonoBehaviour
{
    [SerializeField]float speed;

    [SerializeField] float posValue;

    private Vector2 startPos;
    private float newPos;

    private bool gameClear;
    private static Game_Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        //6�� �� ���� ��ġ�� ȸ�� (�̵� �� �ִϸ��̼� 2�� + �̵� 4��)
        timer = new Game_Timer(UniteData.Play_Scene_Time < 6f ? UniteData.Play_Scene_Time : UniteData.Play_Scene_Time - 6f);
        //Debug.Log("Start Point: "+UniteData.Player_Location_Past);
        Initialized();
    }

    public void Initialized()
    {
        startPos = transform.position;
        //Debug.Log("repeatBG_I : " + transform.position.x);
        //transform.position = GameObject.Find("OriginPos").transform.position;

        gameClear = false;
        newPos = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("CLASS: "+timer.GetTime());
        if(!gameClear)
        {
            if(UniteData.Move_Progress)
            {
                newPos = Mathf.Repeat(timer.GetTime() * speed, posValue);
            }
            else
            {
                //���� �ð� ���
                UniteData.Play_Scene_Time = timer.GetTime();
            }
        }
        transform.position = (startPos + Vector2.left * newPos);
    }

    public void SetGameClearTrue()
    {
        //UniteData.Player_Location_Past = Vector2.zero;
        gameClear = true;
    }
}
