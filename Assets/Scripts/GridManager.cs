using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridManager : MonoBehaviour
{
    
    public GameObject buttonPrefab;
    public Transform gridParent;
    public UIManager uiManager;

    private int row = 10;
    private int col = 10;
    private string letters = "ABCDEFGHIJ";

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
                btn.onClick.AddListener(() => uiManager.Fire(coords));
            }
        }
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
