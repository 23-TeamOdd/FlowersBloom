using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class TutorialCommentManager : MonoBehaviour
{
    TSV tsv;
    DataTable dataTable;
    DataRowCollection dataRowCollection;
    UI_Comment uiComment;

    public GameObject nodeManager;

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

    public GameObject[] UI_system;
    public GameObject gameCharacters;

    public GameObject guideText;

    private Dictionary<string, GameObject> inGame_characters = new Dictionary<string, GameObject>();
    private Dictionary<string, Sprite> characterSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> itemSpriteList = new Dictionary<string, Sprite>();

    private Dictionary<int, List<Vector2>> buttonCoordinatePosition = new Dictionary<int, List<Vector2>>();

    public static bool cutsceneScriptPassed=false;
    private static bool isPlayCutEnd = false;
    private void Awake()
    {
        characterSprites.Add("�ε鷹", speakerSprites[0]);
        characterSprites.Add("ƫ��", speakerSprites[1]);
        characterSprites.Add("������", speakerSprites[2]);
        characterSprites.Add("�ε鷹_���", speakerSprites[3]);
        characterSprites.Add("�ε鷹_�̼�", speakerSprites[4]);
        characterSprites.Add("�ε鷹_�λ�", speakerSprites[5]);
        characterSprites.Add("�ε鷹_ȭ��", speakerSprites[6]);
        characterSprites.Add("ƫ��_���", speakerSprites[7]);
        characterSprites.Add("ƫ��_�̼�", speakerSprites[8]);
        characterSprites.Add("ƫ��_�λ�", speakerSprites[9]);
        characterSprites.Add("ƫ��_ȭ��", speakerSprites[10]);

        inGame_characters.Add("Dandelion", gameCharacters);

        buttonCoordinatePosition.Add(1, new List<Vector2> { new Vector2(0f, 190f) });
        buttonCoordinatePosition.Add(2, new List<Vector2> { new Vector2(0f, 270f), new Vector2(0f, 70f) });
        buttonCoordinatePosition.Add(3, new List<Vector2> { new Vector2(0f, 397f), new Vector2(0f, 189f), new Vector2(0f, -22f) });

        Debug.LogError("���� ���� !��! play/canvas/comment ������Ʈ�� ��Ȱ��ȭ ���ּ���!!!!!");
    }


    private const int COMMAND = 0;
    private const int NUMBERING = 1;
    private const int GOTO = 2;
    private const int BRANCH = 3;
    private const int IMAGEPOSITION = 4;
    private const int IMAGETYPE = 5;
    private const int CHARACTER = 6;
    private const int COMMENT = 7;

    private const int LEFT = 0;
    private const int CENTER = 1;
    private const int RIGHT = 2;

    private Vector2[] characterImagePosition = {
        new Vector2(-1200f, -90f),
        new Vector2(0f, -90f),
        new Vector2(1200f, -90f)
    };

    private Color whoIs = new Color(0f, 0f, 0f, 1f);
    private Color ohItsYou = new Color(1f, 1f, 1f, 1f);
    private Color noOneIsHere = new Color(0f, 0f, 0f, 0f);
    private Color iWillListen = new Color(0.2f, 0.2f, 0.2f, 1f);

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

    private string tsv_file = "���ǳ� ���丮 - tutorialForC#.tsv";

    private Vector3 goAwayToSky = new Vector3(0f, 2500f, 0f);
    private void do_ThrowOutObject()
    {
        Time.timeScale = 0f; //�̰� ������ ���̵� ó���� �ȵ�
        Application.targetFrameRate = 60;
        foreach (GameObject obj in UI_system)
        {
            obj.SetActive(false);
        }
        //Ȱ��ȭ�� ĳ���ʹ� �ϴ÷� ����������
        if (gameCharacters.activeSelf)
        {
            gameCharacters.transform.position = gameCharacters.transform.position + goAwayToSky;
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
                entry.Value.transform.position =
                    entry.Value.transform.position - goAwayToSky;
                //entry.Value.SetActive(true);
                break;
            }
        }
        foreach (GameObject obj in speakerPosition)
        {
            obj?.SetActive(true);
        }
        textGroup.SetActive(false);
    }

    private void OnEnable()
    {
        UniteData.GameMode = "Scripting";

        dataResetBeforeStartScripting();

        //�ƾ� ��Ʈ���� ���� ���� ���� ��ũ��Ʈ�̸�
        if(cutsceneScriptPassed==true && SceneManager.GetActiveScene().name== "InGame-RN-tutorial")
        {
            //��� Ż��
            tuto_nodeControl.Instance.turnOnNodeManager(nodeManager);
            do_BringInObject();
            return;
        }
        //�÷��� ��Ʈ���� ���� ���� ��ũ��Ʈ��
        if(isPlayCutEnd == true && SceneManager.GetActiveScene().name == "Tutorial" && StorySepCommand.Instance.getCommandBranch()==StorySepCommand.commandNum.CutEnd)
        {
            //��� Ż��
            do_BringInObject();

            foreach (var entry in inGame_characters)
            {
                if (entry.Key == UniteData.Selected_Character)
                {
                    //���յ� ĳ���ʹ� ���� �ø���
                    entry.Value.transform.position =
                        entry.Value.transform.position + goAwayToSky;
                    //entry.Value.SetActive(true);
                    break;
                }
            }

            return;
        }
        else if(StorySepCommand.Instance.getCommandBranch() == StorySepCommand.commandNum.CutEnd)
        {
            Debug.LogWarning("PASSHERE");
            isPlayCutEnd = true;
        }
        ////���丮 ���� �� ������Ʈ ��Ȱ��ȭ
        do_ThrowOutObject();

        ////���� ����
        uiComment = GetComponent<UI_Comment>();
        tsv = new TSV(tsv_file);


        ////���丮 ���� ���� ������ ���� �ؽ�Ʈ�� �̸� �ε��Ѵ�. [�Լ��� ����]
        switch(StorySepCommand.Instance.getCommandBranch())
        {
            case StorySepCommand.commandNum.First:
                dataRowCollection = startScripting("First");
                //StorySepCommand.Instance.setCommandBranch(StorySepCommand.commandNum.ClickFail);
                break;
            case StorySepCommand.commandNum.CutEnter:
                dataRowCollection = startScripting("CutEnter");
                //StorySepCommand.Instance.setCommandBranch(StorySepCommand.commandNum.CutEnd);
                break;
            case StorySepCommand.commandNum.CutEnter_E:
                dataRowCollection = startScripting("CutEnter_E");
                break;
            case StorySepCommand.commandNum.CutEnd:
                dataRowCollection = startScripting("CutEnd");
                //StorySepCommand.Instance.setCommandBranch(StorySepCommand.commandNum.KilledBy);
                break;
            case StorySepCommand.commandNum.KilledBy:
                dataRowCollection = startScripting("KilledBy");
                //StorySepCommand.Instance.setCommandBranch(StorySepCommand.commandNum.BossFail);
                break;
            case StorySepCommand.commandNum.BossFail:
                dataRowCollection = startScripting("BossFail");
                //StorySepCommand.Instance.setCommandBranch(StorySepCommand.commandNum.BossSuccess);
                break;
            case StorySepCommand.commandNum.BossSuccess:
                dataRowCollection = startScripting("BossSuccess");
                break;
            default://���� ó��
                //���丮 �� [�Լ��� ����]
                do_BringInObject();
                Debug.LogWarning("���丮 ���Ͽ� �ش� Command�� �����ϴ�.");
                break;
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
                foreach (var selectedResult in results)
                {
                    GameObject selectedObject = selectedResult.gameObject;
                    if (selectedObject.name == "BtnStop" || selectedObject.name == "No")
                    {
                        checkingBackBtn = true;
                        break;
                    }
                }


                if (!checkingBackBtn && !stopPanel.activeSelf)
                {
                    if (pageEnd == 1)  //�ϳ��� ��ũ��Ʈ�� ��� ����� �Ϸ����� ��.
                    {
                        clickTemp = false;
                    }

                    if (!clickTemp) //���� ��ũ��Ʈ�� �Ѿ��.
                    {
                        //�������� ��Ȱ��ȭ
                        guideText.SetActive(false);

                        try
                        {
                            if (dataRowCollection[int.Parse((string)dataRowCollection[page][GOTO])][BRANCH].ToString() != "select")//���� ������ �бⰡ �ƴ϶��!
                            {
                                //���� ��ũ��Ʈ�� �����Ѵ�.
                                initAboutTextValues();
                                page = int.Parse(dataRowCollection[page][GOTO].ToString());
                                clickTemp = true;
                            }
                        }
                        catch
                        {
                            //���� ��ũ��Ʈ�� �����Ѵ�.
                            initAboutTextValues();
                            page = int.Parse(dataRowCollection[page][GOTO].ToString());
                            Debug.Log("��ũ��Ʈ �������");
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
        frameTime++;
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


    private void do_Branching(int rowX)
    {
        handleSelectGroup(3, false); //�ʱ� �б� ���� ��ư ��Ȱ��ȭ
        string isSelect;
        try
        {
            isSelect = dataRowCollection[rowX][BRANCH].ToString();
        }
        catch
        {
            isSelect = dataRowCollection[rowX - 1][BRANCH].ToString();
        }
        //�б� ����
        if (isSelect == "select")
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
        if (row[IMAGEPOSITION].ToString() == "CenterAlong")
        {
            speakerPosition[LEFT].GetComponent<Image>().color = noOneIsHere;
            speakerPosition[RIGHT].GetComponent<Image>().color = noOneIsHere;

            imageWatchDirect(ref speakerPosition[CENTER], false);

            do_ImageSetting(row, CENTER);
        }
        else if (row[IMAGEPOSITION].ToString() == "RightTogether")
        {
            speakerPosition[CENTER].GetComponent<Image>().color = noOneIsHere;

            imageWatchDirect(ref speakerPosition[RIGHT], true);
            do_ImageSetting(row, RIGHT);

            if (speakerPosition[LEFT].GetComponent<Image>().sprite != null && speakerPosition[LEFT].GetComponent<Image>().color != whoIs)
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

    private void outputScript(int rowX, bool isPrintingImmadiately) //!!
    {
        if (rowX == dataRowCollection.Count)//�� �̻� ����� ���� ���ٸ�
        {
            ////���丮 ��. ������Ʈ ȸ��
            do_BringInObject();

            return;
        }
        DataRow row = dataRowCollection[rowX];

        if (dataRowCollection[rowX][CHARACTER].ToString() == "System")
        {
            do_SystemCommand(dataRowCollection[rowX][COMMENT].ToString());
            return;
        }

        do_Branching(int.Parse((string)dataRowCollection[page][GOTO]));

        if (dataRowCollection[rowX][CHARACTER].ToString() == "Narrator")
        {
            //ĳ���� �̹��� ���� ��Ȱ��ȭ
            foreach(GameObject obj in speakerPosition)
                obj.SetActive(false);

            guideText.SetActive(true);
            //�ؽ�Ʈ ��� ���
            Transform parantObj = guideText.transform;
            Text guidetext = parantObj.Find("GuideText").GetComponent<Text>();

            pageEnd = uiComment.printTextToUI(guidetext, row[COMMENT].ToString(), frameTime, FPP, true); //�ؽ�Ʈ ���
        }
        else
        {
            pageEnd = uiComment.printTextToUI(text, row[COMMENT].ToString(), frameTime, FPP, isPrintingImmadiately); //�ؽ�Ʈ ���
        }

        attach_CharacterImage(row);
    }

    private void do_SystemCommand(string commandText)
    {
        if (commandText.Contains("Back"))
        {
            if(frameTime < 2)
                enableClickMode = false;
            if (frameTime == 40)
            {
                //�� ü����
                Debug.Log("�� ü����");
                enableClickMode = true;
                clickTemp = false;
            }
        }

        if(commandText.Contains("EnterCutScene"))
        {
            ScenePass scenePass = new ScenePass();
            StorySepCommand.Instance.setCommandBranch(StorySepCommand.commandNum.CutEnter_E);
            scenePass.SceneLoad_Immediately("InGame-RN-tutorial");
        }

        if (commandText.Contains("SceneStart")) //Ʃ�丮��� ��ɾ�
        {
            tuto_nodeControl.Instance.turnOnNodeManager(nodeManager);
        }

        if(commandText.Contains("Transfusion"))
        {
            //ȸ��
        }

        if (commandText.Contains("Blind"))
        {
            enableClickMode = false;

            if (frameTime <= 30)
            {
                blind.SetActive(true);
                Image b_image = blind.GetComponent<Image>();
                b_image.color = new Vector4(0f, 0f, 0f, 1.0f * frameTime / 30);
            }
            else if (frameTime > 120)
            {
                blind.SetActive(false);
                initAboutTextValues();
                page = int.Parse(dataRowCollection[page][GOTO].ToString());
                enableClickMode = true;
            }
            else if (frameTime > 90)
            {
                Image b_image = blind.GetComponent<Image>();
                b_image.color = new Vector4(0f, 0f, 0f, -1.0f * (frameTime - 120) / 30);
            }
        }
    }

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

    private Sprite findImageForUsingStory(string comment)
    {
        foreach (KeyValuePair<string, Sprite> it in itemSpriteList)
        {
            if (comment.Contains(it.Key))
            { return it.Value; }
        }
        return null;
    }

    private void handleSelectGroup(int buttonAmount, bool isActive)
    {
        selectGroup.SetActive(isActive);
        for (int x = 0; x < buttonAmount; x++)
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
        page = int.Parse(dataRowCollection[page + buttonCode][GOTO].ToString());
    }

    private void imageWatchDirect(ref GameObject character, bool isWatchLeft = false)
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
