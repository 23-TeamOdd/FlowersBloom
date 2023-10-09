using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto_GuideManager : MonoBehaviour
{
    [Header("���̵� �ؽ�Ʈ �ڽ� UI")] public GameObject guideTextBox;
    [Header("���̵� �ؽ�Ʈ UI")] public Text guideText;
    // Start is called before the first frame update
    void Start()
    {
        guideTextBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ���� ���� ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !UniteData.ReStart)
        {
            // ù��° ���� ���� ��
            if (UniteData.mon_num == 1)
            {
                // �ؽ�Ʈ �ڽ� ǥ��
                guideTextBox.SetActive(true);
                guideText.text = "��Ʈ�ڽ��� ǥ�õǴ� ���� �������\r\n��, ���� ��Ʈ�� ��ġ�ϼ���.";
                
                // �̵� ���߱�
                UniteData.Move_Progress = false;
            }
        }
    }
}
