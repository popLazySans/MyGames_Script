using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Vector3 PlayerPosition;
    public GameObject Player;
    internal float speed = 0.85f;
    private AudioSource roarSource;
    // Start is called before the first frame update
    private void OnEnable()
    {
        roarSource = gameObject.GetComponent<AudioSource>();
        InvokeRepeating("Bird_Roar", 10f, 10f);
    }
    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        PlayerPosition = new Vector3(Player.transform.position.x, 0.5f, Player.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, PlayerPosition, Time.deltaTime * speed);
        transform.LookAt(Player.transform.position);
    }
    public void Bird_Roar()
    {
        roarSource.Play();
    }
}
