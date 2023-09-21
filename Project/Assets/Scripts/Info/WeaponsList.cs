using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[System.Serializable]
[CreateAssetMenu(fileName = "WeaponsList", menuName = "ScriptableObjects/BalanceInfo", order = 1)]
public class WeaponsList : ScriptableObject
{
    [SerializeField]
    private List<GameObject> weapons;
}
