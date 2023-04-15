/*
*��� ��ü���� ������ ����ϴ� ��ũ��Ʈ
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

public class node_management : MonoBehaviour
{
    public GameObject node_prefab;

    public UnityEvent<GameObject> onClick;
    public Animator animator;
    public node_delete nd;

    [Header("��� ������ Ÿ�̹��� �����ϴ� ���� \n(������ ���콺 ������ �뺸����)")]
    [Tooltip("���� ������ Ÿ�̹��� ������ (sec ����)")]
    public float ENTRANCE_UNSATISFACTORY_TOUCH = 1.65f;
    [Tooltip("�������� Ÿ�̹��� ������ (sec ����)")]
    public float ENTRANCE_SUCCESSFULL_TOUCH = 1.85f;
    [Tooltip("�������� Ÿ�̹��� ���� (sec ����)")]
    public float EXIT_SUCCESSFULL_TOUCH = 2.05f;
    [Tooltip("���� ������ �κ��� ���� (sec ����)")]
    public float EXIT_UNSATISFACTORY_TOUCH = 2.15f;


    public void node_click_event(GameObject clickObject)
    {
        //UnityEngine.Debug.Log("��� Ŭ��: " + clickObject.name);
        node_click_timing();
        
        nd.delete_node_after_click();
    }

    //Ŭ�� Ÿ�̹� ���� �Լ�
    public void node_click_timing()
    {
        // �ִϸ��̼� Ŭ���� ��� �ð��� ����ϴ�.
        float elapsedTime = call_animation_time();

        //Debug.Log("Ŭ�� �ð�: " + elapsedTime + "s");

        // Ŭ�� �ð��� ���� ������ Ÿ�̹��� ���������� ���� �� [MISS]
        if (elapsedTime < ENTRANCE_UNSATISFACTORY_TOUCH)
        {
            Debug.Log("MISS");
        }
        // Ŭ�� �ð��� ���� ������ Ÿ�̹��� ���������� ���� �� [FAST]
        else if (elapsedTime > ENTRANCE_UNSATISFACTORY_TOUCH && elapsedTime < ENTRANCE_SUCCESSFULL_TOUCH)
        {
            Debug.Log("FAST");
        }
        // Ŭ�� �ð��� �������� Ÿ�̹��� ���������� ���� �� [SUCCESS]
        else if (elapsedTime > ENTRANCE_SUCCESSFULL_TOUCH && elapsedTime < EXIT_SUCCESSFULL_TOUCH)
        {
            Debug.Log("SUCCESS");
        }
        // Ŭ�� �ð��� �������� Ÿ�̹��� �������� ���� �� [SLOW]
        else if (elapsedTime > EXIT_SUCCESSFULL_TOUCH && elapsedTime < EXIT_UNSATISFACTORY_TOUCH)
        {
            Debug.Log("SLOW");
        }
        // Ŭ�� �ð��� ���� ������ Ÿ�̹��� �������� ���� �� [MISS]
        else if (elapsedTime > EXIT_UNSATISFACTORY_TOUCH)
        {
            Debug.Log("MISS");
        }
    }

    //�ִϸ��̼��� ���� ������ �ð��� Ȯ���ϴ� �Լ�
    public float call_animation_time()
    {
        // ���� �ִϸ��̼� ���¸� �����ɴϴ�.
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        return stateInfo.normalizedTime * stateInfo.length;
    }


    void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���Ǿ��� ��
        if (Input.GetMouseButtonDown(0))
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
    }
}
