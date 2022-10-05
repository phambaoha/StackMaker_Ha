using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagers : MonoBehaviour
{

    public List<GameObject> listMapPrefabs = new List<GameObject>();

    public static LevelManagers instance;

    public GameObject Player;
     GameObject curentLevel;

    public int textLevel;

   public int level = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;

        
    }

    private void Start()
    {
        if (level <1)
        {
            LoadLevel(1);
        }    
        else
        {
           
            LoadLevel(level);
        }    

    }


    public void LoadLevel(int index)
    {
        UIManagers.instance.nextLevel.gameObject.SetActive(false);
        if (curentLevel != null)
        {
            Destroy(curentLevel);
        }

        curentLevel = Instantiate(listMapPrefabs[index - 1], Vector3.zero, Quaternion.identity);

        PlayerPrefs.SetInt("level", index);

        OnInit();

    }

    private void OnInit()
    {

        Player.transform.position = new Vector3(0, 0, 0);

        Player.transform.GetChild(0).localPosition = new Vector3(0, 0.4f, 0);

        Player.GetComponent<PlayerMove>().brickQuantity = 0;

        UIManagers.instance.SetTextLevel(level);

        

    }

    public void OnFinishLevel()
    {

    }

}
