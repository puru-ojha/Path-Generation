using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{

    RayCheckerTwo rayChecker;
    HexagonSpawner hexagonSpawner;
    public GameObject wall;
    public Queue<GameObject> wallQueue = new Queue<GameObject>();
    int wallcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        rayChecker = gameObject.GetComponent<RayCheckerTwo>();
        hexagonSpawner = gameObject.GetComponent<HexagonSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Press x for wall generation ");
        if (Input.GetKeyDown(KeyCode.X))
        {
            Driver();
        }
    }
    void WallGenerator()
    {
        //Hexagon has to be spawned
        //hexagonSpawner.SpawnHexagon();
        wallcount = wallcount + 1;
        int rotationchosen = rayChecker.rotationChosen;

        Vector3 wallsLocation = rayChecker.center + Quaternion.Euler(new Vector3(0, rayChecker.playerDirection.y + rotationchosen, 0)) * Vector3.forward * (rayChecker.hexLength);
        Vector3 wallsDirection = new Vector3(0, rayChecker.playerDirection.y + rotationchosen,0);
        GameObject currentWall =  Instantiate(wall, wallsLocation, Quaternion.Euler( wallsDirection));
        wallQueue.Enqueue(currentWall);
        DestroyWall();
        //NewPlayerPosition(wallsLocation, wallsDirection);
    }
    /*
    void NewPlayerPosition(Vector3 wallsPos, Vector3 wallsDir)
    {
        Vector3 path = new Vector3(0, 0, rayChecker.pathlen);
        path = Quaternion.Euler(wallsDir) * path;
        rayChecker.playpos = wallsPos + path;
        rayChecker.playdir = wallsDir;
    }
    */
    void DestroyWall()
    {
        //wallcount = wallcount + 1;
        //Debug.Log("The wall count is " + wallcount);
        if (wallcount > 2)
        {
            Debug.Log("One wall should go ");
            Destroy(wallQueue.Dequeue());
            wallcount --;
        }

    }
    void Driver()
    {
        rayChecker.mainFunction();
        hexagonSpawner.SpawnHexagon();
        WallGenerator();
        rayChecker.MoveForward();

    }

}
