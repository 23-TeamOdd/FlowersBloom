using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class InGameCommentManager : MonoBehaviour
{
    TSV tsv;
    DataTable dataTable;
    DataRowCollection dataRowCollection;
    UI_Comment uiComment;
    Animator animator;

    public GraphicRaycaster graphicRaycaster;
    public EventSystem eventSystem;

    public GameObject textGroup;
    public GameObject blind;
    public GameObject[] speakerPosition;
    public Text text;
    public Text speakerText;
    public GameObject stopPanel;

    public GameObject selectGroup;
    public GameObject[] buttonInSelectGroup;

    public Sprite[] speakerSprites;
    public Sprite[] branchSprites;

    public GameObject[] UI_system;
    public GameObject[] gameCharacters;

    private Dictionary<string, GameObject> inGame_characters = new Dictionary<string, GameObject>();
    private Dictionary<string, Sprite> characterSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> characterBlindSprites = new Dictionary<string, Sprite>();

    private Dictionary<int, List<Vector2>> buttonCoordinatePosition= new Dictionary<int, List<Vector2>>();
    private void Awake()
    {
        characterSprites.Add("�ε鷹", speakerSprites[0]);
        characterSprites.Add("ƫ��", speakerSprites[1]);
        characterSprites.Add("������", speakerSprites[2]);

        inGame_characters.Add("Dandelion", gameCharacters[0]);
        inGame_characters.Add("Tulip", gameCharacters[1]);
        inGame_characters.Add("ForgetMeNot", gameCharacters[2]);

        buttonCoordinatePosition.Add(1, new List<Vector2> { new Vector2(0f, 190f) });
        buttonCoordinatePosition.Add(2, new List<Vector2> { new Vector2(0f, 270f), new Vector2(0f, 70f) });
        buttonCoordinatePosition.Add(3, new List<Vector2> { new Vector2(0f, 397f), new Vector2(0f, 189f), new Vector2(0f, -22f) });

        Debug.LogError("���� ���� !��! play/canvas/comment ������Ʈ�� ��Ȱ��ȭ ���ּ���!!!!!");

        animator = blind.GetComponent<Animator>();
    }


    private const int COMMAND   = 0;
    private const int NUMBERING = 1;
    private const int GOTO      = 2;
    private const int BRANCH    = 3;
    private const int IMAGEPOSITION =4;
    private const int IMAGETYPE=5;
    private const int CHARACTER = 6;
    private const int COMMENT   = 7;

    private const int LEFT = 0;
    private const int CENTER = 1;
    private const int RIGHT = 2;

    private Vector2[] characterImagePosition = { 
        new Vector2(-1200f, -90f), 
        new Vector2(0f, -90f), 
        new Vector2(1200f, -90f) 
    };

    private Color whoIs= new Color(0f, 0f, 0f, 1f);
    private Color ohItsYou = new Color(1f, 1f, 1f, 1f);
    private Color noOneIsHere = new Color(0f, 0f, 0f, 0f);
    private Color iWillListen = new Color(0.2f,0.2f, 0.2f, 1f);

    private int page = 0;
    private int pageEnd = 0; //1:End

    private int frameTime = 0;
    private const int FPP = 3;
    private bool clickTemp = false;
    private bool printAll = false;
    private bool enableClickMode = true;
    private void dataResetBeforeStartScripting()
    {
        page = 0;
        pageEnd = 0;
        frameTime = 0;
        clickTemp = false;
        printAll = false;
    }

    private string[] tsv_file = {
        "���ǳ� ���丮easy1.tsv",
        "���ǳ� ���丮normal1.tsv",
        "���ǳ� ���丮hard1.tsv",
        "���ǳ� ���丮easy2.tsv",
        "���ǳ� ���丮normal2.tsv",
        "newType_W1E.tsv"
    };


    private Vector3 goAwayToSky = new Vector3(0f, 2500f, 0f);
    private void do_ThrowOutObject()
    {
        Time.timeScale = 0f; //�̰� ������ ���̵� ó���� �ȵ�
        Application.targetFrameRate = 60;
        foreach (GameObject obj in UI_system)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in gameCharacters)
        {
            //Ȱ��ȭ�� ĳ���ʹ� �ϴ÷� ����������
            if(obj.activeSelf)
            {
                obj.transform.position=obj.transform.position + goAwayToSky;
                break;
            }
        }
    }
    private void do_BringInObject()
    {
        Time.timeScale = 1f;
        UniteData.GameMode = "Play";
        foreach (GameObject obj in UI_system)
        {
            obj.SetActive(true);
        }
        foreach (var entry in inGame_characters)
        {
            if (entry.Key == UniteData.Selected_Character)
            {
                //���յ� ĳ���ʹ� ������ �Ⱦƹ�����
                entry.Value.transform.position=
                    entry.Value.transform.position - goAwayToSky;
                //entry.Value.SetActive(true);
                break;
            }
        }
        textGroup.SetActive(false);
    }

    private void OnEnable()
    {
        UniteData.GameMode = "Scripting";

        dataResetBeforeStartScripting();
        ////���丮 ���� �� ������Ʈ ��Ȱ��ȭ
        do_ThrowOutObject();

        ////���� ����
        uiComment = GetComponent<UI_Comment>();
        tsv = new TSV(tsv_file[UniteData.Difficulty - 1]);


        ////���丮 ���� ���� ������ ���� �ؽ�Ʈ�� �̸� �ε��Ѵ�. [�Լ��� ����]
        if (UniteData.StoryClear[UniteData.Difficulty - 1] == 0 && !UniteData.finishGame)
        {
            dataRowCollection = startScripting("Prestart");
        }
        else if (UniteData.StoryClear[UniteData.Difficulty - 1] == 1 && UniteData.finishGame)
        {
            dataRowCollection = startScripting("Finish");
        }
        else //���� ó��
        {
            //���丮 �� [�Լ��� ����]
            do_BringInObject();
            Debug.LogWarning("���丮 ���Ͽ� �ش� Command�� �����ϴ�.");
            return;
        }

        handleSelectGroup(3, false);
        clickTemp = true;
        outputScript(page, printAll);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && enableClickMode)
        {
            // ���콺 Ŭ�� ��ġ���� ����ĳ��Ʈ ����
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, results);

            // ����ĳ��Ʈ�� Ư�� UI ������Ʈ�� �浹�ϸ�
            if (results.Count > 0)
            {
                bool checkingBackBtn = false;
                foreach(var selectedResult in results)
                {
                    GameObject selectedObject= selectedResult.gameObject;
                    if (selectedObject.name == "BtnStop" || selectedObject.name == "No")
                    {
                        checkingBackBtn = true;
                        break;
                    }
                }


                if(!checkingBackBtn && !stopPanel.activeSelf) 
                {
                    if (pageEnd == 1)  //�ϳ��� ��ũ��Ʈ�� ��� ����� �Ϸ����� ��.
                    {
                        clickTemp = false;
                    }

                    if (!clickTemp) //���� ��ũ��Ʈ�� �Ѿ��.
                    {
                        if (dataRowCollection[page+1][BRANCH].ToString() != "select")//���� ������ �бⰡ �ƴ϶��!
                        {
                            //���� ��ũ��Ʈ�� �����Ѵ�.
                            initAboutTextValues();
                            page = int.Parse(dataRowCollection[page][GOTO].ToString());
                            clickTemp = true;
                        }
                    }
                    else //printing immediately
                    {
                        printAll = true;
                        clickTemp = false;
                    }
                }
            }
        }

        outputScript(page, printAll);
        frameTime ++;
    }

    private void initAboutTextValues()
    {
        frameTime = 0;
        printAll = false;
    }


    public DataRowCollection startScripting(string command)
    {
        dataTable = tsv.limitTSV(command);
        return dataTable.Rows;
    }


    private void do_Branching(int rowX, DataRow nextRow)
    {
        handleSelectGroup(3, false); //�ʱ� �б� ���� ��ư ��Ȱ��ȭ

        //�б� ����
        if (nextRow[BRANCH].ToString() == "select")
        {
            List<DataRow> selRow = new List<DataRow>();
            for (int x = 0; ; x++) //����Ʈ�� �б� �׸� �ֱ�
            {
                if (dataRowCollection[rowX + x][BRANCH].ToString() == "select")
                {
                    selRow.Add(dataRowCollection[rowX + x]);
                }
                else
                {
                    break;
                }
            }

            for (int x = 0; x < selRow.Count; x++) //��ư�� �б� �׸� �ְ� Ȱ��ȭ
            {
                handleSelectGroup(x + 1, true);
                buttonInSelectGroup[x].GetComponentInChildren<Text>().text = selRow[x][COMMENT].ToString();

                //���⼭ �б��� ������ ���� �б� ���� ��ư�� ��ġ�� �߾����� �����Ѵ�.
                buttonCoordinatePosition.TryGetValue(selRow.Count, out List<Vector2> posList);
                buttonInSelectGroup[x].GetComponent<RectTransform>().anchoredPosition = posList[x];
            }
        }
    }


    private void do_ImageSetting(DataRow row, int loc)
    {
        if (row[IMAGETYPE].ToString() == row[CHARACTER].ToString())
        {
            speakerPosition[loc].GetComponent<Image>().color = ohItsYou;
            speakerPosition[loc].GetComponent<Image>().sprite = speakersBannerImage(row[IMAGETYPE].ToString());
            if (speakerPosition[loc].GetComponent<Image>().sprite == null)
            {
                speakerPosition[loc].GetComponent<Image>().color = noOneIsHere;
                return;
            }
        }
        else if (row[CHARACTER].ToString() == "???")
        {
            speakerPosition[loc].GetComponent<Image>().color = whoIs;
            speakerPosition[loc].GetComponent<Image>().sprite = speakersBannerImage(row[IMAGETYPE].ToString());
        }
    }

    private void attach_CharacterImage(DataRow row)
    {
        if (row[IMAGEPOSITION].ToString()=="CenterAlong")
        {
            speakerPosition[LEFT].GetComponent<Image>().color = noOneIsHere;
            speakerPosition[RIGHT].GetComponent<Image>().color = noOneIsHere;

            imageWatchDirect(ref speakerPosition[CENTER], false);

            do_ImageSetting(row, CENTER);
        }
        else if(row[IMAGEPOSITION].ToString() == "RightTogether")
        {
            speakerPosition[CENTER].GetComponent<Image>().color = noOneIsHere;

            imageWatchDirect(ref speakerPosition[RIGHT], true);
            do_ImageSetting(row, RIGHT);

            if (speakerPosition[LEFT].GetComponent <Image>().sprite!=null && speakerPosition[LEFT].GetComponent<Image>().color!=whoIs)
            {
                speakerPosition[LEFT].GetComponent<Image>().color = iWillListen;
            }
        }
        else if (row[IMAGEPOSITION].ToString() == "LeftTogether")
        {
            speakerPosition[CENTER].GetComponent<Image>().color = noOneIsHere;

            imageWatchDirect(ref speakerPosition[LEFT], false);
            do_ImageSetting(row, LEFT);

            if (speakerPosition[RIGHT].GetComponent<Image>().sprite != null && speakerPosition[RIGHT].GetComponent<Image>().color != whoIs)
            {
                speakerPosition[RIGHT].GetComponent<Image>().color = iWillListen;
            }
        }
        else if (row[IMAGEPOSITION].ToString() == "RightAlong")
        {
            speakerPosition[CENTER].GetComponent<Image>().color = noOneIsHere;
            speakerPosition[LEFT].GetComponent<Image>().color = noOneIsHere;

            imageWatchDirect(ref speakerPosition[RIGHT], true);
            do_ImageSetting(row, RIGHT);
        }
        else if (row[IMAGEPOSITION].ToString() == "LeftAlong")
        {
            speakerPosition[CENTER].GetComponent<Image>().color = noOneIsHere;
            speakerPosition[RIGHT].GetComponent<Image>().color = noOneIsHere;

            imageWatchDirect(ref speakerPosition[LEFT], false);
            do_ImageSetting(row, LEFT);
        }
        else
        {
            return;
        }

        speakerText.text = row[CHARACTER].ToString();
    }
        
    private void outputScript(int rowX, bool isPrintingImmadiately)
    {
        if (rowX + 1 >= dataRowCollection.Count)//�� �̻� ����� ���� ���ٸ�
        {
            ////���丮 ��. ������Ʈ ȸ��
            do_BringInObject();

            UniteData.StoryClear[UniteData.Difficulty - 1] += 1;
            UniteData.SaveUserData();
            return;
        }
        DataRow row = dataRowCollection[rowX];

        if (dataRowCollection[rowX][CHARACTER].ToString() == "System") 
        {
            do_SystemCommand(dataRowCollection[rowX][COMMENT].ToString());
            return;
        }

        handleSelectGroup(3, false); //�ʱ� �б� ���� ��ư ��Ȱ��ȭ

        do_Branching(rowX + 1, dataRowCollection[rowX + 1]);

        pageEnd = uiComment.printTextToUI(text, row[COMMENT].ToString(), frameTime, FPP, isPrintingImmadiately); //�ؽ�Ʈ ���

        attach_CharacterImage(row);
    }

    private void do_SystemCommand(string commandText)
    {
        if (commandText== "Blind\r")
        {
            enableClickMode = false;

            if(frameTime<=30)
            {
                blind.SetActive(true);
                Image b_image=blind.GetComponent<Image>();
                b_image.color = new Vector4(0f, 0f, 0f, 1.0f * frameTime / 30);
            }
            else if (frameTime > 120)
            {
                blind.SetActive(false);
                initAboutTextValues();
                page = int.Parse(dataRowCollection[page][GOTO].ToString());
                enableClickMode = true;
            }
            else if(frameTime>90)
            {
                Image b_image = blind.GetComponent<Image>();
                b_image.color = new Vector4(0f, 0f, 0f, -1.0f * (frameTime-120) / 30);
            }
        }
    }

    //�б� ����

    private Sprite speakersBannerImage(string character)
    {
        if (characterSprites.TryGetValue(character, out Sprite sprite))
        {
            return sprite;
        }
        else
        {
            return null;
        }
    }

    private void handleSelectGroup(int buttonAmount, bool isActive)
    {
        selectGroup.SetActive(isActive);
        for(int x=0; x<buttonAmount; x++)
        {
            buttonInSelectGroup[x].SetActive(isActive);
        }
    }

    public void branchInStoryByUsersSelecting(int buttonCode)
    {
        //�ڵ忡 �´� Row�� ������ 
        //Goto�� �����ؼ� �ο�
        initAboutTextValues();
        clickTemp = true;
        handleSelectGroup(3, false);
        page = int.Parse(dataRowCollection[page+buttonCode][GOTO].ToString());
    }

    private void imageWatchDirect(ref GameObject character, bool isWatchLeft=false)
    {
        RectTransform rect = character.GetComponent<RectTransform>();
        if (isWatchLeft) 
        {
            rect.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            rect.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
    //
}
