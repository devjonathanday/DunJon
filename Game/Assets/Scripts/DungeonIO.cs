using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;
using System.IO;
using UnityEngine.SceneManagement;

public enum EnemyType { ZOMBIE, SKELETON }
public class Enemy
{
    public EnemyType type;
    public int health;

    public Enemy(EnemyType _type, int _health)
    {
        type = _type;
        health = _health;
    }
}

public class DungeonRoom
{
    public Vector2 size;
    public List<Enemy> enemies = new List<Enemy>();
}

public class DungeonIO : MonoBehaviour
{
    public List<DungeonRoom> rooms = new List<DungeonRoom>();
    void Awake()
    {
        GameObject[] loaderObjects = GameObject.FindGameObjectsWithTag("DungeonLoader");

        if (loaderObjects.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public GameObject errorObject;
    public void LoadDialog()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Load Dungeon", "", "jon", false);
        if (paths.Length == 0) return;
        LoadDungeon(paths[0]);
    }
    public void LoadDungeon(string path)
    {
        rooms.Clear();
        try
        {
            StreamReader reader = new StreamReader(path);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Length == 0) continue;

                //Initialize a new room
                DungeonRoom newRoom = new DungeonRoom();
                //Parse through the data on that line
                string[] frags = line.Split(',');
                for (int i = 0; i < frags.Length; i++)
                {
                    switch (frags[i])
                    {
                        case "str":
                            {
                                newRoom.size.x = int.Parse(frags[i + 1]);
                                newRoom.size.y = int.Parse(frags[i + 2]);
                                i += 2;
                            }
                            break;
                        case "rom":
                            {
                                newRoom.size.x = int.Parse(frags[i + 1]);
                                newRoom.size.y = int.Parse(frags[i + 2]);
                                i += 2;
                            }
                            break;
                        case "end":
                            {
                                newRoom.size.x = int.Parse(frags[i + 1]);
                                newRoom.size.y = int.Parse(frags[i + 2]);
                            }
                            break;
                        case "zmb":
                            {
                                newRoom.enemies.Add(new Enemy(EnemyType.ZOMBIE, int.Parse(frags[i + 1])));
                                i++;
                            }
                            break;
                        case "skl":
                            {
                                newRoom.enemies.Add(new Enemy(EnemyType.SKELETON, int.Parse(frags[i + 1])));
                                i++;
                            }
                            break;
                        case "ext":
                            break;
                        default:
                            break;
                    }
                }
                rooms.Add(newRoom);
            }
        }
        catch(System.Exception e)
        {
            errorObject.SetActive(true);
            print(e);
            return;
        }
        SceneManager.LoadScene("BaseGame");
        Screen.SetResolution(Screen.width, Screen.height, Screen.fullScreen);
    }
}