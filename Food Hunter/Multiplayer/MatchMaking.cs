using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using System.Threading.Tasks;
using ParrelSync;

public class MatchMaking : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public GameObject startButton;
    public GameObject matchMakingPanel;
    private string playerName;
    string lobbyName = "MyLobby";
    private Lobby joinedLobby;

    public async void StartGame()
    {
        startButton.SetActive(false);
        matchMakingPanel.SetActive(false);
        playerName = playerNameInput.GetComponent<TMP_InputField>().text;
        joinedLobby = await JoinLobby()?? await CreateLobby();
        if (joinedLobby == null)
        {
            startButton.SetActive(true);
            matchMakingPanel.SetActive(true);
        }
    }
    private async Task<Lobby> CreateLobby()
    {
        try
        {
            string lobbyName = "MyLobby";
            int maxPalyer = 2;
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(2);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            CreateLobbyOptions options = new CreateLobbyOptions
            {
                IsPrivate = false,
                Player = new Player
                {
                    Data = new Dictionary<string, PlayerDataObject>
                    {
                        {
                            "PlayerName",new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member,playerName)
                        }
                    }
                },
                Data = new Dictionary<string, DataObject>
                {
                    {"JoinCodeKey",new DataObject(DataObject.VisibilityOptions.Public,joinCode) }
                }
            };
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPalyer, options);
            StartCoroutine(HeartBeatLobby(lobby.Id, 15));
            Debug.Log("Lobby is created : " + lobby.Name + " : " + lobby.MaxPlayers + " : " + lobby.Id + " : " + lobby.LobbyCode);
            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartHost();
            LobbyController.Instance.PrintPlayer(lobby);
            return lobby;
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
            return null;
        }
    }
    private async Task<Lobby> JoinLobby()
    {
        try
        {
            Lobby lobby = await FindRandomLobby();
            if (lobby == null) return null;

            if (lobby.Data["JoinCodeKey"].Value != null)
            {
                string joinCode = lobby.Data["JoinCodeKey"].Value;
                Debug.Log("JoinCode = "+joinCode);
                JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

                RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
                NetworkManager.Singleton.StartClient();
                return lobby;
            }
            return null;
        }
        catch(LobbyServiceException e)
        {
            Debug.Log("No lobby found");
            return null;
        }
    }
    private async Task<Lobby> FindRandomLobby()
    {
        try
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots,"1",QueryFilter.OpOptions.GE)
                }
            };
            QueryResponse queryResponse = await LobbyService.Instance.QueryLobbiesAsync(queryLobbiesOptions);
            Debug.Log("Lobbies found: " + queryResponse.Results.Count);
            foreach (Lobby lobby in queryResponse.Results)
            {
                return lobby;
            }
            return null;
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
            return null;
        }
    }
    private static IEnumerator HeartBeatLobby(string lobbyId, float waitTime)
    {
        var delay = new WaitForSecondsRealtime(waitTime);
        while (true)
        {
            Lobbies.Instance.SendHeartbeatPingAsync(lobbyId);
            yield return delay;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
