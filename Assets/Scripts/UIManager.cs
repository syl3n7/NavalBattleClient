using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text statusText;
    public GameManager gameManager;
    public void Fire(string coords)
    {
        statusText.text = $" Disparaste em {coords}...";
        gameManager.ReceiveMessageServer(coords);
    }
    public void UpdateStatus(string msg)
    {
        statusText.text = msg;
    }
    public void ShowResult(string coords, string result)
    {
        string resume = result == "HIT" ? " Acertaste!" :
            result == "MISS" ? " Falhaste!" :
            result == "INCOMING" ? " Foste atingido!" : "";
        statusText.text = result + $" ({coords})";
    }
}
