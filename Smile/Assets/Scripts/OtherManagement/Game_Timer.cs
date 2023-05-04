/*
*��� ���� �� �ð��� �ٷ�� ��ũ��Ʈ
*
*���� ��ǥ
*-�÷��� ������ �ð����� ���
*-�ð��� ����, �Ͻ�����, �ٽ� ��� ���
*
*�� ��ũ��Ʈ�� �ʱ� ������ ������� �ۼ��Ͽ���
*���ǻ����� gkfldys1276@yandex.com���� ���� �ٶ��ϴ� (ī�嵵 ����).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game_Timer
{
    private float _startTime;
    private float _pausedTime;
    //private bool _isRunning = false;

    public Game_Timer(float backupTime)
    {
        _startTime = Time.time - backupTime;
    }

    public float GetTime()
    {
        return Time.time - _pausedTime - _startTime;
    }

    /*public void StartTimer()
    {
        _isRunning = true;
    }

    public void PauseTimer()
    {
        _pausedTime += Time.time - _startTime;
        _isRunning = false;
    }

    public void ResumeTimer()
    {
        _startTime = Time.time;
        _isRunning = true;
    }

    public void ResetTimer()
    {
        _startTime = Time.time;
        _pausedTime = 0f;
        _isRunning = false;
    }*/
}
