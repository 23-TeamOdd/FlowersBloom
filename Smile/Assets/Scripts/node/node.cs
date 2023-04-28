/*
*����� ��ü���� ������ ��� �� �����ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*-����� �ϰ����� ���� ���
*-��尣 Line Renderer�� ���� ����
*
*���̵� ���� �� ��������
*-Initialize_node_setting() ���� (���̵��� ���� ����� ���� ����)
*-node_management�� �ð��� ����
*-AnimationClip ��� ���� Ÿ�̹� ����
*-node_delete�� MAX_TIME ���� 
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Node_data
{
    public Vector3 vector3 = new Vector3();
    public Sprite procedure;

    public Node_data(Vector3 _vector3, Sprite _procedure)
    {
        this.vector3 = _vector3;

        //TODO: �̹��� ���� �ȵ� �� ����ó�� �ʿ�
        this.procedure = _procedure;
    }
}

public class node : MonoBehaviour
{
    private List<Node_data> node_location = new List<Node_data>();
    public GameObject nodes_prefab;
<<<<<<< HEAD
    public LineRenderer line_renderer;
    public Transform ParentObject;
=======
    public Transform parentBack;
    public LineRenderer line_renderer;
>>>>>>> CutBackup

    [Header("�Ʒ��� �׸񿡴ٰ� ��Ʈ�� �̹����� ������ �˴ϴ�")]
    public Sprite Node_image_A;
    public Sprite Node_image_B;
    public Sprite Node_image_C;
    public Sprite Node_image_D;

    [Header("�Ʒ��� �׸񿡴ٰ� �ش� �ƾ��� ���̵��� �����մϴ�")]
    public static int difficulty = 1; //difault value 1

    [Header("��Ʈ�� ������ �����մϴ�")]
    public float radius_MIN = 420f; //difault value 420f
    public float radius_MAX = 1000f; //difault value 1000f


    public static int LineIndex = 0; //�� ���� ���µ� ���ϴϱ� ���������� �ٸ� �ҽ��ڵ忡 ���� ��� [HACK]


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

<<<<<<< HEAD
            Instantiate(nodes_prefab, node_location[node_array].vector2, Quaternion.identity, ParentObject);
        }
    }

    private void Insert_Line(List<Vector2> v)
    {
        List<Vector2> vector = new List<Vector2>(v);
=======
            Instantiate(nodes_prefab, node_location[node_array].vector3, Quaternion.identity);//, parentBack);
        }
    }

    private void Insert_Line(List<Vector3> v)
    {
        List<Vector3> vector = new List<Vector3>(v);
>>>>>>> CutBackup

        //list ���� ���ҵ��� Reverse ��Ų��
        vector.Reverse();

        //line renderer�� ��ǥ�� �ִ´�
        for (int i = 0; i < vector.Count; i++)
        {
            line_renderer.positionCount = LineIndex; 
            line_renderer.SetPosition(i, vector[i]);
        }
    }

    public void Delete_Line() //���� �̻��
    {
        LineIndex = LineIndex - 1; //�� ���� ���µ� ���ϴϱ� ���������� �ٸ� �ҽ��ڵ忡 ���� ��� [HACK]
        UnityEngine.Debug.Log("����");
    }


    //��带 �����ϴ� �κ��Դϴ�. Coroutine���� ����
    private IEnumerator D_Coroutine()
    {
<<<<<<< HEAD
        List<Vector2> vector = new List<Vector2>();
=======
        List<Vector3> vector = new List<Vector3>();
>>>>>>> CutBackup
        UnityEngine.Debug.Log("��ǥ ���� �Ϸ� ��� ���...");
        yield return new WaitForSeconds(1.5f);

        for (int i=0; i<node_location.Count; i++)
        {
<<<<<<< HEAD
            vector.Add(node_location[i].vector2);
            yield return new WaitForSeconds(set_node_wait());
            node_placement(i);
=======
            vector.Add(node_location[i].vector3);
            yield return new WaitForSeconds(set_node_wait());
            node_placement(i);

>>>>>>> CutBackup
            LineIndex = LineIndex + 1; //�� ���� ���µ� ���ϴϱ� ���������� �ٸ� �ҽ��ڵ忡 ���� ��� [HACK]
            //Insert_Line(vector);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        //����� �ʱ� ������ �����Ѵ�
        Initialize_node_setting();

        StartCoroutine(D_Coroutine());
    }

    private void Update()
    {
        //line_renderer.positionCount = LineIndex; //�� ���� ���µ� ���ϴϱ� ���������� �ٸ� �ҽ��ڵ忡 ���� ��� [HACK]
<<<<<<< HEAD

        //���� ������Ʈ �� UI_Touch Tag�� SetActive(false)�� �����Ѵ�
        GameObject[] UI_Touch = GameObject.FindGameObjectsWithTag("UI_Touch");
        foreach (GameObject UI in UI_Touch)
        {
            UI.SetActive(false);
        }
=======
>>>>>>> CutBackup
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

    private Vector3 set_node_coordinate()
    {
        System.Random random = new System.Random(unchecked((int)((long)Thread.CurrentThread.ManagedThreadId + (DateTime.UtcNow.Ticks)) - call_random()));

        Vector3 vector = new Vector3(random.Next(-1300, 1301), random.Next(-500, 501), 0);
        
        //��峢�� ���� �Ÿ��� �������� Ż��
        while (check_radius_between_nodes(vector))
        {
            vector = new Vector3(random.Next(-1300, 1301), random.Next(-500, 501), 0);
        }

        return vector;
    }

    private float set_node_wait()
    {
        System.Random random = new System.Random(unchecked((int)((long)Thread.CurrentThread.ManagedThreadId + (DateTime.UtcNow.Ticks)) - call_random()));

        switch(difficulty)
        {
            case 1:
                return 0.1f * random.Next(8, 14);
            case 2:
                return 0.1f * random.Next(6, 11);
            case 3:
                return 0.1f * random.Next(4, 9);
            default:
                return 0.1f * random.Next(8, 14);
        }
    }

    private bool check_radius_between_nodes(Vector3 vector)
    {
        int node_set_locate_count=node_location.Count;

        for (int i = node_set_locate_count < 4 ? 0 : node_set_locate_count - 4; i < node_set_locate_count; i++) 
        {
            Vector3 call_vec = node_location[i].vector3;

            if (Vector3.Distance(vector, call_vec) < radius_MIN)
            {
                return true;
            }
            else if (Vector3.Distance(vector, call_vec) > radius_MAX)
            {
                return true;
            }
        }
        return false;
    }
}