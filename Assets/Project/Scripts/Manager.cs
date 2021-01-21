using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Manager : MonoBehaviour 
{
    [Space(15)]
    [Header("UI:")]
    public Text lvlValText;
    public GameObject UIPanelBrokenSword;

    public Text PlayerMoney;
    public Text SharpingChance;
    public Text SwordPrice;
    public Text SwordPriceNow;

    private GameObject SwordNow;
    SwordClass swordPref = null;
    int sharp_lvl;

    // Use this for initialization
    void Start () {
        CreateSword("Sword_From_Vol_1");
    }

    /// <summary>
    /// Инстансинг готового меча из папки Prefabs
    /// </summary>
    /// <param name="swordPrefName"></param>
    public void CreateSword(string swordPrefName)
    {
        //swordPref = Parse.instance.swordClass.GetByName(swordPrefName);
        GameObject sword = Instantiate(Resources.Load<GameObject>("Prefabs/" + swordPrefName), this.transform);
        swordPref.swordprefab = sword;
        sword.transform.position = Vector3.zero;
        Material glowMat = sword.GetComponent<SwordMove>().RendererGlowMaterial.sharedMaterial;
        glowMat.SetFloat("_Intencity", 0);
        sharp_lvl = 0;
        SharpeningClass ss = swordPref.sharpening.Find(x => x.sharpening_lvl == sharp_lvl);
        SharpeningClass ssPlus = swordPref.sharpening.Find(x => x.sharpening_lvl == sharp_lvl+1);
        glowMat.SetColor("_Color", SharpeningClass.hexToColor(ss.sharpening_color));
        SwordNow = sword;
        SharpingChance.text = ss.sharpening_chance.ToString();
        SwordPrice.text = "+" + (ssPlus.sharpening_coast - ss.sharpening_coast).ToString();
        SwordPriceNow.text = ss.sharpening_coast.ToString();
    }

    /*
    //--- старая версия
    public void CreateSword_OLD(string swordPrefName)
    {
        //swordPref = Parse.instance.swordClass.GetByName(swordPrefName);
        GameObject sword = Instantiate(Resources.Load<GameObject>("Prefabs/" + swordPrefName), this.transform);
        swordPref.swordprefab = sword;
        sword.transform.position = Vector3.zero;
        Material glowMat = sword.GetComponent<SwordMove>().RendererGlowMaterial.sharedMaterial;
        glowMat.SetFloat("_Intencity", 0);
        sharp_lvl = 0;
        SharpeningClass ss = swordPref.sharpening.Find(x => x.sharpening_lvl == sharp_lvl);
        SharpeningClass ssPlus = swordPref.sharpening.Find(x => x.sharpening_lvl == sharp_lvl + 1);
        glowMat.SetColor("_Color", SharpeningClass.hexToColor(ss.sharpening_color));
        SwordNow = sword;
        SharpingChance.text = ss.sharpening_chance.ToString();
        SwordPrice.text = "+" + (ssPlus.sharpening_coast - ss.sharpening_coast).ToString();
        SwordPriceNow.text = ss.sharpening_coast.ToString();
    }
    */

    public void CreateNextSword()
    {
        Destroy(SwordNow);
        CreateSword("Sword_From_Vol_1");
        sharp_lvl = 0;
        lvlValText.text = "0";
    }

    /// <summary>
    /// заточить
    /// </summary>
    public void Sharpen()
    {
        Material glowMat = SwordNow.GetComponent<SwordMove>().RendererGlowMaterial.sharedMaterial;

        SharpeningClass ss = swordPref.sharpening.Find(x => x.sharpening_lvl == sharp_lvl);

        float chance = ss.sharpening_chance;
        float coast = ss.sharpening_coast;

        //---алгоритм определения шанса заточки
        if (Random.Range(0, 100) > 100 - chance)
        {
            glowMat.SetFloat("_Intencity", 1);
            glowMat.SetColor("_Color", SharpeningClass.hexToColor(ss.sharpening_color));
            ss = swordPref.sharpening.Find(x => x.sharpening_lvl == sharp_lvl + 1);
            lvlValText.text = ss.sharpening_lvl.ToString();
            SharpingChance.text = ss.sharpening_chance.ToString();
            SwordPrice.text = "+" + (ss.sharpening_coast - coast).ToString();
            SwordPriceNow.text = ss.sharpening_coast.ToString();

            sharp_lvl += 1;
        }
        else
        {
            UIPanelBrokenSword.SetActive(true);
        }
        //---берем плату
        //PurseClass.soft -= (int)coast;
        //PlayerMoney.text = PurseClass.soft.ToString();
        
        //else
        //{
        //    PlayerMoney.DOColor(Color.red, 0.5f).OnComplete(()=>{
        //        PlayerMoney.DOColor(Color.white, 0.5f);
        //    });
        //}
    }

    /// <summary>
    /// повторить когда меч сломали
    /// </summary>
    public void RepeatSharpen()
    {
        Destroy(SwordNow);
        CreateSword("Sword_From_Vol_1");
        sharp_lvl = 0;
        lvlValText.text = "0";
    }

    /// <summary>
    /// продажа заточеного меча
    /// </summary>
    public void SellSharpenedSword()
    {
        SharpeningClass ss = swordPref.sharpening.Find(x => x.sharpening_lvl == sharp_lvl);
        PurseClass.SellSword(ss.sharpening_coast);
        RepeatSharpen(); //---можнозпменить на выбор меча из коллекции
        PlayerMoney.text = PurseClass.soft.ToString();
    }
}
