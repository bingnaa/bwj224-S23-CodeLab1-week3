using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int mode = 1;

    private const string DIR_DATA = "/Data/";
    private const string FILE_MODE = "mode.txt";
    private string PATH_MODE;

    public const string PREF_MODE = "mode";
    
    public int currentLevel = 0;
    public int slimeB = 0;
    public int targetSl = 1;
    public int health = 100;

    public int Mode
    {
        get { return mode; }
        set
        {
            if (value < 3)
            {
                mode = value;
            }

            Directory.CreateDirectory(Application.dataPath + DIR_DATA);
            File.WriteAllText(PATH_MODE, "" + mode);
        }
    }
    
    void Awake()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PATH_MODE = Application.dataPath + DIR_DATA + FILE_MODE;

        Mode = PlayerPrefs.GetInt(PREF_MODE, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            File.Delete(PATH_MODE);
        }
        if (slimeB == targetSl)
        {
            currentLevel++;
            slimeB=0;
            targetSl *= 2;
            SceneManager.LoadScene(currentLevel);
        }
        
        if (health < 0)
        {
            SceneManager.LoadScene("Scenes/gameOver");
        }
    }
}
