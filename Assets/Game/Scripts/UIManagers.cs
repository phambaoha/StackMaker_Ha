using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagers : MonoBehaviour
{
    public static UIManagers instance;

    [SerializeField] Text textCoin;
    [SerializeField] Text textLevel;
    public Button nextLevel;

    private void Awake()
    {
        if(instance  == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        OnInit();
    }
    
    private void OnInit()
    {
        textCoin.text = " ";
        textLevel.text = "LEVEL 1";
        nextLevel.gameObject.SetActive(false);
    }
    public void SetTextLevel(int index)
    {
        textLevel.text = "Level  " + index.ToString();

    }    
    public void SetCoinText(int coin)
    {
        textCoin.text = coin.ToString();
    }

    public void SetLevel(int level)
    {
        textLevel.text = "LEVEL " + level.ToString();
    }

    // hien thi btn level
   
    public void NextLevel()
    {
        LevelManagers.instance.LoadLevel(LevelManagers.instance.level);

       
    }    
}
