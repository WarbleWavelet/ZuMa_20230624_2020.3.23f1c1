using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;
    public GameObject overPanel;
    public GameObject SuccPanel;

    private void Awake()
    {
        Instance = this;





    }
    public void ShowSuccPanel()
    {
        SuccPanel.Show();
    }
}
