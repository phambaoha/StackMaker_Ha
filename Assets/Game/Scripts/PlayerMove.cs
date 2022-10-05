using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirecton
{
    None,
    Foward,
    Back,
    Right,
    Left,
}


public class PlayerMove : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField]
    private float distanceDrag;

    private Vector3 startPos, endPos;
    public float speed = 1f;


    [Header("animation")]
    [SerializeField]
    Animator playerAnim;

    
    private string curentAnim;

    public LayerMask layerMask;

    private bool canMove;

 

    public Transform WinPos;
 
   

    List<Transform> listBrick = new List<Transform>();
    void Start()
    {

        OnInit();
    }
    void OnInit()
    {

        startPos = endPos;
        transform.GetComponent<BoxCollider>().enabled = false;
        canMove = true;
       
     
        
      
    }

    Vector3 temp;
    // Update is called once per frame
    void Update()
    {
       
        if (canMove)
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startPos = Input.mousePosition;
                }
                endPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
               
                temp = CheckFinishDirectBrick(GetPlayerDirection());

                ChangeAnim("jump");
                StartCoroutine(IResetJump());
            }
            canMove = false;


        }

        if (Vector3.Distance(transform.position, temp) <= 0.1f)
        {
            canMove = true;

        }
          
        transform.position = Vector3.MoveTowards(transform.position, temp, speed * Time.deltaTime);



    }

    IEnumerator IResetJump()
    {
        yield return new WaitForSeconds(0.5f);
        ChangeAnim("idle");

    }

    // tra ve kieu enum, ham di chuyen
    MoveDirecton GetPlayerDirection()
    {
        MoveDirecton dir = MoveDirecton.None;

        if (Mathf.Abs(endPos.x - startPos.x) > Mathf.Abs(endPos.y - startPos.y))
        {
            if (Mathf.Abs(endPos.x - startPos.x) > distanceDrag)
            {

                if (endPos.x > startPos.x)
                {
                    dir = MoveDirecton.Right;

                }
                else
                {
                    dir = MoveDirecton.Left;
                }

                transform.GetComponent<BoxCollider>().enabled = true;

            }
        }
        else
           if (Mathf.Abs(endPos.y - startPos.y) > distanceDrag)
        {

            if (endPos.y > startPos.y)
            {
                dir = MoveDirecton.Foward;
            }
            else
            {
                dir = MoveDirecton.Back;

            }
            transform.GetComponent<BoxCollider>().enabled = true;
        }

       

        return dir;
    }


    
    public Vector3 CheckFinishDirectBrick(MoveDirecton moveDirecton)
    {
        Vector3 finishBlock = Vector3.zero;
        Vector3 moveDir = Vector3.zero;
        Vector3 faceDir = Vector3.forward;
        switch (moveDirecton)
        {
            case MoveDirecton.None:
                break;
            case MoveDirecton.Foward:
                {
                    moveDir = Vector3.forward;
                    faceDir = new Vector3(0, 0, 0);
                }
                break;
            case MoveDirecton.Back:
                {
                    moveDir = Vector3.back;
                    faceDir = new Vector3(0, 180, 0);
                }

                break;
            case MoveDirecton.Right:
                {
                    moveDir = Vector3.right;

                    faceDir = new Vector3(0, 90, 0);
                }

                break;
            case MoveDirecton.Left:
                {
                    moveDir = Vector3.left;
                    faceDir = new Vector3(0, -90, 0);
                }

                break;
            default:
                break;
        }


        
        for (int i = 0; i < 30; i++)
        {
        
            RaycastHit hit;
            if (Physics.Raycast(transform.position + moveDir * i + Vector3.up * 5, Vector3.down, out hit,Mathf.Infinity , layerMask))
            {


                if (hit.transform != null)
                {
                    finishBlock = hit.collider.transform.position + Vector3.up * 2;

                }                      
                
            }
            else
            {
                break;
            }

        }
        transform.rotation = Quaternion.Euler(faceDir);
        return finishBlock;

    }

   

    
   public int brickQuantity = 0;
    int brickQuanityText = 0;


    // them brick

    Vector3 offsetPos = Vector3.zero;
    void AddBrick(Collider other)
    {

        brickQuantity++;
        brickQuanityText = brickQuantity;

        listBrick.Add(other.transform);
        transform.GetChild(0).localPosition += new Vector3(0, 0.2f, 0);
        other.transform.SetParent(this.transform);


        // add vao list
        other.transform.localPosition = new Vector3(0, brickQuantity * 0.2f, 0);
        other.GetComponent<BoxCollider>().enabled = false;
    }

    // xoa het brick
    void ClearAllBrick()
    {
        for (int i = transform.childCount - 1; i > 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
    }
    // xoa tung brick
    void ReMoveBrick()
    {

        brickQuantity--;
        brickQuanityText = brickQuantity;

        Transform lastChild = listBrick[listBrick.Count - 1];
        listBrick.Remove(lastChild);
        Destroy(lastChild.gameObject);
        transform.GetChild(0).localPosition -= new Vector3(0, 0.2f, 0);
        
        

    }
   public void ChangeAnim(string animName)
    {
        if (curentAnim != animName)
        {
            playerAnim.ResetTrigger(animName);

            curentAnim = animName;

            playerAnim.SetTrigger(curentAnim);
        }
    }


   
   
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
          
            AddBrick(other);
            UIManagers.instance.SetCoinText(brickQuanityText);
           
        }


        if (other.gameObject.CompareTag("removebrick"))
        {
            ReMoveBrick();
        }

        if (other.gameObject.CompareTag("youwin"))
        {

            ClearAllBrick();
            
        }
    }
 


}




