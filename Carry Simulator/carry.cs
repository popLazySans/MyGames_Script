using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class carry : MonoBehaviour
{
    public float BasePoint;
    static public float Weight =1;
    private float point;
    private float highpoint;
    private int carryCheck;
    public Text Wtext;
    public Text Ptext;
    public Text Htext;
    public GameObject hand;
    public Vector3 normalPosition;
    public Text inputWeight;
    
    // Start is called before the first frame update
    void Start()
    {
        normalPosition = hand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        point = BasePoint / Weight*9.8f;
        if (BasePoint > highpoint) 
            {
            highpoint = BasePoint;
            } 
        if (Input.GetKeyDown(KeyCode.Q)||Input.GetKeyDown(KeyCode.E))
        {
            BasePoint += 1;
            carryCheck += 1;
            //Debug.Log("Base = " + BasePoint);
            //Debug.Log(carryCheck);
            StartCoroutine(AFK());
        }
      if(carryCheck == 0)
        {
            BasePoint = 0;
            hand.transform.position = Vector3.Lerp(hand.transform.position,normalPosition,10f*Time.deltaTime);
            //Debug.Log("Base = " + BasePoint);
        }
        else
        {
            hand.transform.position = new Vector3(normalPosition.x, normalPosition.y + point, normalPosition.z);
        }
        Wtext.text = "Weight : " + Weight.ToString() ;
        Ptext.text = "Point : " + BasePoint.ToString();
        Htext.text = "High Score " + highpoint.ToString();
    }

    IEnumerator AFK()
    {
        yield return new WaitForSeconds(0.3f);
        
        carryCheck -= 1;
       // Debug.Log(carryCheck);
    }

}
