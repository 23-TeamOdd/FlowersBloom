using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int moveSpeed;

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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            // ���Ϳ� ������ ���߰� RN �� ȣ��
            moveSpeed = 0;
            Debug.Log("Go to RN");


            //�ִϸ��̼� �ֱ�

            //�� �ٷ� �̵�
            scenePass.SceneLoadStart();
        }
    }
}
