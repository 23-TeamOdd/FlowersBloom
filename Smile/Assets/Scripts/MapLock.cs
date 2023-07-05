using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLock : MonoBehaviour
{
    [SerializeField] List<GameObject> StageList;
    private Button btn;
    void Start()
    {
        /*    StageList[0].GetComponent<Stage>().LockMap();
              StageList[1].GetComponent<Stage>().LockMap();
              StageList[2].GetComponent<Stage>().LockMap();
        */

        // easy���� �����ְ�
        StageList[0].GetComponent<Stage>().UnlockMap();

        for (int i = 0; i < StageList.Count - 1; i++)
        {
            if (UniteData.GameClear[i]==1)
            {
                StageList[i+1].GetComponent<Stage>().UnlockMap();
            }
            else
            {
                StageList[i+1].GetComponent<Stage>().LockMap();
            }
        }
        //StageList[2].GetComponent<Stage>().LockMap(); // �������̶� ������ �ܰ�� ���� �ȿ�����
    }
}