using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using TMPro;
using UnityEngine.UI;
public class MainPlayerName : NetworkBehaviour
{
    public TMP_Text nameLabel;
    public TMP_Text namePrefab;
    private LoginManager loginManager;
    public NetworkVariable<NetworkString> playerNameA = new NetworkVariable<NetworkString>(new NetworkString { info = "Player A" }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<NetworkString> playerNameB = new NetworkVariable<NetworkString>(new NetworkString { info = "Player B" }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public Transform playerPos;
    public struct NetworkString : INetworkSerializable
    {
        public FixedString32Bytes info;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref info);
        }
        public override string ToString()
        {
            return info.ToString();
        }
        public static implicit operator NetworkString(string v) => new NetworkString() { info = new FixedString32Bytes(v) }; //implicit
    }
    public override void OnNetworkSpawn()
    {
        //GameObject canvas = GameObject.FindWithTag("MainCanvas");
        //nameLabel = Instantiate(namePrefab, Vector3.zero, Quaternion.identity) as TMP_Text;
        // nameLabel.transform.SetParent(canvas.transform);

        //postX.OnValueChanged += (int previousData, int newValue) => { Debug.Log("Owera ID" + OwnerClientId + ": post x = " + postX.Value); };
        playerNameA.OnValueChanged += (NetworkString previousData, NetworkString newValue) => { Debug.Log("Owner ID = " + OwnerClientId + ": new data = " + newValue.info); };
        playerNameB.OnValueChanged += (NetworkString previousData, NetworkString newValue) => { Debug.Log("Owner ID = " + OwnerClientId + ": new data = " + newValue.info); };

        /* if (IsServer)
         {
             //no implicit
             playerNameA.Value = new NetworkString() { info = new FixedString32Bytes("Player1") }; 
             //must implicit
             string name = "Player2";
             playerNameB.Value = name;
         }*/
        if (IsOwner)
        {
            loginManager = GameObject.FindObjectOfType<LoginManager>();
            if (loginManager != null)
            {
                string name = loginManager.getUsernameFromUser();
                if (IsOwnedByServer) { playerNameA.Value = name; }
                else { playerNameB.Value = name; }
            }
        }
    }
    public override void OnDestroy()
    {
        if (nameLabel != null)
            Destroy(nameLabel.gameObject);
        base.OnDestroy();
    }
    void Start()
    {
        
    }


    void Update()
    {
        SetLabelPosition();
        UpdatePlayerInfo();
    }
    private void SetLabelPosition()
    {
        //Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(playerPos.transform.position + new Vector3(0, 0f, 0));
        //nameLabel.transform.position = nameLabelPos;
        if (IsOwner)
        {
            nameLabel.gameObject.SetActive(false);
        }
    }
    private void UpdatePlayerInfo()
    {
        if (IsOwnedByServer)
        {
            nameLabel.text = playerNameA.Value.ToString();
        }
        else
        {
            nameLabel.text = playerNameB.Value.ToString();
        }
    }
}
