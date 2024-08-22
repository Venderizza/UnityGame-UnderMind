using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager
{
    public static Action<int> onSelectWeapon;
    public static Action onDeathOfEnemy; // смерть врага по любой причине OnDestroy()


    public static Action<int> onKilledEnemy; // вызывается когда враг убит игроком, возвращает количество очков за убийство
    // используется для подсчета очков и отображения в интерфейсе
    public static Action<int> onChangeNumbOfBullets; // вызывается при изменении количества пуль <Bullets>
    public static Action<int> onChangePlayerHealth; // при изменении количества HP игрока <HP>
    public static Action<int> onSetArmor; // при установке брони <value>

    public static Action<string> onFirstPickable; // <tag>

    public static Action onPlayerDeath;

    public static Action onFirstEnemy0;
    public static Action onFirstEnemy1;
    public static Action onFirstEnemy2;


    public static Action<bool> onAllowShoot;
    public static Action<string, int> onPick; // вызывается при подборе Pickable-объекта, <tag> <value>
}
