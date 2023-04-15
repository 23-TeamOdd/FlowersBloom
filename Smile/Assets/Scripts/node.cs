/*
*����� ��ü���� ������ ��� �� �����ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*-����� �ϰ����� ���� ���
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Node_data
{
    public Vector2 vector2 = new Vector2();
    public string procedure = "NULL";

    public Node_data(Vector2 _vector2, string _procedure)
    {
        this.vector2 = _vector2;
        this.procedure = _procedure;
    }
}

public class node : MonoBehaviour
{
    private List<Node_data> node_location = new List<Node_data>();
    public GameObject nodes_prefab;

    //��带 ����Ʈ�� ������ ���� �ϳ��� ���ʷ� ��ġ�ϴ� �Լ�
    void node_placement()
    {
        if(node_location.Count == 0)
        {
            return;
        }
        else
        {
            Instantiate(nodes_prefab, node_location[0].vector2, Quaternion.identity);

            node_location.RemoveAt(0);
        }
    }


    //��带 �����ϴ� �κ��Դϴ�. Coroutine���� ����
    private IEnumerator D_Coroutine()
    {
        UnityEngine.Debug.Log("��ǥ ���� �Ϸ� 5�� ���...");

        yield return new WaitForSeconds(5.0f);
        node_placement();

        yield return new WaitForSeconds(2.0f);
        node_placement();

        yield return new WaitForSeconds(1.0f); //TODO ��峢�� ������ ����µ� �̰� �ذ�
        node_placement();

        yield return new WaitForSeconds(2.0f);
        node_placement();
    }

    // Start is called before the first frame update
    void Start()
    {
        //��尡 ��ġ�� ��ġ�� ������ �����Ѵ�
        node_location.Add(new Node_data(new Vector2(0, 0), "1"));
        node_location.Add(new Node_data(new Vector2(1, 2), "2"));
        node_location.Add(new Node_data(new Vector2(2, 4), "3"));
        node_location.Add(new Node_data(new Vector2(4, 3), "4"));

        StartCoroutine(D_Coroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
