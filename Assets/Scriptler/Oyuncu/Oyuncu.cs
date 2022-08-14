using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oyuncu : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    OyunYoneticisi oyunYonetici;
    SeviyeYonetici seviyeYonetici;
    AdManager adManager;

    public GameObject balikParticle;
    public GameObject olumSesi;
    public GameObject toplamaSesi;

    public GameObject menuUI;
    public GameObject oyunIcýUI;
    public GameObject OlumUI;
    public GameObject bitisUI;

    public float kosmaHizi;
    public float ziplamaGucu;
    public float clampR;
    public float clampL;

    private bool sagdaMi = false;
    private bool donduMu = false;
    private bool zipladiMi = false;
    private bool yerdeMi;

    private float ilkDokunmaX;
    private float ilkDokunmaY;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        oyunYonetici = GameObject.FindWithTag("GameManager").GetComponent<OyunYoneticisi>();
        seviyeYonetici = GameObject.FindWithTag("GameManager").GetComponent<SeviyeYonetici>();
        adManager = GameObject.FindWithTag("AdManager").GetComponent<AdManager>();
    }
    private void Update()
    {
        KaydirmaIleDon();
        KaydirmaIleZipla();
        if (gameObject.transform.position.x == -0.75f)
        {
            sagdaMi = false;
        }
        if (gameObject.transform.position.x != -0.75)
        {
            sagdaMi = true;
        }
    }

    private void FixedUpdate()
    {
        if (oyunYonetici.AnlikOyunDurumu != OyunDurumu.Basla)
        {
            return;
        }
        Vector3 hareketPoz = new Vector3(Mathf.Clamp((transform.position + InputAl()).x, clampL, clampR), (transform.position + InputAl()).y, (transform.position + InputAl()).z);
        rb.MovePosition(hareketPoz);
    }

    private Vector3 InputAl()
    {
        Vector3 HareketVectoru = new Vector3(0, 0, kosmaHizi * Time.deltaTime);
        return HareketVectoru;
    }
    public void Ziplama()
    {
        if (yerdeMi)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(new Vector3(0, 1, 4) * ziplamaGucu);
        }
    }
    void KaydirmaIleDon()
    {
        float fark;
        if (Input.GetMouseButtonDown(0))
        {
            ilkDokunmaX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            float sonDokunmaX = Input.mousePosition.x;
            fark = sonDokunmaX - ilkDokunmaX;
            if (fark < -7 && sagdaMi && !donduMu && !zipladiMi)
            {
                Left();
                donduMu = true;
            }
            if (fark > 7 && !sagdaMi && !donduMu && !zipladiMi)
            {
                Right();
                donduMu = true;
            }
            ilkDokunmaX = sonDokunmaX;
        }
        if (Input.GetMouseButtonUp(0))
        {
            donduMu = false;
        }
    }
    void KaydirmaIleZipla()
    {
        float fark;
        if (Input.GetMouseButtonDown(0))
        {
            ilkDokunmaY = Input.mousePosition.y;
        }
        if (Input.GetMouseButton(0))
        {
            float sonDokunmaY = Input.mousePosition.y;
            fark = sonDokunmaY - ilkDokunmaY;
            if (fark > 5 && !donduMu && !zipladiMi)
            {
                Ziplama();
                zipladiMi = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            zipladiMi = false;
        }
    }
    public void Right()
    {
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(clampR, transform.position.y, transform.position.z), 5 * Time.time);
       // transform.position = new Vector3(0.75f, transform.position.y, transform.position.z);
        sagdaMi = true;
    }
    public void Left()
    {
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(clampL, transform.position.y, transform.position.z), 3 * Time.time);
        sagdaMi = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Collectible")
        {
            AltýnBalikYazisi.altinSayisi++;
            Destroy(other.gameObject);
            Instantiate(balikParticle,other.gameObject.transform.position,Quaternion.identity);

            var olusturulanToplamaSesi = Instantiate(toplamaSesi, gameObject.transform.position, Quaternion.identity);
            Destroy(olusturulanToplamaSesi, 0.5f);
        }
        if (other.gameObject.tag == "Finish")
        {
            animator.SetTrigger("Return");
            oyunIcýUI.SetActive(false);
            bitisUI.SetActive(true);
            oyunYonetici.OyunSonu();
            Invoke("SonrakiSeviyeyiBaslat", 1.5f);
            adManager.RequestInterstitial();
            adManager.interstitialAd.Show();
        }
        if (other.gameObject.tag == "Obstacle")
        {
            animator.SetTrigger("Return");
            oyunIcýUI.SetActive(false);
            OlumUI.SetActive(true);
            oyunYonetici.OyunSonu();
            Invoke("SuAnkiSeviyeyiBaslat", 1.5f);
            adManager.RequestInterstitial();
            adManager.interstitialAd.Show();

            var olusturulanOlumSesi = Instantiate(olumSesi, gameObject.transform.position, Quaternion.identity);
            Destroy(olusturulanOlumSesi, 0.5f);
        }
    }
    void SuAnkiSeviyeyiBaslat()
    {
        seviyeYonetici.LeveliBaslat();
        OlumUI.SetActive(false);
        menuUI.SetActive(true);
    }
    void SonrakiSeviyeyiBaslat()
    {
        oyunYonetici.SonrakiOyunuBaslat();
        bitisUI.SetActive(false);
        menuUI.SetActive(true);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            yerdeMi = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            yerdeMi = false;
        }
    }
}
