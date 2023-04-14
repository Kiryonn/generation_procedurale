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
    public string addresses;
    public string headers;
    public string bodies;
    public string footers;

}