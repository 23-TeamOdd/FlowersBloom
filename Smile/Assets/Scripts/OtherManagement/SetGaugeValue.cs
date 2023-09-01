/*
*��ũ�� ���� ���������� �����ϴ� ��ũ��Ʈ
*
*���� ��ǥ
*-Ư�� �������� ���� �����ϵ��� ����
*-�ʱ� ���� �� ������ �� �ݿ�
*-������ �� ���� �� ���������� ���� �Է�
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetGaugeValue : MonoBehaviour
{
    public Slider slider;
    public enum GaugeType
    {
        BGM,
        Effect
    }

    public GaugeType type;

    private void Start()
    {
        approachStaticValue();
        slider.value = approachStaticValue();
    }

    private void Update()
    {
        gaugeChangeValue(slider.value);
    }

    private void gaugeChangeValue(float gval)
    {
        switch (type)
        {
            case GaugeType.BGM:
                UniteData.BGM = gval;
                break;
            case GaugeType.Effect:
                UniteData.Effect = gval;
                break;
            default:
                break;
        }
    }

    private float approachStaticValue()
    {
        switch(type)
        {
            case GaugeType.BGM:
                return UniteData.BGM;
            case GaugeType.Effect:
                return UniteData.Effect;
            default:
                return 1f;
        }
    }
}
