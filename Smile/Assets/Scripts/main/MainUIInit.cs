using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIInit : MonoBehaviour
{
    public GameObject UI_Character;

    public Sprite �ε鷹_����;
    public Sprite ƫ��_����;

    private void Update()
    {
        if (UniteData.Selected_Character == "Dandelion")
        {
            UI_Character.GetComponent<Image>().sprite = �ε鷹_����;
        }
        else if (UniteData.Selected_Character == "Tulip")
        {
            UI_Character.GetComponent<Image>().sprite = ƫ��_����;
        }
    }
}
