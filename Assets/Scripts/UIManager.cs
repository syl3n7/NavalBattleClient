using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text statusText;
    public GameManager gameManager;
    public Button submitButton; // Add reference to submit button
    
    private void Start()
    {
        // Connect submit button to GameManager method if exists
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(gameManager.SubmeterTabuleiro);
        }
    }
    
    public void Fire(string coords)
    {
        statusText.text = $" Disparaste em {coords}...";
        gameManager.Play(coords);  // Changed to use Play instead of ReceiveMessageServer
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
