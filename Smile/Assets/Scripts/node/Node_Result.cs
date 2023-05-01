using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node_Result : MonoBehaviour
{
    public GameObject[] LifePoint;

    //�ƾ� ��� �ý���
    public static bool Miss_Node_Click()
    {
        Color del_color = new Color(0, 0, 0);

        //LifePoint�� �̹��� ���� ���������� �ٲ۴�
        Node_Result node_result = GameObject.Find("Canvas").GetComponent<Node_Result>();
        foreach(GameObject life in node_result.LifePoint)
        {
            if(life.GetComponent<Image>().color!=del_color)
            {
                life.GetComponent<Image>().color = del_color;
                return false;
            }
        }

        return true;
    }
    
}
