using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class InGameCommentManager : MonoBehaviour
{
    TSV tsv;
    DataTable dataTable;
    DataRowCollection dataRowCollection;
    UI_Comment uiComment;
    Animator animator;

    public GameObject textGroup;
    public GameObject blind;
    public GameObject speaker;
    public Text text;
    public Text speakerText;

    public GameObject selectGroup;
    public GameObject[] buttonInSelectGroup;

    public Sprite[] speakerSprites;
    public Sprite[] branchSprites;

    public GameObject[] UI_system;

    private Dictionary<string, Sprite> characterSprites = new Dictionary<string, Sprite>();
    private void Awake()
    {
        characterSprites.Add("�ε鷹", speakerSprites[0]);
        characterSprites.Add("ƫ��", speakerSprites[1]);
        characterSprites.Add("������", speakerSprites[2]);


        animator = blind.GetComponent<Animator>();
        UniteData.GameMode = "Scripting";
    }

    private const int COMMAND   = 0;
    private const int NUMBERING = 1;
    private const int GOTO      = 2;
    private const int BRANCH    = 3;
    private const int CHARACTER = 4;
    private const int COMMENT   = 5;

    private int page = 0;
    private int pageEnd = 0; //1:End

    private int frameTime = 0;
    private int FPP = 8;
    private bool clickTemp = false;
    private bool printAll = false;


    private string[] tsv_file = { 
        "Assets\\Resources\\comment\\NewW01_Easy_Story.tsv",
        "Assets\\Resources\\comment\\NewW01_Easy_Story.tsv",
        "Assets\\Resources\\comment\\NewW01_Easy_Story.tsv",
        "Assets\\Resources\\comment\\NewW01_Easy_Story.tsv",
        "Assets\\Resources\\comment\\NewW01_Easy_Story.tsv",
        "Assets\\Resources\\comment\\NewW01_Easy_Story.tsv"
    };

    private void OnEnable()
    {
        //���丮 ����
        Time.timeScale = 0f; //�̰� ������ ���̵� ó���� �ȵ�
        foreach (GameObject obj in UI_system)
        {
            obj.SetActive(false);
        }

        uiComment = GetComponent<UI_Comment>();
        tsv = new TSV(tsv_file[UniteData.Difficulty - 1]);

        if (UniteData.StoryClear[UniteData.Difficulty - 1] == 0 && !UniteData.finishGame)
        {
            dataRowCollection = startScripting("Prestart");
        }
        else if (UniteData.StoryClear[UniteData.Difficulty - 1] == 1 && UniteData.finishGame)
        {
            dataRowCollection = startScripting("Finish");
        }
        else
        {
            //���丮 ��
            Time.timeScale = 1f;
            UniteData.GameMode = "Play";
            foreach (GameObject obj in UI_system)
            {
                obj.SetActive(true);
            }

            textGroup.SetActive(false);
            return;
        }

        handleSelectGroup(3, false);
        clickTemp = true;
        outputScript(page, printAll);
    }

    private void Update()
    {
        //1�� Ŭ��(���� �ؽ�Ʈ ���) 
        //2�� Ŭ��(��� �ؽ�Ʈ ���)
        if (Input.GetMouseButtonDown(0))
        {
            if(pageEnd==1)
            {
                clickTemp = false;
            }

            if(!clickTemp) //printing next text
            {
                if(dataRowCollection[page][BRANCH].ToString()!="preselect")
                {
                    initAboutTextValues();
                    page = int.Parse(dataRowCollection[page][GOTO].ToString());
                    clickTemp = true;
                }
            }
            else //printing immediately
            {
                printAll = true;
                clickTemp =false;
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


    private void outputScript(int rowX, bool isPrintingImmadiately)
    {
        if (rowX + 1 >= dataRowCollection.Count)
        {
            textGroup.SetActive(false);
            //���丮 ��
            Time.timeScale = 1f;
            foreach (GameObject obj in UI_system)
            {
                obj.SetActive(true);
            }

            UniteData.StoryClear[UniteData.Difficulty - 1] += 1;
            UniteData.SaveUserData();
            return;
        }
        DataRow row = dataRowCollection[rowX];

        if (dataRowCollection[rowX][CHARACTER].ToString() == "System") 
        {
            if(dataRowCollection[rowX][COMMENT].ToString()=="Blind")
            {
                //����ε� �۾� ����
                Debug.Log("Blind");
            }
            return;
        }

        handleSelectGroup(3, false);
        //�б� ����
        if (row[BRANCH].ToString()=="preselect")
        {
            List<DataRow> selRow=new List<DataRow>();
            for(int x=1; ; x++) 
            {
                if (dataRowCollection[rowX+x][BRANCH].ToString()=="select")
                {
                    selRow.Add(dataRowCollection[rowX + x]);
                }
                else
                {
                    break;
                }
            }

            for(int x=0; x<selRow.Count; x++)
            {
                handleSelectGroup(x + 1, true);
                buttonInSelectGroup[x].GetComponentInChildren<Text>().text = selRow[x][COMMENT].ToString();
            }
        }

        pageEnd = uiComment.printTextToUI(text, row[COMMENT].ToString(), frameTime, FPP, isPrintingImmadiately);

        Image speakerImage = speaker.GetComponent<Image>();
        speakerImage.sprite = speakersBannerImage(row[CHARACTER].ToString());
        if (speakerImage.sprite == null)
            speaker.SetActive(false);
        else
            speaker.SetActive(true);
        if (row[CHARACTER].ToString()=="ƫ��")
            speaker.transform.localPosition = new Vector2(1200f, -90f);
        else
            speaker.transform.localPosition = new Vector2(-1200f, -90f);

        speakerText.text = row[CHARACTER].ToString();
    }

    //�б� ����

    private Sprite speakersBannerImage(string character)
    {
        if (characterSprites.TryGetValue(character, out Sprite sprite))
        {
            Debug.Log(character);
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
}
