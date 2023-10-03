using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using TMPro;
public class HostRoomID : NetworkBehaviour
{
    private LoginManager loginManager;
    public NetworkVariable<NetworkString> roomID = new NetworkVariable<NetworkString>(new NetworkString { info = "Data1" }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

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
    // Start is called before the first frame update
    void Start()
    {
    }
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            loginManager = GameObject.FindObjectOfType<LoginManager>();
            if (loginManager != null)
            {
                string roomID_data = loginManager.getRoomIDFromUser();
                roomID.Value = roomID_data;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
