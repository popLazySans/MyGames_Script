using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SwipeDetection : MonoBehaviour
{
    public float minDistance = .2f;
   public float maxTime = 1f;

    InputManager inputManager;
    Vector2 startPosition;
    Vector2 endPosition;
    float startTime;
    float endTime;
    // Start is called before the first frame update
    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    [SerializeField]
    public GameObject trail;
    private Coroutine coroutine;

    void SwipeStart(Vector2 postion, float time)
    {
        startPosition = postion; startTime = time;
        trail.SetActive(true);
        trail.transform.position = postion;
        coroutine = StartCoroutine(Trail());
    }

    public IEnumerator Trail()
    {
        while (true)
        {
            trail.transform.position = inputManager.PrimaryPosition();
            yield return null;
        }
    }

    void SwipeEnd(Vector2 postion, float time)
    {
        StopCoroutine(coroutine);
        trail.SetActive(false);
        endPosition = postion; endTime = time;
        DetectSwipe();
    }

    void DetectSwipe()
    {
        Debug.Log("Start pos = " + startPosition); Debug.Log("End pos = " + endPosition);
        if (Vector3.Distance(startPosition, endPosition) >= minDistance && (endTime - startTime) <= maxTime)
        {
            Debug.Log("Swipe detected");
            Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            Vector3 direction3 = endPosition - startPosition;
            Vector2 direction2 = new Vector2(direction3.x, direction3.y).normalized;
            SwipeDirection(direction2);
        }
    }
    
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = 0.9f;
    public slimeMove moveScript;
    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up,direction)>directionThreshold)
        {
            if (moveScript != null)
            {
                moveScript.SwipeToJump(1f);
            }
        }
        else if (Vector2.Dot(Vector2.down,direction)>directionThreshold)
        {
            if (moveScript != null)
            {
                moveScript.SwipeToWalk(0f);
                moveScript.SwipeToJump(0f);
            }
        }
        else if (Vector2.Dot(Vector2.left,direction)>directionThreshold)
        {
            if (moveScript != null)
            {
                moveScript.SwipeToWalk(-0.5f);
            }
        }
        else if (Vector2.Dot(Vector2.right,direction)>directionThreshold)
        {
            if (moveScript != null)
            {
                moveScript.SwipeToWalk(0.5f);
            }
        }
        Debug.Log("Swiped");
    }
}
