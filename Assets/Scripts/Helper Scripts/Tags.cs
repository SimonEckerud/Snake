using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    public static string Wall = "Wall";
    public static string Fruit = "Fruit";
    public static string Bomb = "Bomb";
    public static string Tail = "Tail";

}

public class Metrics
{
    public static float NODE = 0.2f;
}

public enum PlayerDirection
{
    Left = 0,
    Up = 1,
    Right = 2,
    Down = 3,
    Count = 4
}