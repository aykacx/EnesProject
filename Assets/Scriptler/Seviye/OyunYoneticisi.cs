using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum OyunDurumu
{
    Basla,
    Durdur,
    Bitti
}

public class OyunYoneticisi : MonoBehaviour
{
    SeviyeYonetici levelManager;
    private OyunDurumu _suAnkiOyunDurumu;

    public Animator OyuncuAnimator;

    public GameObject SonUI;
    public GameObject MenuUI;

    public Text SeviyeYazýsý;
    public const string LEVEL_STRING = "LEVEL ";
    public OyunDurumu AnlikOyunDurumu { get => _suAnkiOyunDurumu; set => _suAnkiOyunDurumu = value; }
    void Start()
    {
        levelManager = GetComponent<SeviyeYonetici>();
        AnlikOyunDurumu = OyunDurumu.Durdur;
        SeviyeYaziGüncelleme(levelManager._suAnkiSeviye);
    }

    public void SeviyeYaziGüncelleme(int Seviye)
    {
        SeviyeYazýsý.text = LEVEL_STRING + (Seviye + 1);
    }
    public void OyunuBaslat()
    {
        OyuncuAnimator.SetTrigger("Run");
        AnlikOyunDurumu = OyunDurumu.Basla;
        SeviyeYaziGüncelleme(levelManager._suAnkiSeviye);
        levelManager.LeveliBaslat();
    }
    public void SonrakiOyunuBaslat()
    {
        levelManager.StartNextLevel();
        SeviyeYaziGüncelleme(levelManager._suAnkiSeviye);
    }
    public void OyunSonu()
    {
        AnlikOyunDurumu = OyunDurumu.Bitti;
    }

    public void OyunuResetle()
    {
        SonUI.SetActive(false);
        MenuUI.SetActive(true);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
