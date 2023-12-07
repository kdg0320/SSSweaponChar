using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpeningText : MonoBehaviour
{
    public TextMeshProUGUI TextTMP;
    public GameObject UIimage;
    public GameObject startBack;
    public GameObject howToPlay;
    public GameObject background1;
    public GameObject background2;
    public GameObject background3;
    public GameObject background4;
    int index = 0;
    public string[] dialogue = { 
        "���1.",
        "���2.",
        "���3.",
        "���4.",
        "���5."
    };

    // Start is called before the first frame update
    void Start()
    {
        TextTMP.text = "GAME START";
        UIimage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TextTMP.text = dialogue[index];
            UIimage.SetActive(true);
            switch (index)
            {
                case 0:
                    startBack.SetActive(false);
                    break;
                case 1:
                    howToPlay.SetActive(false);
                    break;
                case 2:
                    background1.SetActive(false);
                    break;
                case 3:
                    background2.SetActive(false);
                    break;
                case 4:
                    background3.SetActive(false);
                    break;
                case 5:
                    background4.SetActive(false);
                    break;

            }
               
            if(index<4)
                index++;
        }
        
    }
}
