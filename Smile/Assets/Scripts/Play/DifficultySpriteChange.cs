using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���̵��� ���� ���� �̹��� ���� ��ũ��Ʈ

public class DifficultySpriteChange : MonoBehaviour
{
    [Header("���� �̹��� �߰�")] public Sprite[] Monster_Image;
    public GameObject monster;

    // Start is called before the first frame update
    void Start()
    {
        MonsterSpriteChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MonsterSpriteChange()
    {
        SpriteRenderer spriteRenderer = monster.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Monster_Image[UniteData.Difficulty - 1];
    }

    private void BackgroundChange()
    {
        
    }
}
