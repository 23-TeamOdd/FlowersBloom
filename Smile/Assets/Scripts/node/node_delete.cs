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
using UnityEngine.SceneManagement;

public class node_delete : MonoBehaviour
{
    public GameObject node_prefab;
    public node_management_click n_m;

    private float MAX_TIME_EASY = node_management_click.EASY_FPS / 60f;
    private float MAX_TIME_NORMAL = node_management_click.NORMAL_FPS / 60f;
    private float MAX_TIME_HARD = node_management_click.HARD_FPS / 60f;

    private float MAX_TIME = node_management_click.EASY_FPS / 60f;

    public void delete_node_after_click()
    {
        //����� Prefab�� �����մϴ�.
        Destroy(node_prefab);

        node.LineIndex = node.LineIndex - 1; //�� ���� ���µ� ���ϴϱ� ���������� �ٸ� �ҽ��ڵ忡 ���� ��� [HACK]

        UniteData.Node_Click_Counter += 1;
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

        //��� ����
        UniteData.Node_LifePoint -= 1;

        //���ӽ���ó�� -> ��ȸ ����Ʈ1 ����(PlyaerController���� ó������) / ĳ���� ��� ����Ʈ -1
        if (Node_Result.Miss_Node_Click())
        {
            //���� ��ȸ����Ʈ�� ������ ���� �� �н�
            //if (UniteData.notePoint > 0)
            //{
                node.UnPassed = true;
                UniteData.lifePoint--;

                //��ȸ ����Ʈ ����
                //UniteData.notePoint--;
            //}
            /*else
            {
                Animator fadeAnimator = GameObject.Find("FadeOut").GetComponent<Animator>();
                // ���̵� �ƿ� �ִϸ��̼� ���� ���� ��ȯ�մϴ�.
                fadeAnimator.SetBool("IsStartFade", true);
            }*/
        }

        // ����� Prefab�� �����մϴ�.
        delete_node_after_click();
    }

    private void Start()
    {
        /*switch(UniteData.Difficulty)
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
        }*/

        StartCoroutine(delete_node_AFK_state());
    }
}
