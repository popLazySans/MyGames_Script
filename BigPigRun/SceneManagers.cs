using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class SceneManagers : MonoBehaviour,IPointerClickHandler
{
    public PortalSpawner portalSpawner;
    public string sceneName;
    SceneStateMachine stateMachine;
    public PlayerSpeedHandler playerSpeedHandler;
    private ShowText showText;
    public DistanceToPoint distanceToPoint;
    private ScoreSave saveManager;
    public AudioSource winSound;
    public AudioSource loseSound;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneStateMachine>();
        showText = GameObject.FindGameObjectWithTag("ShowText").GetComponent<ShowText>();
        saveManager = GameObject.FindGameObjectWithTag("Saver").GetComponent<ScoreSave>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PassActive()
    {
        distanceToPoint.distToPoint(stateMachine.Scenenumber/2);
        if (stateMachine.Scenenumber == stateMachine.sceneGameObjects.Count-1)
        {
            saveManager.SaveCurrentScoreToList(distanceToPoint.currentPoint);
            showText.ShowWinText(distanceToPoint.distance, stateMachine.Scenenumber / 2, distanceToPoint.prevPoint, distanceToPoint.currentPoint);
            winSound.Play();
            distanceToPoint.resetDistance();
        }
        else
        {
            showText.ShowPassText (distanceToPoint.distance,stateMachine.Scenenumber/2,distanceToPoint.prevPoint,distanceToPoint.currentPoint);
            distanceToPoint.resetDistance();
        }
    }
    public void TryAgain()
    {
       // if (winText != null)
           // winText.text = "Please Balance Your Energy.";
        showText.ShowTryAgainText();
    }
    public void LoseActive()
    {
        distanceToPoint.distToPoint(stateMachine.Scenenumber/2);
        saveManager.SaveCurrentScoreToList(distanceToPoint.currentPoint);
        showText.ShowLoseText(distanceToPoint.distance, stateMachine.Scenenumber / 2, distanceToPoint.prevPoint, distanceToPoint.currentPoint);
        loseSound.Play();
        clickToAnotherScene("Menu");
        distanceToPoint.resetDistance();
        //playerSpeedHandler.stop();
    }
    public void clickToAnotherScene(string sceneName)
    {
        //UseForChangeByScene
        //SceneManager.LoadScene(sceneName);
        //UseForChangeByGameObject
        DestroyAllItem();
        stateMachine.ChangeScene(sceneName);
    }
    public void DestroyAllItem(){
        GameObject[] ItemList = GameObject.FindGameObjectsWithTag("Item");
        foreach(GameObject item in ItemList){
            Destroy(item);
        }
    }
    public void exit()
    {
        Application.Quit();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        stateMachine.ChangeScene(sceneName);
        if (sceneName == "Exit")
            Application.Quit();
    }
}
