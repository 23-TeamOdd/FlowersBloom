/*
*�� ī���� �Ӽ��� �ο��ϴ� Ŭ����
*
*���� ��ǥ
*-��ݿ���
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using UnityEngine;
using UnityEngine.UI;
public class Card_Status : MonoBehaviour
{
    public Sprite Unlock_image;
    public bool unlocked=false; //false�� ������

    //��� ���� Check
    [Header("�Ʒ��� ��������Դϴ�.")]
    public bool isEasyClear = false;
    public bool isNormalClear = false;
    public bool isHardClear = false;
}
