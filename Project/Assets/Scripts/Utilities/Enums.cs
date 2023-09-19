using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : Singleton<Enums>
{
    public enum CharType
    {
        ERROR = -1,
        Default = 0,
        Player = 1,
        Neutral = 2,
        Enemy = 3,
    };
}
