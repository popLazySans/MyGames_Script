using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Character : MonoBehaviour
{
    public int CharacterId;
    public string CharacterName;
    public GameObject CharacterModel;
    public GameObject CharacterSkillPrefab;
    public Sprite CharacterIcon_Self;
    public Sprite CharacterIcon_PassiveSkill;
    public Sprite CharacterIcon_UltimateSkill;
    public Sprite CharacterLogo;
    public string CharacterDetail;
    public string CharacterPassiveDetail;
    public string CharacterUltimateDetail;
    public Sprite CharacterBackground;
    public Sprite CharacterInGameLogo;
    public Character(int id,string name,GameObject model,GameObject skillPreb,Sprite self,Sprite passive,Sprite ulti,Sprite logo,string detail,string passiveDetail,string ultimaterDetail,Sprite backgrond,Sprite charInGameLogo)
    {
        CharacterId = id;
        CharacterName = name;
        CharacterModel = model;
        CharacterSkillPrefab = skillPreb;
        CharacterIcon_Self = self;
        CharacterIcon_PassiveSkill = passive;
        CharacterIcon_UltimateSkill = ulti;
        CharacterLogo = logo;
        CharacterDetail = detail;
        CharacterPassiveDetail = passiveDetail;
        CharacterUltimateDetail = ultimaterDetail;
        CharacterBackground =backgrond;
        CharacterInGameLogo = charInGameLogo;
    }
}
