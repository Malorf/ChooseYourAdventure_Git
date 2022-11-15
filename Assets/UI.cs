using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI PointsText;
    public static int PointsValue = 0;
    public TextMeshProUGUI ForceText;
    public static int ForceTotal = 10 + Constructeur.ForceConstru;
    public TextMeshProUGUI EnduranceText;
    public static int EnduranceTotal = 10 + Constructeur.EnduranceConstru;
    public TextMeshProUGUI IntelligenceText;
    public static int IntelligenceTotal = 10 + Constructeur.IntelligenceConstru;
    public TextMeshProUGUI SagesseText;
    public static int SagesseTotal = 10 + Constructeur.SagesseConstru;
    public TextMeshProUGUI DextéritéText;
    public static int DexteriteTotal = 10 + Constructeur.DextéritéConstru;
    public TextMeshProUGUI CourageText;
    public static int CourageTotal = 10 + Constructeur.CourageConstru;
    bool activation = false;
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }
    void Update()
    {
        PointsText.text = " Points disponibles : " + PointsValue;
        ForceText.text = "Force :             " + ForceTotal;
        EnduranceText.text = "Endurance :    " + EnduranceTotal;
        IntelligenceText.text = "Intelligence : " + IntelligenceTotal;
        SagesseText.text = "Sagesse :        " + SagesseTotal;
        DextéritéText.text = "Dextérité :      " + DexteriteTotal;
        CourageText.text = "Courage :       " + CourageTotal;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activation = !activation;
            GetComponent<Canvas>().enabled = activation;
        }
    }
    public void addForce(int ForceValue)
    {
        if (PointsValue >=1)
        {
            PointsValue -= 1;
            ForceTotal += ForceValue;
        }
    }
    public void addEndurance(int EnduranceValue)
    {
        if (PointsValue >= 1)
        {
            PointsValue -= 1;
            EnduranceTotal += EnduranceValue;
        }
    }
    public void addIntelligence(int IntelligenceValue)
    {
        if (PointsValue >= 1)
        {
            PointsValue -= 1;
            IntelligenceTotal += IntelligenceValue;
        }
    }
    public void addSagesse(int SagesseValue)
    {
        if (PointsValue >= 1)
        {
            PointsValue -= 1;
            SagesseTotal += SagesseValue;
        }
    }
    public void addDextérité(int DextéritéValue)
    {
        if (PointsValue >= 1)
        {
            PointsValue -= 1;
            DexteriteTotal += DextéritéValue;
        }
    }
    public void addCourage(int CourageValue)
    {
        if (PointsValue >= 1)
        {
            PointsValue -= 1;
            CourageTotal += CourageValue;
        }
    }
}