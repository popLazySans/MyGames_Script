using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System;
public class SetDatatoPlayer : NetworkBehaviour
{
    public CharacterList characterList;
    private GameObject character;
    public List<GameObject> characterinThisObjectList = new List<GameObject>();
    public NetworkVariable<int> CharacterID1 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> CharacterID2 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public CanvasVariable canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<CanvasVariable>();
        characterList = GameObject.FindGameObjectWithTag("CharacterSelecter").GetComponent<CharacterList>();
        if (IsOwner)
        {
            if (IsOwnedByServer)
            {
                CharacterID1.Value = characterList.selectedID;
            }
            else
            {
                CharacterID2.Value = characterList.selectedID;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
       
        if (IsOwnedByServer)
        {
            ModelActive(CharacterID1.Value);
            canvas.leftCharPicture.GetComponent<Image>().sprite = characterList.charactersList[CharacterID1.Value].CharacterInGameLogo;
            if (IsOwner)
            {
                canvas.iconSkill.GetComponent<Image>().sprite = characterList.charactersList[CharacterID1.Value].CharacterIcon_UltimateSkill;
            }
        }
        else
        {
            ModelActive(CharacterID2.Value);
            canvas.rightCharPicture.GetComponent<Image>().sprite = characterList.charactersList[CharacterID2.Value].CharacterInGameLogo;
            if (IsOwner)
            {
                canvas.iconSkill.GetComponent<Image>().sprite = characterList.charactersList[CharacterID2.Value].CharacterIcon_UltimateSkill;
            }
        }
    }

    public void ModelActive(int id)
    {
        for (int i = 0; i < characterinThisObjectList.Count; i++)
        {
            if (i != id)
            {
                characterinThisObjectList[i].SetActive(false);
            }
            else
            {
                characterinThisObjectList[i].SetActive(true);
            }

        }
    }

}
