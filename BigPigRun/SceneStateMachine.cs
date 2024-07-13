using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneStateMachine : MonoBehaviour
{
    [SerializeField] public int Scenenumber = 0;
    public GameObject playerGameObject;
    public List<GameObject> sceneGameObjects = new List<GameObject>();
    public LineRenderer pointer_LineRenderer;
    public GameObject VRcanvasGameObject;
    public GameObject directionalLightGameObject;
    public GameObject lightGameObject;
    private GameObject distancePathObject;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        distancePathObject = GameObject.FindGameObjectWithTag("DistancePath");
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ChangeScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeScene(string sceneName)
    {
        pointer_LineRenderer.enabled = false;
        foreach (GameObject gameObject in sceneGameObjects)
        {
            gameObject.SetActive(false);
        }
        if (sceneName == "Menu")
        {
            toMainMenu();
        }
        else if (sceneName == "Play")
        {
            toGameplay();
        }
        else if (sceneName == "Rest")
        {
            toRest();
        }
        changeEnvironment();
        sceneGameObjects[Scenenumber].SetActive(true);
    }
    public void toMainMenu()
    {
        setPlayerPosition();
        Scenenumber = 0;
        pointer_LineRenderer.enabled = true;
        //VRcanvasGameObject.SetActive(false);
        distancePathObject.GetComponent<UpdateDistancePath>().isPlaying = false;
        directionalLightGameObject.SetActive(false);
    }
    public void toGameplay()
    {
        if (Scenenumber < sceneGameObjects.Count-1)
        {
            setPlayerPosition();
            Scenenumber += 1;
            //VRcanvasGameObject.SetActive(true);
            if (Scenenumber > 1)
            {
                distancePathObject.GetComponent<UpdateDistancePath>().isPlaying = true;
            }
        }
        else
        {
            toMainMenu();
        }
    }
    public void toRest()
    {
        setPlayerPosition();
        if (Scenenumber < sceneGameObjects.Count-1)
        {
            Scenenumber += 1;
            //VRcanvasGameObject.SetActive(false);
            distancePathObject.GetComponent<UpdateDistancePath>().isPlaying = false;
            directionalLightGameObject.SetActive(true);
        }
        else
        {
            toMainMenu();
        }
     
    }
    public void changeEnvironment()
    {
        if(sceneGameObjects[Scenenumber].tag == "NoonScene")
        {
            changeLight(1.25f,false,true,new Color32(150,239,255,255));
        }
        else if (sceneGameObjects[Scenenumber].tag == "NightScene")
        {
            changeLight(0f, true, false, new Color32(15, 15, 15, 255));
        }
    }
    private void changeLight(float ambientIntensityValue,bool hasPointLight,bool hasSun,Color32 color)
    {
        RenderSettings.ambientIntensity = ambientIntensityValue;
        lightGameObject.SetActive(hasPointLight);
        directionalLightGameObject.SetActive(hasSun);
        camera.backgroundColor = color;
    }
    public void setPlayerPosition(){
        playerGameObject.transform.position = new Vector3(0,1,0);
    }
}
