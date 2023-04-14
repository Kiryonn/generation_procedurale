using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DocumentTechniqueAttaque")]
public class DocumentStrutuDonne : ScriptableObject
{
    public List<DocumentInformation> typeDeTechnique;
}

[System.Serializable]
public class DocumentInformation
{
    public string Titre;
    public string text;
}
