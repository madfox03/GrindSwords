using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCreater : MonoBehaviour
{
    [Header("Подразумевается собирать мечи из частей ассета!")]
    public GameObject[] SwordBlades;
    public GameObject[] SwordCrossguards;
    public GameObject[] SwordHilts;
    public GameObject[] SwordPommels;

    void Start()
    {
        CreateSwordFromParts();
    }

    /// <summary>
    /// создание меяач из частей
    /// </summary>
    [ContextMenu("CreateSwordFromParts")]
    public void CreateSwordFromParts()
    {
        GameObject SwordBlade = Instantiate(GetGOFromMassive(SwordBlades), this.transform);
        SwordBlade.transform.position = Vector3.zero;
        GameObject SwordCrossguard = Instantiate(GetGOFromMassive(SwordCrossguards), SwordBlade.transform);
        GameObject SwordHilt = Instantiate(GetGOFromMassive(SwordHilts), SwordBlade.transform);
        GameObject SwordPommel = Instantiate(GetGOFromMassive(SwordPommels), SwordBlade.transform);
    }

    private GameObject GetGOFromMassive(GameObject[] _GObjects)
    {
        int rnd = Random.Range(0, _GObjects.Length);
        return _GObjects[rnd];
    }
}
