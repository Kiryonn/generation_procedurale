using Data;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int currentMail = 0;

    private int jour;
    public Reputation reputation;
    public float perteReputation;
    public GameObject email;
    public MailInteraction mail;

    public Email[] listEmail;

    private void Start()
    {

    }

    /// <summary>
    /// l'utilisateur a dit que le mail est bon
    /// </summary>
    public void valideMail()
    {
        checkResult(true);
    }

    /// <summary>
    /// l'utilisateur a dit que le mail n'est pas bon
    /// </summary>
    public void refuseMail()
    {
        checkResult(false);
    }

    /// <summary>
    /// verifie si la reponse du joeur est bonne 
    /// </summary>
    /// <param name="playerResponse"> reponse du joeur </param>
    private void checkResult(bool playerResponse)
    {
        if (mailGood(currentMail) != playerResponse)
        {
            reputation.addReputation(perteReputation);
        }
        else
        {
            mail.gameObject.SetActive(true);
            email.SetActive(false);
        }

    }

    /// <summary>
    /// on veut savoir si notre mail est bon ou pas
    /// </summary>
    /// <param name="n">le numero du mail actuel</param>
    /// <returns></returns>
    private bool mailGood(int n)
    {

        return true;
    }

}
