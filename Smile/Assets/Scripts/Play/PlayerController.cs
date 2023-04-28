using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    public GameObject Cut_Scene_prefab;

    public IScenePass scenePass;
    // Start is called before the first frame update
    void Start()
    {
        scenePass = GetComponent<IScenePass>();
        scenePass.LoadSceneAsync("InGame-RN");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (transform.right * moveSpeed * Time.deltaTime);
    }


    public static Vector3 CamabsolutePosition = new Vector3(0, 0, 0);


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            // ���Ϳ� ������ ���߰� RN �� ȣ��
            moveSpeed = 0;
            Debug.Log("Go to RN");

            StartCoroutine(LoadCutScene());
        }
    }

    private IEnumerator LoadCutScene()
    {
        //���� ������Ʈ �� UI_Touch Tag�� SetActive(false)�� �����Ѵ�
        GameObject[] UI_Touch = GameObject.FindGameObjectsWithTag("UI_Touch");
        foreach (GameObject UI in UI_Touch)
        {
            UI.SetActive(false);
        }

        GameObject Player = GameObject.Find("Player");
        GameObject Cam = GameObject.Find("Player/Main Camera");

        //ī�޶��� ������ǥ�� �����´�
        CamabsolutePosition = Player.transform.TransformPoint(Cam.transform.localPosition + new Vector3(0, 0, 10));

        //�ִϸ��̼� �ֱ�

        //�ƾ� �� �����
        //Instantiate(Cut_Scene_prefab, CamabsolutePosition, Quaternion.identity);
        Cut_Scene_prefab.transform.position = CamabsolutePosition;

        //�ִϸ��̼� ����
        Animator anim = Cut_Scene_prefab.GetComponent<Animator>(); 
        anim.SetBool("IsStart", true);

        //�ƾ� �ִϸ��̼��� ������ �� �ٷ� �̵�
        yield return new WaitForSeconds(1.15f);
        scenePass.SceneLoadStart();
    }
}
