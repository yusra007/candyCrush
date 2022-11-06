using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class candy : MonoBehaviour
{
    float x, y;
    bool fall=true;
    public static candy firstChoiceCandy;
    public static candy secondChoiceCandy;
    public Vector3 targetLocation;
    public bool placeChange=false;
    public List<candy> candyX;
    public List<candy> candyY;
    public string color;
    GameObject chose;
    // Start is called before the first frame update
    void Start()
    {
       
       chose = GameObject.FindWithTag("select");
       
                
        
    }
    public void newTransform(float _x, float _y)
    {
        x = _x;
        y = _y;
    }

    // Update is called once per frame
    void Update()
    {
        if(fall)
        {
            if (transform.position.y-y<0.2f)
            {
                fall = false;
                transform.position=new Vector3(x,y,0);
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, 0), Time.deltaTime * 1f);
        }
        if(placeChange)
        {
            placeChangess();
        }
    }
    private void OnMouseDown()
    {
        
        chose.transform.position = transform.position;
        SugarControl();
        
    }
    void SugarControl()
    {
        if (firstChoiceCandy == null)
        {
            firstChoiceCandy = this;

        }

        else
        {
            secondChoiceCandy = this;
            if (firstChoiceCandy != secondChoiceCandy)
            {
                float differenceX = Mathf.Abs(firstChoiceCandy.x - secondChoiceCandy.x);
                float differenceY = Mathf.Abs(firstChoiceCandy.y - secondChoiceCandy.y);
                if (differenceX + differenceY == 1)
                {
                    Debug.Log("deðiþsinler");
                    
                    firstChoiceCandy.targetLocation=secondChoiceCandy.transform.position;
                    secondChoiceCandy.targetLocation=firstChoiceCandy.transform.position;

                    firstChoiceCandy.placeChange = true;
                    secondChoiceCandy.placeChange = true;

                    changingVariables();
                    firstChoiceCandy.checkXAxis();
                    firstChoiceCandy.checkYAxis();

                    secondChoiceCandy.checkXAxis();
                    secondChoiceCandy.checkYAxis();

                    StartCoroutine(firstChoiceCandy.destroyObjects());
                    StartCoroutine(secondChoiceCandy.destroyObjects());
                   
                    firstChoiceCandy=null;
                    



                }
                else
                {
                    firstChoiceCandy = secondChoiceCandy;
                    
                }
            }  
            
                secondChoiceCandy = null;
            
        }
    }
    void changingVariables()
    {
        GameManager.gameCandys[(int)firstChoiceCandy.x, (int)firstChoiceCandy.y] = secondChoiceCandy;
        GameManager.gameCandys[(int)secondChoiceCandy.x, (int)secondChoiceCandy.y] = firstChoiceCandy;

        float firstChoiceX = firstChoiceCandy.x;
        float firstChoiceY = firstChoiceCandy.y;

        firstChoiceCandy.x = secondChoiceCandy.x;
        firstChoiceCandy.y = secondChoiceCandy.y;

        secondChoiceCandy.x = firstChoiceX;
        secondChoiceCandy.y = firstChoiceY;

    }

    void placeChangess()
    {
        transform.position = Vector3.Lerp(transform.position, targetLocation, 0.3f);
    }
    void checkXAxis()
    {
        for (int i =(int) x; i < GameManager.gameCandys.GetLength(0); i++)
        {
            candy candyRight=GameManager.gameCandys[i,(int)y];
            if(color==candyRight.color)
            {
                candyX.Add(candyRight);
            }
            else
            {
                break;
            }
        }
        for (int i = (int)x - 1; i >= 0; i--)
        {
            candy candyRight = GameManager.gameCandys[i, (int)y];
            if (color == candyRight.color)
            {
                candyX.Add(candyRight);
            }
            else
            {
                break;
            }
        }
    }
    void checkYAxis()
    {
        for (int i = (int)y; i < GameManager.gameCandys.GetLength(0); i++)
        {
            candy candyUnder = GameManager.gameCandys[i, (int)x];
            if (color == candyUnder.color)
            {
                candyY.Add(candyUnder);
            }
            else
            {
                break;
            }
        }
        for (int i = (int)y - 1; i >= 0; i--)
        {
            candy candyUnder = GameManager.gameCandys[i, (int)x];
            if (color == candyUnder.color)
            {
                candyY.Add(candyUnder);
            }
            else
            {
                break;
            }
        }
    }

    IEnumerator destroyObjects()
    {
        yield return new WaitForSeconds(0.3f);
        if(candyX.Count>=2||candyY.Count>=2)
        {
            if(candyX.Count>=2)
            {
                foreach(var item in candyX)
                {
                    Destroy(item.gameObject);
                }
            }
            else
            {
                foreach (var item in candyY)
                {
                    Destroy(item.gameObject);
                }
            }
        }
    }
}
