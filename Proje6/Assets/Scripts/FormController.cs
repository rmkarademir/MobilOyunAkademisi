using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Analytics;
using DG.Tweening;

public class FormController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel,panel2;
    public Button button;
    public TextMeshProUGUI textFrom;
    public TMP_InputField iName;
    public TMP_InputField iLastName;
    public Toggle tFamale;
    public Toggle tMale;
    public TMP_InputField iAge;
    public TMP_InputField iCountry;
    public Slider _sliderBar;
    string name;
    string lastName;
    string gender;
    int age;
    string country;
    int successBar;
    string _textForm;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FormGenaretor()
    {
        name = iName.text;
        lastName = iLastName.text;
        if(tFamale.isOn) gender = "Kadın";
        if(tMale.isOn) gender = "Erkek";
        age = Int32.Parse(iAge.text);
        country = iCountry.text;
        successBar = (int)_sliderBar.value;
        Debug.Log(name);
        Debug.Log(lastName);
        Debug.Log(gender);
        Debug.Log(age);
        Debug.Log(country);
        Debug.Log(successBar);
        panel.SetActive(true);
        panel.GetComponent<Image>().DOFillAmount(1, 1);
        panel2.GetComponent<Image>().DOFillAmount(1, 1).OnComplete(()=>StartCoroutine(TypeWrite()));
        _textForm = String.Format("Merhaba ben {0} {1} {2} yılında {3} ilinde doğdum. Erzurum Mobil Oyun Akdemisinde yaptığım çalışmalar ile %{4} oranında başarılı olacağımı düşünüyorum.",
                                      name, lastName, DateTime.Now.Year-age, country, successBar);
        
    }
    IEnumerator TypeWrite()
    {
        foreach(char i in _textForm)
        {
            textFrom.text += i.ToString();
            if(i.ToString() == ".") yield return new WaitForSeconds(0.5f);
            else yield return new WaitForSeconds(0.1f);
        }    
    }
}
