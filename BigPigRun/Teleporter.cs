using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Teleporter : MonoBehaviour
{
    public Transform gameObjectTransform;
    public TMP_Text Message;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Teleport(Transform toTransform)
    {
        if (gameObjectTransform.position != toTransform.position)
        {
            gameObjectTransform.position = toTransform.position;
            gameObjectTransform.rotation = toTransform.rotation;
        }
    }
    public void UpdateMessage(string text)
    {
        Message.text = text;
    }
}
