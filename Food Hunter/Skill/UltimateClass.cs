using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class UltimateClass : NetworkBehaviour
{
    public SetDatatoPlayer characterData;
    private CharacterList characterList;
    private List<GameObject> skillPrefab = new List<GameObject>();
    public List<GameObject> skillPrefabSpawner = new List<GameObject>();
    private Animator anim;
    private OwnerNerworkAnimator netAnim;
    private void Start()
    {
        characterList = GameObject.FindGameObjectWithTag("CharacterSelecter").GetComponent<CharacterList>();
        SetSkillPrefab();
    }
    public void SetSkillPrefab()
    {
        if (skillPrefab.Count == 0) 
        {
            foreach (Character data in characterList.charactersList)
            {
                skillPrefab.Add(data.CharacterSkillPrefab);
            }
        }
    }
    public void Ultimate_1(){
        SetCastAnimation(0);
        AllowedPrefab_Ultimate(0);
    }
    public void Ultimate_2()
    {
        SetCastAnimation(1);
        gameObject.GetComponent<CharactorPoint>().TargetPoint += 5; 
    }
    public void Ultimate_3()
    {
        SetCastAnimation(2);
        AllowedPrefab_Ultimate(2);
    }  
    public void AllowedPrefab_Ultimate(int numberPrefab)
    {
        if (numberPrefab == 0)
        {
            Spawn_1ServerRpc();
        }
        else if (numberPrefab == 2)
        {
            Spawn_3ServerRpc();
        }
      
    } 
    [ServerRpc]
    public void Spawn_1ServerRpc()
    {
        GameObject prefab = Instantiate(skillPrefab[0],skillPrefabSpawner[0].transform.position, skillPrefabSpawner[0].transform.rotation);
        prefab.GetComponent<NetworkObject>().Spawn(true);
    }
    [ServerRpc]
    public void Spawn_3ServerRpc()
    {
        int x_range = Random.Range(-10,10);
        int z_range = Random.Range(-10, 10);
        GameObject prefab = Instantiate(skillPrefab[2], skillPrefabSpawner[2].transform.position,skillPrefabSpawner[2].transform.rotation);
        prefab.GetComponent<NetworkObject>().Spawn(true);
    }
    public void SetCastAnimation(int numberObjList)
    {
        anim = characterData.characterinThisObjectList[numberObjList].GetComponent<Animator>();
        netAnim = characterData.characterinThisObjectList[numberObjList].GetComponent<OwnerNerworkAnimator>();
        anim.SetTrigger("Ultimate");
        netAnim.SetTrigger("Ultimate");
    }
}
