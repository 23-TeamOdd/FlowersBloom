/*
*����� ��ü���� ������ ��� �� �����ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*-����� �ϰ����� ���� ���
*
*���̵� ���� �� ��������
*-Initialize_node_setting() ���� (���̵��� ���� ����� ���� ����)
*-node_management�� �ð��� ����
*-AnimationClip ��� ���� Ÿ�̹� ����
*-node_delete�� MAX_TIME ���� ��
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Node_data
{
    public Vector2 vector2 = new Vector2();
    public Sprite procedure;

    public Node_data(Vector2 _vector2, Sprite _procedure)
    {
        this.vector2 = _vector2;

        //TODO: �̹��� ���� �ȵ� �� ����ó�� �ʿ�
        this.procedure = _procedure;
    }
}

public class node : MonoBehaviour
{
    private List<Node_data> node_location = new List<Node_data>();
    public GameObject nodes_prefab;

    [Header("�Ʒ��� �׸񿡴ٰ� ��Ʈ�� �̹����� ������ �˴ϴ�")]
    public Sprite Node_image_A;
    public Sprite Node_image_B;
    public Sprite Node_image_C;
    public Sprite Node_image_D;

    [Header("�Ʒ��� �׸񿡴ٰ� �ش� �ƾ��� ���̵��� �����մϴ�")]
    public static int difficulty = 3; //difault value 1

    [Header("��Ʈ�� ������ �����մϴ�")]
    public float radius_MIN = 420f; //difault value 420f
    public float radius_MAX = 1000f; //difault value 1000f

    //��带 ����Ʈ�� ������ ���� �ϳ��� ���ʷ� ��ġ�ϴ� �Լ�
    void node_placement(int node_array)
    {
        if (node_location.Count == 0)
        {
            return;
        }
        else
        {
            SpriteRenderer sr = nodes_prefab.GetComponent<SpriteRenderer>();
            sr.sprite = node_location[node_array].procedure;

            Instantiate(nodes_prefab, node_location[node_array].vector2, Quaternion.identity);
        }
    }


    //��带 �����ϴ� �κ��Դϴ�. Coroutine���� ����
    private IEnumerator D_Coroutine()
    {
        UnityEngine.Debug.Log("��ǥ ���� �Ϸ� ��� ���...");
        yield return new WaitForSeconds(1.3f);

        for (int i=0; i<node_location.Count; i++)
        {
            yield return new WaitForSeconds(set_node_wait());
            node_placement(i);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //����� �ʱ� ������ �����Ѵ�
        Initialize_node_setting();

        StartCoroutine(D_Coroutine());
    }

    private void Initialize_node_setting()
    {
        switch (difficulty)
        {
            case 1: //easy
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_A));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_B));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_C));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_D));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_A));
                break;

            case 2: //normal
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_A));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_B));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_C));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_D));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_A));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_B));
                break;

            case 3: //hard
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_A));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_B));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_C));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_D));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_A));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_B));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_C));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_D));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_A));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_B));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_C));
                break;

            default: //default
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_A));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_B));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_C));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_D));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_A));

                difficulty = 1;
                break;
        }
    }



    private int call_random()
    {
        System.Random r = new System.Random();
        return r.Next(-210000000, 210000000);
    }

    private Vector2 set_node_coordinate()
    {
        System.Random random = new System.Random(unchecked((int)((long)Thread.CurrentThread.ManagedThreadId + (DateTime.UtcNow.Ticks)) - call_random()));

        Vector2 vector = new Vector2(random.Next(-1300, 1301), random.Next(-500, 501));
        
        //��峢�� ���� �Ÿ��� �������� Ż��
        while (check_radius_between_nodes(vector))
        {
            vector = new Vector2(random.Next(-1300, 1301), random.Next(-500, 501));
        }


        return vector;
    }

    private float set_node_wait()
    {
        System.Random random = new System.Random(unchecked((int)((long)Thread.CurrentThread.ManagedThreadId + (DateTime.UtcNow.Ticks)) - call_random()));

        switch(difficulty)
        {
            case 1:
                return 0.2f * random.Next(5, 11);
            case 2:
                return 0.1f * random.Next(5, 9);
            case 3:
                return 0.1f * random.Next(3, 8);
            default:
                return 0.25f * random.Next(2, 6);
        }
    }

    private bool check_radius_between_nodes(Vector2 vector)
    {
        int node_set_locate_count=node_location.Count;

        for (int i = node_set_locate_count < 4 ? 0 : node_set_locate_count - 4; i < node_set_locate_count; i++) 
        {
            Vector2 call_vec = node_location[i].vector2;

            if (Vector2.Distance(vector, call_vec) < radius_MIN)
            {
                return true;
            }
            else if (Vector2.Distance(vector, call_vec) > radius_MAX)
            {
                return true;
            }
        }
        return false;
    }
}