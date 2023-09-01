/*
*��� ��ü���� ������ ����ϴ� ��ũ��Ʈ (�巡���� ���)
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

public class node_management_drag : MonoBehaviour
{
    public GameObject node_prefab;
    public GameObject ring;
    public GameObject bright; 
    private GameObject shadow;
    private CircleCollider2D collider;

    public Vector2 ping;

    public UnityEvent<GameObject> onClick;
    public Animator animator;

    private float Drag_fast = 16f;

    private bool move_unlock = false;

    public void node_drag_event(GameObject clickObject)
    {
        shadow = GameObject.Find("shadow");

        //Ŭ�� ȿ������ �ο��մϴ�
        AudioSource se = GameObject.Find("ClickEffect").GetComponent<AudioSource>();
        se.volume = UniteData.Effect;
        se.Play();

        node_drag_timing();

        //delete_node_after_click();
    }

    //�巡�� Ÿ�̹� ���� �Լ�
    public void node_drag_timing()
    {
        //�巡�� �� Ŭ�� �ð��� ���� ������ Ÿ�̹��� ���������� ���� �� [MISS]
        if (ring.transform.localScale.x > 0.25f)//(elapsedTime < ENTRANCE_UNSATISFACTORY_TOUCH)
        {
            UniteData.Node_LifePoint -= 1;
            if (Node_Result.Miss_Node_Click())
            {
                node.UnPassed = true;
                UniteData.lifePoint--;
            }
            delete_node_after_click();
        }
        //�巡�� �� Ŭ�� �ð��� ���� ������ Ÿ�̹��� ���������� ���� �� [FAST]
        else if (ring.transform.localScale.x <= 0.25f && ring.transform.localScale.x > 0.21f)
        {
            //ring ������Ʈ ��Ȱ��ȭ
            ring.SetActive(false);
            //bright Ȱ��ȭ
            bright.SetActive(true);
            move_unlock = true;
            Debug.Log("FAST");
        }
        //�巡�� �� Ŭ�� �ð��� �������� Ÿ�̹� [SUCCESS]
        else if (ring.transform.localScale.x <= 0.21f && ring.transform.localScale.x > 0.16f)
        {
            //ring ������Ʈ ��Ȱ��ȭ
            ring.SetActive(false);
            //bright Ȱ��ȭ
            bright.SetActive(true);
            move_unlock = true;
            Debug.Log("SUCCESS");
        }
        //�巡�� �� Ŭ�� �ð��� �������� Ÿ�̹��� �������� ���� �� [SLOW]
        else if (ring.transform.localScale.x <= 0.16f && ring.transform.localScale.x > 0.14f)
        {
            //ring ������Ʈ ��Ȱ��ȭ
            ring.SetActive(false);
            //bright Ȱ��ȭ
            bright.SetActive(true);
            move_unlock = true;
            Debug.Log("SLOW");
        }
        //�巡�� �� Ŭ�� �ð��� ���� ������ Ÿ�̹��� �������� ���� �� [MISS]
        else if (ring.transform.localScale.x <= 0.14f)
        {
            UniteData.Node_LifePoint -= 1;
            if (Node_Result.Miss_Node_Click())
            {
                node.UnPassed = true;
                UniteData.lifePoint--;
            }
            delete_node_after_click();
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

        switch (UniteData.Difficulty)
        {
            case 4:
                animator.SetInteger("Difficulty", 1);
                break;
        }

        //bright ��Ȱ��ȭ
        bright.SetActive(false);
    }

    private void Start()
    {
        //ping�� ��ġ�� nodes_prefab�� ��ġ���� 500�Ÿ���ŭ ������ ���� ��ġ�Ѵ�.
        //ping.transform.position = new Vector3(node_prefab.transform.position.x, node_prefab.transform.position.y + 500, node_prefab.transform.position.z);
        //Debug.Log(ping);

        collider= node_prefab.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        Vector2 mousePosition = new Vector2(10000, 10000);
        // ���� ���� ���� ���콺 ���� ��ư�� Ŭ���Ǿ��� ��
        if (Input.GetMouseButtonDown(0) && UniteData.Move_Progress == true)
        {
            // ���콺 ��ġ�� ȭ�� �������� ���� �������� ��ȯ�մϴ�.
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ���콺 ��ġ���� Ray�� �����մϴ�.
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Ray�� ���� �浹�ߴ��� Ȯ��
            if (hit.collider != null)
            {
                // ��尡 Ŭ���Ǿ��� ��
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    collider.radius = 2.0f;
                    // ��� Ŭ�� �̺�Ʈ ����
                    onClick.Invoke(gameObject);
                }
            }
        }

        //��� Ŭ�� ���� �� ��� �̵� Ȱ��ȭ
        if(Input.GetMouseButton(0) && UniteData.Move_Progress == true && move_unlock==true)
        {
            // ���콺 ��ġ�� ȭ�� �������� ���� �������� ��ȯ�մϴ�.
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ���콺 ��ġ���� Ray�� �����մϴ�.
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Ray�� ���� �浹�ߴ��� Ȯ��
            if (hit.collider != null)
            {
                // ��尡 Ŭ���Ǿ��� ��
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    // ��� Ŭ�� �̺�Ʈ ����
                    //Debug.Log(mousePosition+" ȣ����");
                }
            }
            else
            {
                // ��� Ŭ�� �̺�Ʈ ����
                Debug.Log("�������� ����");
                UniteData.Node_LifePoint -= 1;
                if (Node_Result.Miss_Node_Click())
                {
                    node.UnPassed = true;
                    UniteData.lifePoint--;
                }
                delete_node_after_click();
            }
        }

        //��� �巡�� ���� ���� ���� �������� �� ó��
        if(Input.GetMouseButtonUp(0) && UniteData.Move_Progress == true && move_unlock == true)
        {
            // ��� Ŭ�� �̺�Ʈ ����
            Debug.Log("����");
            delete_node_after_click();
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

    private void FixedUpdate()
    {
        if(move_unlock)
        {
            //��尡 100frame ���� node_prefab�� vector2 variation�� ping ������ �̵� Like Linearity
            Vector2 v2 = Vector2.MoveTowards(node_prefab.transform.position, ping, Drag_fast);
            node_prefab.transform.position = v2;

            if (node_prefab.transform.position.x == ping.x)
            {
                if (node_prefab.transform.position.y == ping.y)
                {
                    //��尡 ping�� �����ϸ� ��� �̵��� �����.
                    move_unlock = false;
                    Debug.Log("����");
                    delete_node_after_click();
                }
            }
        }
    }

    public void delete_node_after_click()
    {
        shadow = GameObject.Find("shadow");
        //shadow.transform.parent = node_prefab.transform;
        shadow.transform.SetParent(node_prefab.transform, false);

        //����� Prefab�� �����մϴ�.
        Destroy(node_prefab);

        node.LineIndex = node.LineIndex - 2; //�� ���� ���µ� ���ϴϱ� ���������� �ٸ� �ҽ��ڵ忡 ���� ��� [HACK]

        UniteData.Node_Click_Counter += 2;
    }
}
