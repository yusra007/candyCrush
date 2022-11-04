using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] candys;
    public int width, height;
    public static candy[,] gameCandys; 
    // Start is called before the first frame update
    void Start()
    {
        gameCandys=new candy[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int  y= 0; y < height;y++)
            {
                generateCandy(x,y);
            }
        }
        
    }
    public void generateCandy(int x, int y)
    {
        GameObject randomCandyObject = generateRandomCandy();
        GameObject newCandy=GameObject.Instantiate(randomCandyObject, new Vector3(x,y+10),Quaternion.identity);
       candy candy= newCandy.GetComponent<candy>();
        candy.newTransform(x, y);
        candy.color = randomCandyObject.name;
        gameCandys[x,y] = candy;
    }
    public GameObject generateRandomCandy() //rastgele þeker oluþtur
    {
        int random=Random.Range(0, candys.Length);
        return candys[random];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
