using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeviyeYonetici : MonoBehaviour
{
    public GameObject[] levels;
    private int suAnkiSeviye;

    private Oyuncu oyuncu;
    private Vector3 oyuncuIlkKonum;
    public Transform kameraKonum;
    private Vector3 kameraIlkKonum;

    public int _suAnkiSeviye { get => suAnkiSeviye; set => suAnkiSeviye = value; }
    void Start()
    {
        _suAnkiSeviye = PlayerPrefs.GetInt("Level", 0);
        oyuncu = GameObject.FindWithTag("Player").GetComponent<Oyuncu>();
        oyuncuIlkKonum = oyuncu.transform.position;
        kameraIlkKonum = kameraKonum.position;
        LeveliBaslat();
    }
    public void LeveliBaslat()
    {
        LeveliAktifEt(true);
        oyuncu.transform.position = oyuncuIlkKonum;
        kameraKonum.position = kameraIlkKonum;
    }
    public void StartNextLevel()
    {
        LeveliAktifEt(false);
        _suAnkiSeviye++;
        LeveliBaslat();
        PlayerPrefs.SetInt("Level", _suAnkiSeviye);
        PlayerPrefs.Save();
    }
    void LeveliAktifEt(bool isActive)
    {
        levels[_suAnkiSeviye % levels.Length].gameObject.SetActive(isActive);
    }
}
