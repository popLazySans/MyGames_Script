using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;
public class LobbyController : Singleton<LobbyController>
{
    private string playerName;
    private Lobby hostLobby;
    private void Start()
    {
        playerName = "name" + Random.Range(1, 999);
        Debug.Log("Player Name = "+ playerName);
    }
    private async void CreateLobby()
    {
        try
        {
            string lobbyName = "MyLobby";
            int maxPalyer = 2;
            CreateLobbyOptions options = new CreateLobbyOptions
            {
                IsPrivate = true,
                Player = new Player
                {
                    Data = new Dictionary<string, PlayerDataObject>
                    {
                        {
                            "PlayerName",new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member,playerName)
                        }
                    }
                }
            };
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPalyer,options);
            hostLobby = lobby;
            StartCoroutine(HeartBeatLobby(hostLobby.Id, 15));
            Debug.Log("Lobby is created : " + lobby.Name + " : " + lobby.MaxPlayers + " : " + lobby.Id+ " : " + lobby.LobbyCode);
            PrintPlayer(lobby);
        }catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public void PrintPlayer(Lobby lobby)
    {
        foreach (Player player in lobby.Players)
        {
            Debug.Log("Players = " + player.Id + " : " + player.Data["PlayerName"].Value);
        }
    }
    private async void JoinLobbyByCode(string lobbyCode)
    {
        try
        {
            await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode);
            Debug.Log("Joined Lobby by code : " + lobbyCode);
        }catch(LobbyServiceException e) { Debug.Log(e); }
    }
    private async void QuickJoinLobby() 
    {
        try
        {
            Lobby lobby = await Lobbies.Instance.QuickJoinLobbyAsync();
            Debug.Log("Joined Lobby : " + lobby.Name + " , " + lobby.AvailableSlots);
        }
        catch (LobbyServiceException e) { Debug.Log(e); }
    }
    private static IEnumerator HeartBeatLobby(string lobbyId,float waitTime)
    {
        var delay = new WaitForSecondsRealtime(waitTime);
        while (true)
        {
            Lobbies.Instance.SendHeartbeatPingAsync(lobbyId);
            yield return delay;
        }
    }
    private async void LobbiesList()
    {
        try
        {
            QueryLobbiesOptions options = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots,"0",QueryFilter.OpOptions.GT)
                },
                Order = new List<QueryOrder>
                {
                    new QueryOrder(false,QueryOrder.FieldOptions.Created)
                }
            };
            QueryResponse queryResponse = await LobbyService.Instance.QueryLobbiesAsync();
            Debug.Log("Lobbies found : " + queryResponse.Results.Count);
            foreach(Lobby lobby in queryResponse.Results)
            {
                Debug.Log("Lobby : " + lobby.Name + " , " + lobby.MaxPlayers);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    private async void JoinLobby()
    {
        try
        {
            QueryResponse queryResponse = await LobbyService.Instance.QueryLobbiesAsync();
            await Lobbies.Instance.JoinLobbyByIdAsync(queryResponse.Results[0].Id);
            Debug.Log("Joined Lobby : " + queryResponse.Results[0].Name + "," + queryResponse.Results[0].AvailableSlots);
        }
        catch (LobbyServiceException e) { Debug.Log(e); }
    }
}