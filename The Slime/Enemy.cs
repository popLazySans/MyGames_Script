using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;
    public bool GameActived = false;
    public GameObject cameraController;
    SpriteRenderer mesh;
    public GameObject soundSource;
    AudioSource soundEffect;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<SpriteRenderer>();
        soundEffect = soundSource.GetComponent<AudioSource>();
        Current.Score += 1;
        HP = 5;
    }

    // Update is called once per frame
    void Update()
    {
        GameActived = CameraControl.GameStarted;
        if (HP == 0)
        {
            Current.Score -= 1;
            soundEffect.Play();
            Destroy(this.gameObject);
        }
    }

    public void OnMouseDown()
    {
        if (GameActived == true && Time.timeScale == 1) 
        {
            StartCoroutine(Damaged());
            HP -= 1;
        }
        
    }
    IEnumerator Damaged()
    {
        mesh.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mesh.color = Color.white;
    }

}
