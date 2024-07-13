using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public PointManager pointManager;
    private EnemyMove enemyMove;
    private SceneManagers sceneManagers;
    private bool isRecovering;
    // Start is called before the first frame update
    void Start()
    {
        enemyMove = gameObject.GetComponent<EnemyMove>();
        sceneManagers = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagers>();
        isRecovering = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isRecovering == false)
        {
            if (pointManager.Vitamin >= 100)
            {
                pointManager.SetVitaminToZero();
                Stunned();
            }
            else
            {
                gameObject.transform.position = new Vector3(0,0,-17.82f);
                pointManager.resetPoint();
                sceneManagers.LoseActive();
                //gameObject.SetActive(false);
            }
        }
    }
    public void Stunned()
    {
        isRecovering = true;
        enemyMove.speed = 0;
        StartCoroutine(Recovering());
    }
    IEnumerator Recovering()
    {
        yield return new WaitForSeconds(5);
        enemyMove.speed = 10;
        isRecovering = false;
    }
}
