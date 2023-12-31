using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
public class UseSkill : NetworkBehaviour
{
    public PlayerData playerData;

    public void UseSkillfromId()
    {
        TurnBaseStateMachine TurnBaseManager = GameObject.FindGameObjectWithTag("TurnBaseManager").GetComponent<TurnBaseStateMachine>();
        playerUseSkillServerRpc(Convert.ToInt32(NetworkManager.Singleton.LocalClientId), new ServerRpcParams());
        TurnBaseManager.rollableDice -= 1;
        if (TurnBaseManager.rollableDice == 0)
        {
            TurnBaseManager.isRolled = false;
            TurnBaseManager.skillObject.SetActive(false);
            TurnBaseManager.rollableText.enabled = false;
            TurnBaseManager.ChangeToAnotherPlayerTurnServerRpc();
        }

    }
    [ServerRpc(RequireOwnership = false)]
    public void playerUseSkillServerRpc(int player,ServerRpcParams serverRpcParams)
    {
        Debug.Log("Player : "+player);
        PlayerData playerData = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerData>();
        CharacterList characterList = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterList>();
        switch (player)
        {
            case 0:
                characterList.characters[playerData.player1CharId.Value].characterSkill.UseSkill(player);
                break;
            case 1:
                characterList.characters[playerData.player2CharId.Value].characterSkill.UseSkill(player);
                break;
            case 2:
                characterList.characters[playerData.player3CharId.Value].characterSkill.UseSkill(player);
                break;
            case 3:
                characterList.characters[playerData.player4CharId.Value].characterSkill.UseSkill(player);
                break;
        }
    }

}
