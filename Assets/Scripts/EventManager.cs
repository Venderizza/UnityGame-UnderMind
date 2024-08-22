using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager
{
    public static Action<int> onSelectWeapon;
    public static Action onDeathOfEnemy; // ������ ����� �� ����� ������� OnDestroy()


    public static Action<int> onKilledEnemy; // ���������� ����� ���� ���� �������, ���������� ���������� ����� �� ��������
    // ������������ ��� �������� ����� � ����������� � ����������
    public static Action<int> onChangeNumbOfBullets; // ���������� ��� ��������� ���������� ���� <Bullets>
    public static Action<int> onChangePlayerHealth; // ��� ��������� ���������� HP ������ <HP>
    public static Action<int> onSetArmor; // ��� ��������� ����� <value>

    public static Action<string> onFirstPickable; // <tag>

    public static Action onPlayerDeath;

    public static Action onFirstEnemy0;
    public static Action onFirstEnemy1;
    public static Action onFirstEnemy2;


    public static Action<bool> onAllowShoot;
    public static Action<string, int> onPick; // ���������� ��� ������� Pickable-�������, <tag> <value>
}
