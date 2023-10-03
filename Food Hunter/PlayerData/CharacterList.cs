using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class CharacterList : MonoBehaviour
{
    LoginManager loginManager;
    public List<Character> charactersList = new List<Character>();
    public int selectedID =0;
    private void Start()
    {
        //loginManager = gameObject.GetComponent<LoginManager>();
    }
    public void OnConnectedToServer()
    {
    }
}
