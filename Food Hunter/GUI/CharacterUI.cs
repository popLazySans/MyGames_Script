using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterUI : MonoBehaviour
{
    public int selectedId = 0;
    public CharacterList characterList;
    public Character character;
    public TMP_Text numberId;
    public Image Logo;
    public Image passiveLogo;
    public Image ultimateLogo;
    public GameObject Self;
    public TMP_Text Detail;
    public Image BackGrond;
    public GameObject passiveDetail;
    public GameObject ultimateDetail;
    // Start is called before the first frame update
    void Start()
    {

   
    }

    // Update is called once per frame
    void Update()
    {
        character = characterList.charactersList[selectedId];
        characterList.selectedID = selectedId;
        SetCharacterUI();
    }
    public void SetCharacterUI()
    {
        numberId.text = character.CharacterId.ToString();
        Logo.sprite = character.CharacterLogo;
        passiveLogo.sprite = character.CharacterIcon_PassiveSkill;
        ultimateLogo.sprite = character.CharacterIcon_UltimateSkill;
        Self.GetComponent<Image>().sprite = character.CharacterIcon_Self;
        Self.GetComponent<RectTransform>().sizeDelta = new Vector2(character.CharacterIcon_Self.rect.width,character.CharacterIcon_Self.rect.height);
        Detail.text = character.CharacterDetail;
        BackGrond.sprite = character.CharacterBackground;
        passiveDetail.GetComponentInChildren<TMP_Text>().text = character.CharacterPassiveDetail;
        ultimateDetail.GetComponentInChildren<TMP_Text>().text = character.CharacterUltimateDetail;
    }
    public void toLeft()
    {
        if (characterList.charactersList[selectedId].CharacterId != 0)
        {
            Self.SetActive(false);
            Self.SetActive(true);
            selectedId -= 1;
            
        }
        else
        {
            Self.SetActive(false);
            Self.SetActive(true);
            selectedId = characterList.charactersList.Count - 1;
        }
    }
    public void toRight()
    {
        if (characterList.charactersList[selectedId].CharacterId != characterList.charactersList.Count-1)
        {
            Self.SetActive(false);
            Self.SetActive(true);
            selectedId += 1;
        }
        else
        {
            Self.SetActive(false);
            Self.SetActive(true);
            selectedId = 0;
        }
        
    }
}
