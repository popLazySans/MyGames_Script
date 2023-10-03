using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DataWriter : MonoBehaviour
{
    private MainPlayerName mainPlayerName;
    private HostRoomID hostRoomID;
    private LoginManager loginManager;
    // Start is called before the first frame update
    [System.Serializable]
    public class Room
    {
        public string Name;
        public string RoomID;
    }
    [System.Serializable]
    public class RoomsList
    {
        public Room[] rooms;
    }

    public Room room = new Room();
    public RoomsList roomsList = new RoomsList();

    private void Start()
    {
    }
    public void outputJSON()
    {
        loginManager = GameObject.FindObjectOfType<LoginManager>();
        if(loginManager != null)
        {
            room.Name = loginManager.username;
            room.RoomID = loginManager.roomID;
            string strOutput = JsonUtility.ToJson(room);
            File.WriteAllText(@"C:\Users\USER\Downloads\Test.txt", strOutput);
        }
      
    }
    // Update is called once per frame

}
