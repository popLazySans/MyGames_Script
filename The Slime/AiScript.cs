using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AiScript : MonoBehaviour
{
    public float speed = 200f;
    public float nextWayPointDistance = 0.5f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    public Transform target;
    Animator anim;
    bool RunState = false;
    bool StandState = false;
    bool ATKState = false;
    public static bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 1, 0.3f);
        
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
        
    }

    void UpdatePath()
    {
        if (seeker.IsDone()&&CameraControl.GameStarted == true )
        {
            if (RunState ==false && StandState == false && ATKState == false)
            {
                RunState = true;
            }
            seeker.StartPath(rb.position, target.position, OnPathComplete);
            
        }
        else {
            
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (CameraControl.GameStarted == false)
        {
            speed = 0f;
        }
        else
        {
            speed = 200f;
        }
        if (path == null)
        {
           
            return;
        }
        if (currentWaypoint>=path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;


        anim.SetBool("Run", RunState);
        anim.SetBool("Stand", StandState);
        anim.SetBool("ATK", ATKState);
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance<nextWayPointDistance)
        {
            currentWaypoint++;
        }
        if (force.x >= 0.01f)
        {
            this.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        }

      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(ATKstate());
            isAttack = true;
        }
    }
    IEnumerator ATKstate()
    {
        ATKState = true;
        RunState = false;
        StandState = false;
        yield return new WaitForSeconds(0.5f);
       
        StartCoroutine(restState());
     
        
    }
    IEnumerator restState()
    {
       
        StandState = true;
        RunState = false;
        ATKState = false;
        speed = 0;
        yield return new WaitForSeconds(2f);
        StandState = false;
        speed = 200;
    }
}
