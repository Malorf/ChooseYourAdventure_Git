using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Constructeur : MonoBehaviour
{
    public static string NameCharacter = "player1"; //Récupérer le nom du joueur
    private string lobbySceneName = "ProperGame";
    public static int ForceConstru;
    public static int EnduranceConstru;
    public static int IntelligenceConstru;
    public static int SagesseConstru;
    public static int DextéritéConstru;
    public static int CourageConstru;
    public void LogName(Text username)
    {
        NameCharacter = username.text;
        SceneManager.LoadScene(lobbySceneName);
        Debug.Log("valeur name:" + Constructeur.NameCharacter);
    }
    void Start()
    {
        ForceConstru = 0;
        EnduranceConstru = 0;
        IntelligenceConstru = 0;
        SagesseConstru = 0;
        DextéritéConstru = 0;
        CourageConstru = 0;
    }
    public void addForce(int ForceValue)
    {
         ForceConstru += ForceValue;  
    }
    public void addEndurance(int EnduranceValue)
    {
        EnduranceConstru += EnduranceValue;
    }
    public void addIntelligence(int IntelligenceValue)
    {
        IntelligenceConstru += IntelligenceValue;
    }
    public void addSagesse(int SagesseValue)
    {
        SagesseConstru += SagesseValue;
    }
    public void addDextérité(int DextéritéValue)
    {
        DextéritéConstru += DextéritéValue;
    }
    public void addCourage(int CourageValue)
    {
        CourageConstru += CourageValue;
    }
}
