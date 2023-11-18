using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public TextMeshProUGUI  textTumSayilar;
    public TextMeshProUGUI  textIkiyeBolunenler;
    public TextMeshProUGUI  textUceBolunenler;
    public TextMeshProUGUI  textDordeBolunenler;
    public TextMeshProUGUI  textBeseBolunenler;

    string TumSayilar = "Tüm Sayılar: ";
    string IkiyeBolunenler = "İkiye Bölünenler: ";
    string UceBolunenler = "Üçe Bölünenler: ";
    string DordeBolunenler = "Dörde Bölünenler: ";
    string BeseBolunenler = "Beşe Bölünenler: ";

    void Start()
    {
        BolenleriBul(5,27);
    }

    void Update()
    {

    }

    void BolenleriBul (int sayi1, int sayi2)
    {
        for(int i=sayi1; i<=sayi2; i++)
        {
            TumSayilar += i.ToString() + "-";
            
            if(i%2==0)
            {
                IkiyeBolunenler += i.ToString() + "-";
            }

            if(i%3==0)
            {
                UceBolunenler += i.ToString() + "-";
            }

            if(i%4==0)
            {
                DordeBolunenler += i.ToString() + "-";
            }

            if(i%5==0)
            {
                BeseBolunenler += i.ToString() + "-";
            }
        }
        textTumSayilar.text = TumSayilar.Substring(0, TumSayilar.Length - 1);
        textIkiyeBolunenler.text = IkiyeBolunenler.Substring(0, IkiyeBolunenler.Length - 1);
        textUceBolunenler.text = UceBolunenler.Substring(0, UceBolunenler.Length - 1);
        textDordeBolunenler.text = DordeBolunenler.Substring(0, DordeBolunenler.Length - 1);
        textBeseBolunenler.text = BeseBolunenler.Substring(0, BeseBolunenler.Length - 1);

        print(TumSayilar.Substring(0, TumSayilar.Length - 1));
        print(IkiyeBolunenler.Substring(0, IkiyeBolunenler.Length - 1));
        print(UceBolunenler.Substring(0, UceBolunenler.Length - 1));
        print(DordeBolunenler.Substring(0, DordeBolunenler.Length - 1));
        print(BeseBolunenler.Substring(0, BeseBolunenler.Length - 1));
    }
}
