/*
*����� �������� �����ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*-��� ���� ��ɾ�
*-���� �ð��� ���� ����� ���� ����
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node_delete : MonoBehaviour
{
    public GameObject node_prefab;
    public node_management n_m;

    private float MAX_TIME_EASY = node_management.EASY_FPS / 60f;
    private float MAX_TIME_NORMAL = node_management.NORMAL_FPS / 60f;
    private float MAX_TIME_HARD = node_management.HARD_FPS / 60f;

    private float MAX_TIME = node_management.EASY_FPS / 60f;

    public void delete_node_after_click()
    {
        //����� Prefab�� �����մϴ�.
        Destroy(node_prefab);
    }

    private IEnumerator delete_node_AFK_state()
    {
        // ����� �ð��� ����մϴ�.
        float waitTime = MAX_TIME - n_m.call_animation_time();

        // ������ ���� �ʵ��� ��� �ð��� �����մϴ�.
        if (waitTime < 0)
        {
            waitTime = 0;
        }

        // ���� �ð���ŭ ����մϴ�.
        yield return new WaitForSeconds(waitTime);

        //����ó��
        UnityEngine.Debug.Log("MISS: NON-CLICK");

        // ����� Prefab�� �����մϴ�.
        Destroy(node_prefab);
    }

    private void Start()
    {
        switch(node.difficulty)
        {
            case 1:
                MAX_TIME = MAX_TIME_EASY;
                break;
            case 2:
                MAX_TIME = MAX_TIME_NORMAL;
                break;
            case 3:
                MAX_TIME = MAX_TIME_HARD;
                break;
            default:
                MAX_TIME = MAX_TIME_EASY;
                break;
        }

        StartCoroutine(delete_node_AFK_state());
    }
}
