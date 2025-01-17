using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDrag : MonoBehaviour
{
    private Vector2 mousePosDown;
    private float cameraXPosDown;
    private Camera Camera;
    private float[] backPos;
    public GameObject errorMessage_copy, back;
    public AudioSource audio;
    void Start()
    {
        backPos[0] = back.transform.position.x;
        backPos[1] = back.transform.position.y;
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosDown = Input.mousePosition;
            mousePosDown = Camera.ScreenToWorldPoint(mousePosDown);
            cameraXPosDown = Camera.transform.position.x;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.ScreenToWorldPoint(mousePos);

            Vector3 cameraPos = Camera.transform.position;
            float newXPos = cameraPos.x + mousePosDown.x - mousePos.x;
            newXPos = Mathf.Clamp(newXPos, 0f, 3200f);
            Camera.transform.position = new Vector3(newXPos, cameraPos.y, cameraPos.z);
            back.transform.position = new Vector3(newXPos + backPos[0], backPos[1], 0);
            errorMessage_copy.transform.position = new Vector3(newXPos, 0, 0);
        }

        //if (Input.GetMouseButtonUp(0))
        //{
        //    Vector3 cameraPos = Camera.transform.position;
        //    if (cameraPos.x < 1600)
        //    {
        //        Camera.transform.position = new Vector3(0, cameraPos.y, cameraPos.z);
        //        errorMessage_copy.transform.position = new Vector3(0, 0, 0);
        //    }
        //    else
        //    {
        //        Camera.transform.position = new Vector3(3200, cameraPos.y, cameraPos.z);
        //        errorMessage_copy.transform.position = new Vector3(3200, 0, 0);
        //    }
        //
        //    if (Camera.transform.position.x != cameraXPosDown) audio.Play();
        //}

    }
}
