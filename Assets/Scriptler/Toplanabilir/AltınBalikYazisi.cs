using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltýnBalikYazisi : MonoBehaviour
{
    Text altinTxt;

    public static int altinSayisi;

    private void Start()
    {
        altinTxt = GetComponent<Text>();
        PlayerPrefs.GetInt("altinTxt", altinSayisi);
    }
    void Update()
    {
        altinTxt.text = "GOLD FISH: " + altinSayisi.ToString();
        PlayerPrefs.SetInt("altinTxt", altinSayisi);
        PlayerPrefs.Save();
    }
}
