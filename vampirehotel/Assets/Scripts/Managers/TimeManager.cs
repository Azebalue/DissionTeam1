using UnityEngine;

public enum TimeState
{
    morning,
    day,
    evening,
    night
}

public class TimeManager
{
    public TimeState state; //�ð���

    public static int morningHour = 8;
    public static int dayHour = 9;
    public static int eveningHour = 15;
    public static int nightHour = 18;


    public int hour;
    public int minute;

    public float delta;
    public int timeWeight = 2;

    public void InitTime()
    {
        delta = 0;
        state = TimeState.morning;
        //Managers.Game.day= 1;
        //����� �ʱ�ȭ
        hour = morningHour;
        minute = 0;
    }

    public void SimulateTime()
    {
        //�ΰ��� �ð� �帧 ����
        delta += Time.deltaTime;
        Debug.Log($"{delta}�� ���");

    }


    public int GetTotalMinutes() => hour * 60 + minute;
}