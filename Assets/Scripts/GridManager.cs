using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Text;

public class GridManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform gridParent;
    public UIManager uiManager;

    private int row = 10;
    private int col = 10;
    private string letters = "ABCDEFGHIJ";
    
    // Add dictionary to track cell states
    private Dictionary<string, string> estadoCelulas = new Dictionary<string, string>();

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject btnObj = Instantiate(buttonPrefab, gridParent);
                string coords = letters[j] + i.ToString();
                btnObj.name = coords;
                btnObj.GetComponentInChildren<TMP_Text>().text = coords;
                
                Button btn = btnObj.GetComponent<Button>();
                // Change to toggle ship placement instead of firing
                btn.onClick.AddListener(() => AlternarEstadoCelula(coords, btnObj));
                estadoCelulas[coords] = "~"; // Initialize as water
            }
        }
    }

    public void AlternarEstadoCelula(string coord, GameObject btn)
    {
        estadoCelulas[coord] = estadoCelulas[coord] == "~" ? "N" : "~";
        btn.GetComponent<Image>().color = estadoCelulas[coord] == "N" ? Color.gray : Color.white;
    }

    public string ObterTabuleiroFormatado()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 10; i++)
        {
            List<string> linha = new List<string>();
            for (int j = 0; j < 10; j++)
            {
                // Fix: Explicitly convert int to string
                string coord = letters[j] + i.ToString();
                linha.Add(estadoCelulas[coord]);
            }
            sb.AppendLine(string.Join(" ", linha));
        }
        return sb.ToString();
    }

    public void MarkCell(string coords, string result)
    {
        GameObject btn = GameObject.Find(coords);
        if(btn == null) return;
        
        Image img = btn.GetComponent<Image>();
        
        if(result == "HIT") img.color = Color.red;
        else if (result == "MISS") img.color = Color.blue;
        else if (result == "INCOMING") img.color = Color.yellow;
    }
}
