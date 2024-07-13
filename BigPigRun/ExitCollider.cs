using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Player"){
            Application.Quit();
        }
    }
}
