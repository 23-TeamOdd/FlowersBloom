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
}
