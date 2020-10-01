using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class PurseClass
{
    public static int soft;
    public static int hard;

    public static void SellSword(float price)
    {
        soft += (int) price;
    }
}
