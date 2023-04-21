/*
*노드의 전체적인 구조를 담당 및 생성하는 스크립트
*
*구현 목표
*-노드의 일괄적인 생성 담당
*
*난이도 변경 시 수정절차
*-Initialize_node_setting() 수정 (난이도에 따른 노드의 개수 변경)
*-node_management의 시간값 수정
*-AnimationClip 노드 감소 타이밍 수정
*-node_delete의 MAX_TIME 수정 ㅇ
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
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

        //TODO: 이미지 적용 안될 때 예외처리 필요
        this.procedure = _procedure;
    }
}

public class node : MonoBehaviour
{
    private List<Node_data> node_location = new List<Node_data>();
    public GameObject nodes_prefab;

    [Header("아래의 항목에다가 노트의 이미지를 넣으면 됩니다")]
    public Sprite Node_image_A;
    public Sprite Node_image_B;
    public Sprite Node_image_C;
    public Sprite Node_image_D;

    [Header("아래의 항목에다가 해당 컷씬의 난이도를 지정합니다")]
    public static int difficulty = 3; //difault value 1

    [Header("노트간 간격을 조절합니다")]
    public float radius_MIN = 420f; //difault value 420f
    public float radius_MAX = 1000f; //difault value 1000f

    //노드를 리스트의 순서에 따라 하나를 차례로 배치하는 함수
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


    //노드를 생성하는 부분입니다. Coroutine으로 구현
    private IEnumerator D_Coroutine()
    {
        UnityEngine.Debug.Log("좌표 설정 완료 잠시 대기...");
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
        //노드의 초기 설정을 지정한다
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
        
        //노드끼리 일정 거리가 떨어지면 탈출
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