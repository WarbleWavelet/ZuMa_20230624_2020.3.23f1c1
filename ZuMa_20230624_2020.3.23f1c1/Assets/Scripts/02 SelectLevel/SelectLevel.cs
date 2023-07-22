using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public Button BtnWorld1;
    public Button BtnWorld2;
    public Button BtnWorld3;
    public GameObject LevelSelectGo;
    public GameObject WorldSelectGo;
    public Button BtnBack;


    public int levelIndex = 0;




    private void Awake()
    {
        LevelSelectGo = transform.Find("LevelSelect").gameObject;
        WorldSelectGo = transform.Find("WorldSelect").gameObject;
        //LevelSelect

    }
}
