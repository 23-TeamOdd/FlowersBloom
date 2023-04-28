using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private int moveSpeed;

    public IScenePass scenePass;
    public GameObject Cut_Scene_prefab;

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
                //scenePass.SceneLoadStart();

                //�� �ִϸ��̼��� �� �� �̵�
                StartCoroutine(LoadCutScene());
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
        }
    }


    public static Vector3 CamabsolutePosition = new Vector3(0, 0, 0);
    private IEnumerator LoadCutScene()
    {
        //���� ������Ʈ �� UI_Touch Tag�� SetActive(false)�� �����Ѵ�
        GameObject[] UI_Touch = GameObject.FindGameObjectsWithTag("PlayScene_UI");
        foreach (GameObject UI in UI_Touch)
        {
            UI.SetActive(false);
        }

        GameObject Cam = GameObject.Find("Main Camera");

        //ī�޶��� ������ǥ�� �����´�
        CamabsolutePosition = Cam.transform.localPosition + new Vector3(0, 0, 10);

        //�ִϸ��̼� �ֱ�

        //�ƾ� �� �����
        //Instantiate(Cut_Scene_prefab, CamabsolutePosition, Quaternion.identity);
        Cut_Scene_prefab.transform.position = CamabsolutePosition;

        //�ִϸ��̼� ����
        Animator anim = Cut_Scene_prefab.GetComponent<Animator>();
        anim.SetBool("IsStart", true);

        //�ƾ� �ִϸ��̼��� ������ �� �ٷ� �̵�
        yield return new WaitForSeconds(1.17f);
        scenePass.SceneLoadStart();
    }
}
