using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtnTest : MonoBehaviour
{
    public IScenePass sceneLoader;
    // Start is called before the first frame update
    void Start()
    {
        /*
         * �ε�â ����� ���� �̸� �ε�� ���ݽ��ϴ�!
        //Scene�� �񵿱������� ������ �̸� �ε�
        sceneLoader=GetComponent<IScenePass>();
        sceneLoader.LoadSceneAsync("Play");
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBtn()
    {
        //SceneManager.LoadScene("Play");

        //���� ���� �������� �ʱ�ȭ
        UniteData.lifePoint = 3;
        UniteData.notePoint = 2;
        UniteData.Play_Scene_Time = 0f;
        UniteData.Player_Location_Past = Vector2.zero;

        //�ε� �ҷ�����
        //sceneLoader.SceneLoadStart("Play");

        LoadingSceneManager.LoadScene("Play");
    }
}
