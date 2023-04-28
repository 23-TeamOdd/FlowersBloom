using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private int moveSpeed;

    public IScenePass scenePass;

    // ��ȸ ����Ʈ ����
    public int notePoint = 2; // ��ȸ ����Ʈ
    public GameObject[] go_notePoints; // ��ȸ ����Ʈ ������Ʈ

    // ��� ����Ʈ ����
    public int lifePoint = 3; // ��� ����Ʈ

    // Start is called before the first frame update
    void Start()
    {
        scenePass = GetComponent<IScenePass>();
        scenePass.LoadSceneAsync("InGame-RN");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + (transform.right * moveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            // ���Ϳ� ������ ���߰� RN �� ȣ��
            Debug.Log("Go to RN");
            
            //moveSpeed = 0;

            // ��ȸ�� �����ִٸ� �����ϰ� �� �̵�
            if (notePoint > 0)
            {
                notePoint--;
                go_notePoints[notePoint].gameObject.SetActive(false);

                //�� �ٷ� �̵�
                scenePass.SceneLoadStart();
            }

            // ��ȸ�� 0�̶�� ��� ����Ʈ ����
            else if (notePoint == 0)
            {
                // ��� ����Ʈ�� �����ִٸ� ����
                if(lifePoint > 0)
                {
                    lifePoint--;
                }
                // �������� �ʴٸ� ���� ����    
                else if(lifePoint == 0)
                {
                    Debug.Log("GameOver");
                }
            }
                

            //�ִϸ��̼� �ֱ�

            
        }
    }
}
