/*
*��� ��ü���� ������ ����ϴ� ��ũ��Ʈ (Ŭ���� ���)
*
*���� ��ǥ
*-����� �̺�Ʈ ����
*-����� Ŭ�� ����
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class node_management_click : MonoBehaviour
{
    public GameObject node_prefab;
    public GameObject ring;

    public UnityEvent<GameObject> onClick;
    public Animator animator;
    public Animator drag;
    //public node_delete nd;

    public static float EASY_FPS = 120f;
    public static float NORMAL_FPS = 96f;
    public static float HARD_FPS = 78f;


    public void node_click_event(GameObject clickObject)
    {
        //Ŭ�� ȿ������ �ο��մϴ�
        AudioSource se = GameObject.Find("ClickEffect").GetComponent<AudioSource>();
        se.volume = UniteData.Effect;
        se.Play();

        node_click_timing();
        
        delete_node_after_click();
    }

    //Ŭ�� Ÿ�̹� ���� �Լ�
    public void node_click_timing()
    {
        // Ŭ�� �ð��� ���� ������ Ÿ�̹��� ���������� ���� �� [MISS]
        if (ring.transform.localScale.x>0.25f)//(elapsedTime < ENTRANCE_UNSATISFACTORY_TOUCH)
        {
            UniteData.Node_LifePoint -= 1;
            if (Node_Result.Miss_Node_Click())
            {
                node.UnPassed = true;
                UniteData.lifePoint--;
            }
        }
        // Ŭ�� �ð��� ���� ������ Ÿ�̹��� ���������� ���� �� [FAST]
        else if (ring.transform.localScale.x <= 0.25f && ring.transform.localScale.x > 0.21f)
        {
            Debug.Log("FAST");
        }
        // Ŭ�� �ð��� �������� Ÿ�̹� [SUCCESS]
        else if (ring.transform.localScale.x <= 0.21f && ring.transform.localScale.x > 0.16f)
        {
            Debug.Log("SUCCESS");
        }
        // Ŭ�� �ð��� �������� Ÿ�̹��� �������� ���� �� [SLOW]
        else if (ring.transform.localScale.x <= 0.16f && ring.transform.localScale.x > 0.14f)
        {
            Debug.Log("SLOW");
        }
        // Ŭ�� �ð��� ���� ������ Ÿ�̹��� �������� ���� �� [MISS]
        else if (ring.transform.localScale.x <= 0.14f)
        {
            UniteData.Node_LifePoint -= 1;
            if (Node_Result.Miss_Node_Click())
            {
                node.UnPassed = true;
                UniteData.lifePoint--;
            }
        }
    }

    //�ִϸ��̼��� ���� ������ �ð��� Ȯ���ϴ� �Լ�
    public float call_animation_time()
    {
        // ���� �ִϸ��̼� ���¸� �����ɴϴ�.
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        return stateInfo.normalizedTime * stateInfo.length;
    }

    private void Awake()
    {
        animator.SetInteger("Difficulty", UniteData.Difficulty);
    }

    private void Start()
    {
        
    }

    void Update()
    {
        // ���� ���� ���� ���콺 ���� ��ư�� Ŭ���Ǿ��� ��
        if (Input.GetMouseButtonDown(0) && UniteData.Move_Progress==true)
        {
            // ���콺 ��ġ�� ȭ�� �������� ���� �������� ��ȯ�մϴ�.
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ���콺 ��ġ���� Ray�� �����մϴ�.
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Ray�� ���� �浹�ߴ��� Ȯ��
            if (hit.collider != null)
            {
                // ��尡 Ŭ���Ǿ��� ��
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    // ��� Ŭ�� �̺�Ʈ ����
                    onClick.Invoke(gameObject);
                }
            }
        }

        if (ring.transform.localScale.x <= 0.135f)
        {
            //��� ����
            UniteData.Node_LifePoint -= 1;

            //���ӽ���ó�� -> ��ȸ ����Ʈ1 ����(PlyaerController���� ó������) / ĳ���� ��� ����Ʈ -1
            if (Node_Result.Miss_Node_Click())
            {
                node.UnPassed = true;
                UniteData.lifePoint--;
            }

            // ����� Prefab�� �����մϴ�.
            delete_node_after_click();
        }
    }

    public void delete_node_after_click()
    {
        //����� Prefab�� �����մϴ�.
        Destroy(node_prefab);

        node.LineIndex = node.LineIndex - 1; //�� ���� ���µ� ���ϴϱ� ���������� �ٸ� �ҽ��ڵ忡 ���� ��� [HACK]

        UniteData.Node_Click_Counter += 1;
    }
}
