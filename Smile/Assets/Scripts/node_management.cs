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

    public static float EASY_FPS = 120f;
    public static float NORMAL_FPS = 96f;
    public static float HARD_FPS = 78f;

    [Header("EASY MODE�� ��Ʈ ������ Ÿ�̹��� �����ϴ� ���� \n(������ ���콺 ������ �뺸����)")]
    [Tooltip("���� ������ Ÿ�̹��� ������ (sec ����)")]
    public float ENTRANCE_UNSATISFACTORY_TOUCH_EASY = (EASY_FPS - 30f) / 60f;
    [Tooltip("�������� Ÿ�̹��� ������ (sec ����)")]
    public float ENTRANCE_SUCCESSFULL_TOUCH_EASY = (EASY_FPS - 20f) / 60f;
    [Tooltip("�������� Ÿ�̹��� ���� (sec ����)")]
    public float EXIT_SUCCESSFULL_TOUCH_EASY = (EASY_FPS - 10f) / 60f;
    [Tooltip("���� ������ �κ��� ���� (sec ����)")]
    public float EXIT_UNSATISFACTORY_TOUCH_EASY = (EASY_FPS - 5f) / 60f;

    [Header("NORMAL MODE�� ��Ʈ ������ Ÿ�̹��� �����ϴ� ���� \n(������ ���콺 ������ �뺸����)")]
    [Tooltip("���� ������ Ÿ�̹��� ������ (sec ����)")]
    public float ENTRANCE_UNSATISFACTORY_TOUCH_NORMAL = (NORMAL_FPS - 24f) / 60f;
    [Tooltip("�������� Ÿ�̹��� ������ (sec ����)")]
    public float ENTRANCE_SUCCESSFULL_TOUCH_NORMAL = (NORMAL_FPS - 16f) / 60f;
    [Tooltip("�������� Ÿ�̹��� ���� (sec ����)")]
    public float EXIT_SUCCESSFULL_TOUCH_NORMAL = (NORMAL_FPS - 8f) / 60f;
    [Tooltip("���� ������ �κ��� ���� (sec ����)")]
    public float EXIT_UNSATISFACTORY_TOUCH_NORMAL = (NORMAL_FPS - 4f) / 60f;

    [Header("HARD MODE�� ��Ʈ ������ Ÿ�̹��� �����ϴ� ���� \n(������ ���콺 ������ �뺸����)")]
    [Tooltip("���� ������ Ÿ�̹��� ������ (sec ����)")]
    public float ENTRANCE_UNSATISFACTORY_TOUCH_HARD = (HARD_FPS - 19f) / 60f;
    [Tooltip("�������� Ÿ�̹��� ������ (sec ����)")]
    public float ENTRANCE_SUCCESSFULL_TOUCH_HARD = (HARD_FPS - 13f) / 60f;
    [Tooltip("�������� Ÿ�̹��� ���� (sec ����)")]
    public float EXIT_SUCCESSFULL_TOUCH_HARD = (HARD_FPS - 7f) / 60f;
    [Tooltip("���� ������ �κ��� ���� (sec ����)")]
    public float EXIT_UNSATISFACTORY_TOUCH_HARD = (HARD_FPS - 4) / 60f;


    private float ENTRANCE_UNSATISFACTORY_TOUCH = (EASY_FPS - 30f) / 60f;
    private float ENTRANCE_SUCCESSFULL_TOUCH = (EASY_FPS - 20f) / 60f;
    private float EXIT_SUCCESSFULL_TOUCH = (EASY_FPS - 10f) / 60f;
    private float EXIT_UNSATISFACTORY_TOUCH = (EASY_FPS - 5f) / 60f;

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

    private void Awake()
    {
        animator.SetInteger("Difficulty", UniteData.Difficulty);
    }

    private void Start()
    {
        switch(UniteData.Difficulty)
        {
            case 1:
                ENTRANCE_UNSATISFACTORY_TOUCH = ENTRANCE_UNSATISFACTORY_TOUCH_EASY;
                ENTRANCE_SUCCESSFULL_TOUCH = ENTRANCE_SUCCESSFULL_TOUCH_EASY;
                EXIT_SUCCESSFULL_TOUCH = EXIT_SUCCESSFULL_TOUCH_EASY;
                EXIT_UNSATISFACTORY_TOUCH = EXIT_UNSATISFACTORY_TOUCH_EASY;

                //Debug.Log("��� ���̵�: EASY");
                break;
            case 2:
                ENTRANCE_UNSATISFACTORY_TOUCH = ENTRANCE_UNSATISFACTORY_TOUCH_NORMAL;
                ENTRANCE_SUCCESSFULL_TOUCH = ENTRANCE_SUCCESSFULL_TOUCH_NORMAL;
                EXIT_SUCCESSFULL_TOUCH = EXIT_SUCCESSFULL_TOUCH_NORMAL;
                EXIT_UNSATISFACTORY_TOUCH = EXIT_UNSATISFACTORY_TOUCH_NORMAL;

                //Debug.Log("��� ���̵�: NORMAL");
                break;
            case 3:
                ENTRANCE_UNSATISFACTORY_TOUCH = ENTRANCE_UNSATISFACTORY_TOUCH_HARD ;
                ENTRANCE_SUCCESSFULL_TOUCH = ENTRANCE_SUCCESSFULL_TOUCH_HARD;
                EXIT_SUCCESSFULL_TOUCH = EXIT_SUCCESSFULL_TOUCH_HARD;
                EXIT_UNSATISFACTORY_TOUCH = EXIT_UNSATISFACTORY_TOUCH_HARD;

                //Debug.Log("��� ���̵�: HARD");
                break;
            default:
                ENTRANCE_UNSATISFACTORY_TOUCH = ENTRANCE_UNSATISFACTORY_TOUCH_EASY;
                ENTRANCE_SUCCESSFULL_TOUCH = ENTRANCE_SUCCESSFULL_TOUCH_EASY;
                EXIT_SUCCESSFULL_TOUCH = EXIT_SUCCESSFULL_TOUCH_EASY;
                EXIT_UNSATISFACTORY_TOUCH = EXIT_UNSATISFACTORY_TOUCH_EASY;
                break;
        }
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
