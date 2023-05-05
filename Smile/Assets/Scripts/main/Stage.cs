using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stage : MonoBehaviour
{
    [SerializeField] GameObject Button;
    [SerializeField] GameObject Title;
    [SerializeField] GameObject Ping;

    public void LockMap()
    {
        Button.GetComponent<Button>().interactable = false;
        /* Ÿ��Ʋ, �� ��Ȱ��ȭ ó�� ����*/
    }
    
    public void UnlockMap()
    {
        Button.GetComponent<Button>().interactable = true;
    }
    


}
