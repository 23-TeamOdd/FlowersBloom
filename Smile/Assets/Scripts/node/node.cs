/*
*����� ��ü���� ������ ��� �� �����ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*-����� �ϰ����� ���� ���
*-��尣 Line Renderer�� ���� ����
*-�� ���� ���
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
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Node_data
{
    private static float SIDE_V = 500f;
    private static float STR_V = 900f;

    public Vector2 vector2 = new Vector2();
    public Sprite procedure;
    public Sprite Ring_Color;
    public Sprite bright_Color;
    public int mode; //1: Ŭ�� / 2: �巡��(���� ����Ʈ) / 3: �巡�� (�� ����Ʈ)

    public Node_data(Vector2 _vector2, Sprite _procedure, Sprite _ring_Color, Sprite _bright_Color, int _mode)
    {
        this.vector2 = _vector2;

        //TODO: �̹��� ���� �ȵ� �� ����ó�� �ʿ�
        this.procedure = _procedure;
        this.Ring_Color = _ring_Color;
        this.bright_Color = _bright_Color;
        this.mode = _mode;
    }

    static public Vector2 resettingShadowNode(Vector2 forwardVector, int direction)
    {
        if (direction == 0) //12��
        {
            Vector2 v = new Vector2(forwardVector.x, forwardVector.y + STR_V);
            if (v.y>500f)
            {
                v.y = 500f;
            }
            return new Vector2(v.x, v.y);
        }
        else if(direction == 1) //1~2��
        {
            Vector2 v = new Vector2(forwardVector.x+ SIDE_V, forwardVector.y + SIDE_V);
            if (forwardVector.y > 500f)
            {
                v.y = 500f;
            }
            if(forwardVector.x>1300f)
            {
                v.x = 1300f;
            }
            return new Vector2(v.x, v.y);
        }
        else if (direction == 2) //3��
        {
            Vector2 v = new Vector2(forwardVector.x + STR_V, forwardVector.y);
            if (forwardVector.x > 1300f)
            {
                v.x = 1300f;
            }
            return new Vector2(v.x, v.y);
        }
        else if (direction == 3) //4~5��
        {
            Vector2 v = new Vector2(forwardVector.x + SIDE_V, forwardVector.y - SIDE_V);
            if (forwardVector.y < -500f)
            {
                v.y = -500f;
            }
            if (forwardVector.x > 1300f)
            {
                v.x = 1300f;
            }
            return new Vector2(v.x, v.y);
        }
        else if(direction==4) //6��
        {
            Vector2 v = new Vector2(forwardVector.x, forwardVector.y - STR_V);
            if (forwardVector.y < -500f)
            {
                v.y = -500f;
            }
            return new Vector2(v.x, v.y);
        }
        else if(direction==5) //7~8��
        {
            Vector2 v = new Vector2(forwardVector.x - SIDE_V, forwardVector.y - SIDE_V);
            if (forwardVector.y < -500f)
            {
                v.y = -500f;
            }
            if (forwardVector.x < -1300f)
            {
                v.x = -1300f;
            }
            return new Vector2(v.x, v.y);
        }
        else if(direction==6) //9��
        {
            Vector2 v = new Vector2(forwardVector.x - STR_V, forwardVector.y);
            if (forwardVector.x < -1300f)
            {
                v.x = -1300f;
            }
            return new Vector2(v.x, v.y);
        }
        else if(direction==7) //10~11��
        {
            Vector2 v = new Vector2(forwardVector.x - SIDE_V, forwardVector.y + SIDE_V);
            if (forwardVector.y > 500f)
            {
                v.y = 500f;
            }
            if (forwardVector.x < -1300f)
            {
                v.x = -1300f;
            }
            return new Vector2(v.x, v.y);
        }
        return forwardVector;
    }
}

public class node : MonoBehaviour
{
    private const int A = 0;
    private const int B = 1;
    private const int C = 2;
    private const int D = 3;

    private List<Node_data> node_location = new List<Node_data>();
    public GameObject nodes_prefab;
    public GameObject nodedrag_prefab;
    public LineRenderer line_renderer;
    public GameObject Highlight_Node;
    public GameObject[] backgrounds;

    [Header("�Ʒ��� �׸񿡴ٰ� ��Ʈ�� �̹����� ������ �˴ϴ�")]
    public Sprite Node_image_A;
    public Sprite Node_image_B;
    public Sprite Node_image_C;
    public Sprite Node_image_D;
    public Sprite ping_locate_shadow;

    [Header("�Ʒ��� �׸񿡴ٰ� ��Ʈ�� ���� ������ �˴ϴ�")]
    public Sprite[] Ring;

    [Header("�Ʒ��� �׸񿡴ٰ� �߱����� ������ �˴ϴ�")]
    public Sprite[] BR_Ring;
    public Sprite[] BR_Big_Ring;

    [Header("��Ʈ�� ������ �����մϴ�")]
    public float radius_MIN = 420f; //difault value 420f
    public float radius_MAX = 1000f; //difault value 1000f

    private IScenePass sceneLoader;
    public static bool UnPassed; //��Ʈ����Ʈ�� �ӽ� ����

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
            if (node_location[node_array].mode == 1)
            {
                SpriteRenderer sr = nodes_prefab.GetComponent<SpriteRenderer>();
                SpriteRenderer rn = nodes_prefab.transform.Find("ring").GetComponent<SpriteRenderer>();
                sr.sprite = node_location[node_array].procedure;
                rn.sprite = node_location[node_array].Ring_Color;

                Instantiate(nodes_prefab, node_location[node_array].vector2, Quaternion.identity);
            }
            else if (node_location[node_array].mode == 2)
            {
                SpriteRenderer sr = nodedrag_prefab.GetComponent<SpriteRenderer>();
                SpriteRenderer rn = nodedrag_prefab.transform.Find("ring").GetComponent<SpriteRenderer>();
                SpriteRenderer br = nodedrag_prefab.transform.Find("bright").GetComponent<SpriteRenderer>();
                sr.sprite = node_location[node_array].procedure;
                rn.sprite = node_location[node_array].Ring_Color;
                br.sprite = node_location[node_array].bright_Color;

                GameObject newNode = Instantiate(nodedrag_prefab, node_location[node_array].vector2, Quaternion.identity);
                newNode.name = "nodedrag";
                node_management_drag nmd=newNode.GetComponent<node_management_drag>();
                nmd.ping = node_location[node_array + 1].vector2;
            }
            else if (node_location[node_array].mode == 3)
            {
                SpriteRenderer sr = nodes_prefab.GetComponent<SpriteRenderer>();
                SpriteRenderer rn = nodes_prefab.transform.Find("ring").GetComponent<SpriteRenderer>();
                sr.sprite = node_location[node_array].procedure;
                rn.sprite = node_location[node_array].Ring_Color;

                //nodes_prefab ���� �����ִ� ring object�� ��Ȱ��ȭ �Ѵ�
                nodes_prefab.transform.Find("ring").gameObject.SetActive(false);

                GameObject sha=Instantiate(nodes_prefab, node_location[node_array].vector2, Quaternion.identity);
                sha.name = "shadow";


                nodes_prefab.transform.Find("ring").gameObject.SetActive(true);
            }
            else
            {
                Instantiate(nodes_prefab, node_location[node_array].vector2, Quaternion.identity);
            }
        }
    }

    private void Insert_Line(List<Vector2> v)
    {
        List<Vector2> vector = new List<Vector2>(v);

        //list ���� ���ҵ��� Reverse ��Ų��
        vector.Reverse();

        LineIndex = LineIndex + 1; //�� ���� ���µ� ���ϴϱ� ���������� �ٸ� �ҽ��ڵ忡 ���� ��� [HACK]

        //line renderer�� ��ǥ�� �ִ´�
        for (int i = 0; i < LineIndex; i++)
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

    private float[] easyTime_01 = { 0f, 2f, 1.8f, 1.8f, 2f };
    private float[] easyTime_23 = { 0f, 1.8f, 2f, 2f, 2f };
    private float[] normalTime_01 = { 0f, 1.6f, 1.8f, 1.8f, 1.6f, 1.6f, 1.6f, 1.8f };
    private float[] normalTime_23 = { 0f , 1.8f , 1.8f , 1.6f , 1.6f , 1.8f , 1.6f , 1.6f };
    private float[] hardTime = { 0, 0f, 2.5f, 1.5f, 1.5f, 1.5f, 0f, 2.3f, 1.3f, 1.5f, 1.3f };
    private int cas;

    //��带 �����ϴ� �κ��Դϴ�. Coroutine���� ����
    private IEnumerator D_Coroutine()
    {
        List<Vector2> vector = new List<Vector2>();

#if true
        for (int i=0; i<node_location.Count; i++)
        {
            vector.Add(node_location[i].vector2);
            if(node_location[i].mode == 1 )
            {
                yield return new WaitForSeconds(set_node_wait());
            }
            else if(node_location[i].mode == 2)
            {
                yield return new WaitForSeconds(1f);
            }
            node_placement(i);
            
            Insert_Line(vector);

            if(node_location[i].mode == 3)
            {
                yield return new WaitForSeconds(2f);
            }
        }
#else
        float[] non;
        if(cas-1<=0)
        {
            if (UniteData.Difficulty == 1)
            {
                non = easyTime_01;
            }
            else if (UniteData.Difficulty == 2)
            {
                non = normalTime_01;
            }
            else
            {
                non = hardTime;
            }
        }
        else
        {
            if(UniteData.Difficulty==1)
            {
                non = easyTime_23;
            }
            else if(UniteData.Difficulty==2)
            {
                non = normalTime_23;
            }
            else { non = hardTime; }
        }

        for(int i = 0; i < node_location.Count; i++)
        {
            vector.Add(node_location[i].vector2);
            if (node_location[i].mode == 1)
            {
                yield return new WaitForSeconds(non[i]);
                node_placement(i);
                Insert_Line(vector);
            }
            else if (node_location[i].mode == 2)
            {
                yield return new WaitForSeconds(non[i]);
                node_placement(i);
                Insert_Line(vector);
                i++;
                vector.Add(node_location[i].vector2);
                node_placement(i);
                Insert_Line(vector);
            }

            /*if (node_location[i].mode == 3)
            {
                yield return new WaitForSeconds(2f);
            }*/
        }
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        //Play ���� ScenePass�� ���� �񵿱������� �ε��Ѵ�
        sceneLoader = GetComponent<IScenePass>();
        sceneLoader.LoadSceneAsync("Play");

        //�ƾ� �ʱ�ȭ
        UniteData.Move_Progress = true;
        UnPassed = false;
        UniteData.Node_LifePoint = 2; //��� ���
        UniteData.Node_Click_Counter = 0; //��� Ŭ�� Ƚ��
        LineIndex = 0;
        line_renderer.material.color = Color.white;

        //��� ����
        // ó������ ��� ��� �ʱ�ȭ
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(false);
        }

        // ��� ����
        switch (UniteData.Difficulty)
        {
            case 1:
                backgrounds[0].SetActive(true);
                break;
            case 2:
                backgrounds[1].SetActive(true);
                break;
            case 3:
                //backgrounds[2].SetActive(true); // ���� hard ��尡 �ȳ��� ����
                break;
            default:
                backgrounds[0].SetActive(true);
                break;
        }

        //����� �ʱ� ������ �����Ѵ�
        Initialize_node_setting();

        StartCoroutine(D_Coroutine());
    }

    private void Update()
    {
        line_renderer.positionCount = LineIndex; //�� ���� ���µ� ���ϴϱ� ���������� �ٸ� �ҽ��ڵ忡 ���� ��� [HACK]

        //���� �ƾ��� Ŭ���� �߰ų� / ��ȸ����Ʈ�� �����ִ� ��Ȳ���� �������� ��
        if ((UniteData.Node_LifePoint >= 0 && UniteData.Node_Click_Counter == node_location.Count) || UnPassed)
        {
            //�ز��� ��� [HACK]
            for(int x=0; x<500000000; x++)
            {
                //���
            }
            //������ �ʱ�ȭ
            UniteData.Node_LifePoint = 2;
            UniteData.Node_Click_Counter = 0;
            UnPassed = false;
            UniteData.GameMode = "Play";
            //Ŭ���� �ִϸ��̼� ����
            sceneLoader.SceneLoadStart("Play");
        }


    }

    private void Initialize_node_setting()
    {
#if true
    cas = call_random() % 4;
#else
    cas = 1;
#endif
        switch (UniteData.Difficulty)
        {
            case 1: //easy
                if( cas == 0) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[C], BR_Ring[C], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                else if( cas == 1) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[C], BR_Ring[C], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                else if ( cas == 2) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[C], BR_Ring[C], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                else {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[C], BR_Ring[C], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                break;

            case 2: //normal
                if (cas == 0) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                }
                else if(cas == 1) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                }
                else if (cas == 2) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                }
                else {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                }
                break;

            case 3: //hard
                if (cas == 0) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                else if (cas == 1) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));

                    /*node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[0], 2));
                    node_location.Add(new Node_data(Node_data.resettingShadowNode(node_location[node_location.Count-1].vector2,5), ping_locate_shadow, Ring[1], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[2], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[3], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[0], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[1], 2));
                    node_location.Add(new Node_data(Node_data.resettingShadowNode(node_location[node_location.Count - 1].vector2,2), ping_locate_shadow, Ring[3], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[2], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[0], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[1], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[2], 1));*/
                }
                else if (cas == 2) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                else {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                break;

            default: //default
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[C], BR_Ring[C], 3));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));

                UniteData.Difficulty = 1;
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

        switch(UniteData.Difficulty)
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