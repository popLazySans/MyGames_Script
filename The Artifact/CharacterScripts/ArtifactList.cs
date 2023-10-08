using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactList : MonoBehaviour
{
    public List<GameObject> artifactList;
    public GameObject spawnPosition;
    public WinOrLose winorlose;
    public int whichItem;
    // Start is called before the first frame update
    void Start()
    {
        winorlose = gameObject.GetComponent<WinOrLose>();
    }

    // Update is called once per frame
    void Update()
    {
        if (winorlose.SetArtifact == true)
        {
            whichItem = Random.Range(0,artifactList.Count);
            Vector2 randomSpawnPos = new Vector2(spawnPosition.transform.position.x,Random.Range(spawnPosition.transform.position.y-3.5f,spawnPosition.transform.position.y-0.5f));
            Instantiate(artifactList[whichItem],randomSpawnPos,spawnPosition.transform.rotation);
            winorlose.SetArtifact = false;
        }
    }
}
