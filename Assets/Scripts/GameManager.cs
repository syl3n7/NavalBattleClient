using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public NetworkClient network;
    public TMP_Text statusCheck;
    public GridManager grid;
    public UIManager ui;

    public void Play(string position)
    {
        network.SendMessage("FIRE;" + position);
        statusCheck.text = "Awaiting answer..";
    }

    // Add method to submit board to server
    public void SubmeterTabuleiro()
    {
        string tabuleiroStr = grid.ObterTabuleiroFormatado();
        network.SendMessage("BOARD;" + tabuleiroStr);
        ui.UpdateStatus("Tabuleiro submetido. Aguardando outro jogador...");
    }

    public void ReceiveMessageServer(string msg)
    {
        if (msg.StartsWith("HIT"))
        {
            string target = msg.Split(';')[1];
            statusCheck.text = "Hit at: " + target;
            // Update grid visually
            grid.MarkCell(target, "HIT");
        }
        else if (msg.StartsWith("MISS"))
        {
            string target = msg.Split(';')[1];
            statusCheck.text = "Miss at: " + target;
            // Update grid visually
            grid.MarkCell(target, "MISS");
        }
        else if (msg == "YOUR_TURN")
        {
            statusCheck.text = "Its Your Turn";
        }
        else if (msg.StartsWith("INCOMING"))
        {
            string[] parts = msg.Split(';');
            if (parts.Length > 1)
            {
                string pos = parts[1];
                grid.MarkCell(pos, "INCOMING");
                statusCheck.text = "Enemy attacked at " + pos;
            }
        }
        else if (msg == "TABULEIRO_RECEBIDO")
        {
            ui.UpdateStatus("Tabuleiro recebido pelo servidor. Aguardando outro jogador...");
        }
        else if (msg == "START")
        {
            ui.UpdateStatus("O jogo começou!");
        }
    }
}
