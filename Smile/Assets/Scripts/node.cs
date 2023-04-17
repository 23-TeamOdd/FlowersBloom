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

    [Header("�Ʒ��� �׸񿡴ٰ� ����� �̹����� ������ �˴ϴ�")]
    public Sprite Node_image_A;
    public Sprite Node_image_B;
    public Sprite Node_image_C;
    public Sprite Node_image_D;

    //��带 ����Ʈ�� ������ ���� �ϳ��� ���ʷ� ��ġ�ϴ� �Լ�
    void node_placement()
    {
        if (node_location.Count == 0)
        {
            return;
        }
        else
        {
            SpriteRenderer sr = nodes_prefab.GetComponent<SpriteRenderer>();
            sr.sprite = node_location[0].procedure;

            Instantiate(nodes_prefab, node_location[0].vector2, Quaternion.identity);

            node_location.RemoveAt(0);
        }
    }


    //��带 �����ϴ� �κ��Դϴ�. Coroutine���� ����
    private IEnumerator D_Coroutine()
    {
        UnityEngine.Debug.Log("��ǥ ���� �Ϸ� 2�� ���...");

        yield return new WaitForSeconds(2.0f);
        node_placement();

        yield return new WaitForSeconds(0.8f);
        node_placement();

        yield return new WaitForSeconds(1.2f);
        node_placement();

        yield return new WaitForSeconds(0.5f);
        node_placement();
    }

    // Start is called before the first frame update
    void Start()
    {
        //��尡 ��ġ�� ��ġ�� ������ �����Ѵ�
        node_location.Add(new Node_data(new Vector2(-980, -280), Node_image_A));
        node_location.Add(new Node_data(new Vector2(-540, 350), Node_image_B));
        node_location.Add(new Node_data(new Vector2(915, -290), Node_image_C));
        node_location.Add(new Node_data(new Vector2(440, 200), Node_image_D));

        StartCoroutine(D_Coroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
}