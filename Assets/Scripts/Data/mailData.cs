using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "email")]
public class mailData : ScriptableObject
{
    List<mail> listeMail;
}





[System.Serializable]
public class mail 
{
    public Email email;
    public string addresses
    public string Titre;
    public string text;

}