using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAdminka : MonoBehaviour {
    public Text PlayerMoney;

    public void AddMoneyToPurse()
    {
        PurseClass.soft += 1000;
        PlayerMoney.text = PurseClass.soft.ToString();
    }
}
