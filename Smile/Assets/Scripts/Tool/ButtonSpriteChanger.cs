using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSpriteChanger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button button;
    public Sprite normalSprite;
    public Sprite pressedSprite;

    void Start()
    {
        // ��ư�� �Ϲ� ���� ��������Ʈ ����
        button.GetComponent<Image>().sprite = normalSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //�ִϸ��̼����� �̹��� ü���� ��ǥ[FIXME]
        // ��ư�� ������ Ȧ���� �� ��������Ʈ ����
        button.GetComponent<Image>().sprite = pressedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ��ư���� ���� ���� ��������Ʈ�� �Ϲ� ���·� ����
        button.GetComponent<Image>().sprite = normalSprite;
    }
}
