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
        //Scene�� �񵿱������� ������ �̸� �ε�
        sceneLoader=GetComponent<IScenePass>();
        sceneLoader.LoadSceneAsync("Play");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBtn()
    {
        //SceneManager.LoadScene("Play");
        
        //�ε� �ҷ�����
        sceneLoader.SceneLoadStart();
    }
}
