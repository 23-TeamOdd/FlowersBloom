using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameclearManager : MonoBehaviour
{
    private ReStartGame s_rsg;
    // Start is called before the first frame update
    void Start()
    {
        //�������� �ʱ�ȭ
        UniteData.notePoint = 2; // ��ȸ ����Ʈ
        UniteData.lifePoint = 3; // ��� ����Ʈ    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoMain()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RetryGame()
    {
        UniteData.ReStart = true;
        SceneManager.LoadScene("Play");
    }

    public void NextStage()
    {
        UniteData.Difficulty++;

        UniteData.Move_Progress = true;
        UniteData.lifePoint = 3;
        UniteData.notePoint = 2;
        UniteData.Play_Scene_Time = 0f;
        UniteData.Player_Location_Past = Vector2.zero;
        UniteData.ReStart = true;

        if(UniteData.Difficulty < 5) // world 2 easy ���ķδ� ���
            SceneManager.LoadScene("Play");

    }
}
