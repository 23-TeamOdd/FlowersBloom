using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnType : MonoBehaviour
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defualtScale;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    public CanvasGroup teamGroup;
    private bool isOptionPanel;
    private bool isTeamPanel;
   
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.GS:
                Debug.Log("���ӽ���");
                break;
            case BTNType.Char:
                Debug.Log("ĳ����");
                break;
            case BTNType.Option:
                isOptionPanel = true;
                CanvasGroupOn(optionGroup);
                MainCanvasGroupOff(mainGroup);
                break;
            case BTNType.Sound:
                Debug.Log("����");
                break;
            case BTNType.Teammate:
                isOptionPanel = false;
                isTeamPanel = true;
                CanvasGroupOn(teamGroup);
                CanvasGroupOff(optionGroup);
                break;
            case BTNType.Back:
                SaveSettingData();
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case BTNType.TeamBack:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(teamGroup);
                break;
            case BTNType.Reset:
                UniteData.ResetUserData();
                Slider BGM = GameObject.Find("MusicSlider").GetComponent<Slider>();
                UniteData.BGM = 0.7f;
                BGM.value = 0.7f;
                Slider Effect = GameObject.Find("SoundEffectSlider").GetComponent<Slider>();
                UniteData.Effect = 0.7f;
                Effect.value = 0.7f;
                UniteData.SaveUserData();
                break;
        }
    }
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void MainCanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0.5f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    //�������� ���� �� ��� �����͸� �����մϴ�
    private void SaveSettingData()
    {
        Slider BGM=GameObject.Find("MusicSlider").GetComponent<Slider>();
        UniteData.BGM = BGM.value;

        Slider Effect=GameObject.Find("SoundEffectSlider").GetComponent<Slider>();
        UniteData.Effect = Effect.value;

        UnityEngine.Debug.Log(UniteData.BGM + " / " + UniteData.Effect);
        UniteData.SaveUserData();
    }

    void Update()
    {
        if (isOptionPanel)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);

                if (results.Count == 0)
                {
                    SaveSettingData();
                    CanvasGroupOn(mainGroup);
                    CanvasGroupOff(optionGroup);
                }
            }
        }

        if (isTeamPanel)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);

                if (results.Count == 0)
                {
                    SaveSettingData();
                    CanvasGroupOn(optionGroup);
                    CanvasGroupOff(teamGroup);
                }
            }
        }
    }
}
