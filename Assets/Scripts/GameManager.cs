using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public NetworkClient network;
    public TMP_Text statusCheck;

    public void Play(string position)
    {
        network.SendMessage("FIRE;" + position);
        statusCheck.text = "Awaiting answer..";
    }

    public void ReceiveMessageServer(string msg)
    {
        if (msg.StartsWith("HIT"))
        {
            string target = msg.Split(';')[1];
            statusCheck.text = "Hit at: " + target;
        }
        else if (msg.StartsWith("MISS"))
        {
            string target = msg.Split(';')[1];
            statusCheck.text = "Miss at: " + target;
        }else if (msg == "YOUR_TURN")
        {
            statusCheck.text = "Its Your Turn";
        }else if (msg == "INCOMING")
        {
            string pos = msg.Split(';')[1];
            //update table with enemy attacked spot
        }
    }

}
