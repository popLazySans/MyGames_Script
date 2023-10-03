using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Netcode.Transports.UTP;
public class LoginManager : MonoBehaviour
{
    private bool isApproveConnection = false;
    public List <TMP_InputField> InputText = new List<TMP_InputField>();
    public List <TMP_InputField> InputRoomID = new List<TMP_InputField>();
    public GameObject hostPanel;
    public GameObject joinPanel;
    public GameObject leaveButton;
    public GameObject scorePanel;
    public GameObject waitPanel;
    public GameObject loadingPanel;
    public int x_Range = 0;
    public int z_Range = 0;
    //public List<string> roomList = new List<string>();
    public string username;
    public string roomID;
    public int selectedId;
    public CharacterList characterList;
    public int playerCount = 0;
    public TMP_Text countDownText;
    UnityTransport transport;
    public TMP_InputField joinCodeInput;
    public string joinCode;
    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += HandleServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisConnected;
        //SetStartPanel(false);
    }
    private void SetStartPanel(bool isPlay)
    {
        hostPanel.SetActive(!isPlay);
        scorePanel.SetActive(isPlay);
        loadingPanel.SetActive(isPlay);
        joinPanel.SetActive(!isPlay);
        //leaveButton.SetActive(isPlay);
    }
    private void OnDestroy()
    {
        if (NetworkManager.Singleton == null) { return; }
        NetworkManager.Singleton.OnServerStarted -= HandleServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisConnected;
    }
    private void HandleClientDisConnected(ulong clientId)
    {
        playerCount -= 1;
    }
    public void leave()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.Shutdown();
            NetworkManager.Singleton.ConnectionApprovalCallback -= ApprovalCheck;
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.Shutdown();
        }
        SetStartPanel(false);
        waitPanel.SetActive(false);
        DestroyAllGameObjectWithTag("PoolingObject");
    }

    private static void DestroyAllGameObjectWithTag(string tag)
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < allObjects.Length; i++)
        {
            Destroy(allObjects[i]);
        }
    }

    public void newGame()
    {
        NetworkManager.Singleton.Shutdown();
        NetworkManager.Singleton.ConnectionApprovalCallback -= ApprovalCheck;
        SetStartPanel(false);
        waitPanel.SetActive(false);
    }
    private void HandleClientConnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            SetStartPanel(true);
        }
    }
    private void OnConnectedToServer()
    {
        Debug.Log("ServerConnected");
    }
    private void HandleServerStarted()
    {
        Debug.Log("ServerConnected");
        //throw new System.NotImplementedException();
    }

    public async void Host()
    {
        if (RelayManager.Instance.IsRelayEnabled)
        {
            await RelayManager.Instance.CreateRelay();
        }
        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
        NetworkManager.Singleton.StartHost();
    }
    public void AddHostDataToList()
    {
        username = getUsernameFromUser();
        roomID = getRoomIDFromUser();
        //roomList.Add(username + "_" + roomID);
    }
    public void stopServerForClient()
    {
        NetworkManager.Singleton.Shutdown();
    }
    public async void Client()
    {
        joinCode = joinCodeInput.GetComponent<TMP_InputField>().text;
        if (RelayManager.Instance.IsRelayEnabled && !string.IsNullOrEmpty(joinCode))
        {
            await RelayManager.Instance.JoinRelay(joinCode);
        }
        username =  getUsernameFromUser();
        roomID = getRoomIDFromUser();
        selectedId = characterList.selectedID;
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes(username+"_"+roomID);
        NetworkManager.Singleton.StartClient();
    }
    public string getRoomIDFromUser()
    {
        string room = "";
        foreach (TMP_InputField inputField in InputRoomID )
        {
            if (inputField.text != "")
            {
                room = inputField.GetComponent<TMP_InputField>().text;
            }
        }
        return room;
    }
    public string getUsernameFromUser()
    {
        string user = "";
        foreach(TMP_InputField inputField in InputText)
        {
            if(inputField.text != "")
            {
                user = inputField.GetComponent<TMP_InputField>().text;
            }
        }
        return user;
    }
    private bool approveNameConnection(string clientData, string serverData)
    {
        bool isApprove = System.String.Equals(clientData.Trim(), serverData.Trim()) ? false : true;
        return isApprove;
    }
    private bool approveRoomIDConnection(string clientData, string serverData)
    {
        bool isApprove = System.String.Equals(clientData.Trim(), serverData.Trim()) ? true : false;
        return isApprove;
    }
    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        
        // The client identifier to be authenticated
        var clientId = request.ClientNetworkId;

        // Additional connection data defined by user code
        var connectionData = request.Payload;
        int byteLenght = connectionData.Length;
        bool isApprove = false;
        string clientData = System.Text.Encoding.ASCII.GetString(connectionData, 0, byteLenght);
        if (byteLenght == 0)
        {

        }
        if (byteLenght > 0)
        {
            string[] splitText = clientData.Split(char.Parse("_"));
            string clientUsername = splitText[0];
            string clientRoomID = splitText[1];
            string usernameData = getUsernameFromUser();
            string roomIDData = getRoomIDFromUser();
            isApprove =  (approveNameConnection(clientUsername, usernameData))&(approveRoomIDConnection(clientRoomID,roomIDData));
            if (isApprove == false) { clientId -= 1; }
            //else
            //{
            //    foreach(string room in roomList){
            //        if(room == clientData)
            //        {
            //            roomList.Remove(room);
            //        }
            //    }
            //}
            Debug.Log(clientUsername + " " + clientRoomID + " " + isApprove);
        }
        if (playerCount > 2)
        {
            isApprove = false;
        }
        else
        {
            if(playerCount == 0)
            {
                isApprove = true;
            }
            playerCount += 1;
            setSpawnLocation(clientId, response);
            StopTimeIfOnePlayer(clientId);
            NetworkLog.LogInfoServer("SpawnPos of " + clientId + "is" + response.Position.ToString());
        }
        response.Approved = isApprove;
        response.CreatePlayerObject = isApprove;
        //startCount(isApprove);
        // Your approval logic determines the following values
        //if(connectionData = )

        // The prefab hash value of the NetworkPrefab, if null the default NetworkManager player prefab is used
        response.PlayerPrefabHash = null;

        // Position to spawn the player object (if null it uses default of Vector3.zero)
        //response.Position = Vector3.zero;

        // Rotation to spawn the player object (if null it uses the default of Quaternion.identity)
        //response.Rotation = Quaternion.identity;

        // If response.Approved is false, you can provide a message that explains the reason why via ConnectionApprovalResponse.Reason
        // On the client-side, NetworkManager.DisconnectReason will be populated with this message via DisconnectReasonMessage
        //response.Reason = "Some reason for not approving the client";

        // If additional approval steps are needed, set this to true until the additional steps are complete
        // once it transitions from true to false the connection approval response will be processed.
        response.Pending = false;
    }
    private void setSpawnLocation(ulong clientId,NetworkManager.ConnectionApprovalResponse response){
        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            spawnPos = new Vector3(randomPosition(x_Range), 0, randomPosition(z_Range));
           // spawnRot = Quaternion.Euler(0, 135, 0);
        }
        else
        {
            switch (NetworkManager.Singleton.ConnectedClients.Count)
            {
                case 1:

                    spawnPos = new Vector3(randomPosition(x_Range), 0, randomPosition(z_Range));//spawnRot = Quaternion.Euler(0, 100, 0);
                    break;
                case 2:
                    spawnPos = new Vector3(randomPosition(x_Range), 0, randomPosition(z_Range));//spawnRot = Quaternion.Euler(0, 80, 0);
                    break;
            }
        }
        response.Position = spawnPos;
        response.Rotation = spawnRot;
    }
    public int randomPosition(int range)
    {
        int randomNumber = Random.Range(1, range);
        return randomNumber;
    }
    public void StopTimeIfOnePlayer(ulong isTwoPlayer)
    {
        if(isTwoPlayer >= 1)
        {
            Time.timeScale = 1;
            waitPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            waitPanel.SetActive(true);
        }
    }
    public void startCount(bool isAppove)
    {
        countDownText.gameObject.SetActive(isAppove);
    }

}
