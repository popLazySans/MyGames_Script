using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerHit : NetworkBehaviour
{
    public CharactorPoint charactorPoint;
    public GaugeManager gaugeManager;
    private BaseObject baseObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PoolingObject")
        {
            if (!IsLocalPlayer) return;
            CreateCollisionObject(collision.gameObject);
            charactorPoint.receivePoint(baseObject.point);
        }
        else if(collision.gameObject.tag == "Treasure")
        {
            if (!IsLocalPlayer) return;
            CreateCollisionObject(collision.gameObject);
            gaugeManager.ReceiveGauge();
        }
    }
    public void CreateCollisionObject(GameObject collisionObject)
    {
        BaseObject BasePoint = collisionObject.gameObject.GetComponent<BaseObject>();
        baseObject = new BaseObject(collisionObject.gameObject.name, BasePoint.point);
    }

}
