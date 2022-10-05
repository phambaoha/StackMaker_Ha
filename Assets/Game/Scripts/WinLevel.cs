using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel : MonoBehaviour
{
    [SerializeField] Transform ChestOpen, ChestClose, FireWork;

    GameObject player;
    private bool canMoveToWin;

    private void Awake()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        OnInit();  
    }

    void OnInit()
    {
        ChestClose.gameObject.SetActive(true);
        ChestOpen.gameObject.SetActive(false);
        FireWork.gameObject.SetActive(false);
    }
    private void Update()
    {
        MoveToWinPos();
    }
    public void EffectWin()
    {
        ChestClose.gameObject.SetActive(false);
        ChestOpen.gameObject.SetActive(true);
        FireWork.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canMoveToWin = true;
            LevelManagers.instance.level++;
        }
    }

    public void MoveToWinPos()
    {
        if (canMoveToWin)
        {
            if (player != null)
            {
                Transform child = player.transform.GetChild(0);

                child.position = Vector3.MoveTowards(child.position, ChestOpen.position + Vector3.back, 5f* Time.deltaTime);


                if (Vector3.Distance(child.position, ChestOpen.position + Vector3.back) < 0.1f)
                {
                    player.GetComponent<PlayerMove>().ChangeAnim("win");
                    EffectWin();

                    UIManagers.instance.nextLevel.gameObject.SetActive(true);
                }
            }
        }

    }
}
