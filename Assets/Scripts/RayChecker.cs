using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayChecker : MonoBehaviour
{
    //This script checks for the rays not hitting any surface from the player position and direction and generates a list of no hit rays.

    int pathlen = 20;
    int pathwid = 8;
    public PlayerSpawner playerSpawner;
    public Vector3 playpos;
    public Vector3 playdir;
    public Vector3 center;
    public List<int> noHitRayIndex;
    public List<Vector3> locationFromCenter;
    public List<int> rotationFromPlayer;
    //public Dictionary<int, int> degreeFromIndex = new Dictionary<int, int>() { { 0, -120 }, { 1, -60 }, { 2, 0 }, { 3, 60 }, { 4, 120 } } ;
    int[] dirarray = new int[5] { -120, -60, 0, 60, 120 };

    void Start()
    {
        //playerSpawner = gameObject.GetComponent<PlayerSpawner>();
        playpos = playerSpawner.playerPosition;
        playdir = playerSpawner.playerDirection;
        //MainFunction(playpos, playdir,3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void CheckForRays()
    {
        //Spawning the player
        //playerSpawner.SpawnPlayer();
        noHitRayIndex = new List<int>();
        locationFromCenter = new List<Vector3>();
        rotationFromPlayer = new List<int>();


        center = GetCenterFromPlayerPosition();

        //Debug.Log("The playpos is " + playpos);

        float raylen = pathlen + 3  * Mathf.Sqrt(3)* pathwid / 2 ;
        int[] dirarray = new int[5] { -120, -60, 0, 60, 120 };

        for (int i = 0; i < 5; i++)
        {
            Vector3 dir = center - playpos; // This vector stores the direction of center from the player
           // Debug.Log("The direction of center of hexagon from spawning position is " + dir + "at count " + i);
            // This then rotates the vector dir to the respective rotation
            dir = Quaternion.Euler(0, dirarray[i], 0) * dir;

            //Debug.Log("Checking for direction at count  " + i + "  and at direction" + dir);
            if (Physics.Raycast(center, dir, raylen))
                Debug.Log("It hits. It hits at count" + i);
            else
            {
                locationFromCenter.Add(dir);
                rotationFromPlayer.Add(dirarray[i]);
                noHitRayIndex.Add(i);
            }
        }
    }
    */
    Vector3 GetCenterFromPlayerPosition(Vector3 playerPosition, Vector3 playerDirection)
    {
        /*
        //This Function takes as parameter the position of Player, the direction of player and returns coordinates of center of Hexagon.
        float hexlen = pathwid * Mathf.Sqrt(3) / 2;
        //Debug.Log("The hexlen is  " + hexlen);

        float xcomp = playpos.x + hexlen * Mathf.Sin(Mathf.PI * playdir.y / 180);
        float zcomp = playpos.z + hexlen * Mathf.Cos(Mathf.PI * playdir.y / 180);
        Vector3 center = new Vector3(xcomp, 0, zcomp);
        //Debug.Log("The center is " + center);
        return center;
        */
        float hexlen = pathwid * Mathf.Sqrt(3) / 2;
        float xcomp = playerPosition.x + hexlen * Mathf.Sin(Mathf.PI * playerDirection.y / 180);
        float zcomp = playerPosition.z + hexlen * Mathf.Cos(Mathf.PI * playerDirection.y / 180);
        Vector3 center = new Vector3(xcomp, 0, zcomp);
        return center;
    }

    Vector3 GetPlayerPositionFromCenter(Vector3 playerPosition, Vector3 playerDirection, int degree)
    {
        //This Function takes as parameters coordinates of the center, direction of the wallSection and returns new Player Position
        Vector3 center = GetCenterFromPlayerPosition(playerPosition, playerDirection);
        float newlen = pathlen + Mathf.Sqrt(3) * pathwid / 2;
        Vector3 additional = Quaternion.Euler(0, playerDirection.y + degree, 0) * Vector3.forward * newlen;
        return center + additional;

    }

    List<int> CheckHittingRays(Vector3 playerPosition,Vector3 playerDirection)
    {
        //This function takes coordinates of center and direction of player as input, checks for rays in five directions and returns indexes of the rays not hitting
        Vector3 center = GetCenterFromPlayerPosition(playerPosition, playerDirection);
        float raylen = pathlen + 3 * Mathf.Sqrt(3) * pathwid / 2;
        
        Vector3 dir = center - playerPosition;
        List<int> indexofRays = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            dir = Quaternion.Euler(0, dirarray[i], 0) * dir;
            if (Physics.Raycast(center, dir, raylen))
            {
                Debug.Log("It hits. It hits at count" + i);
                //indexofRays[i] = 0;
            }
            else
            {
                Debug.Log("it does not hit at count " + i);
                indexofRays.Add(i);
            }
        }
        return indexofRays;
    }


    void MainFunction(Vector3 playerPosition, Vector3 playerDirection,int raysToCheck)
    {
        //This function will be running everything
        /*
         This will take as input the position and direction of player.
        Then it will call GetCenterFromPlayerPosition and calculate the coordinates of center.
        Then it will call CheckHittingRays and get a list/array of indices of rays not hitting
            looping on GetPlayerPositionFromCenter for getting an updated playerPosition and Direction for all the rays not hitting
            GetCenterFromPlayerPosition to get new centers for all rays not hitting
            for each Center we run CheckHittingRays and get indices of secondDegreeRays
                looping on GetPlayerPositionFromCenter for getting an updated playerPosition and Direction for all the rays not hitting
                GetCenterFromPlayerPosition to get new centers for all rays not hitting
                for each Center we run CheckHittingRays and get indices of thirdDegreeRays

                if the third degree rays are not hitting we make a final list of rays not hitting, From this final list we choose one ray randomly.
                For this ray we have already checked directions upto second degree so from the second degree list of directions we check again for raysnot hitting.
         */
        List<int> firstDegreeRays = CheckHittingRays(playerPosition, playerDirection);
        Debug.Log("First Degree Rays has been called ");
        List<List<int>> secondDegreeRay = new List<List<int>>();
        ArrayList thirdDegreeRay = new ArrayList();

        foreach(int index in firstDegreeRays)
        {
            Debug.Log("Checking for second degree when first degree was " + index);
            Vector3 newPos = GetPlayerPositionFromCenter(playerPosition, playerDirection, dirarray[index]);
            Vector3 newDir = new Vector3(0, playerDirection.y + dirarray[index], 0);
            secondDegreeRay.Add(CheckHittingRays(newPos, newDir));
        }
        //foreach(List<int> second in secondDegreeRay)
        for(int i = 0; i < secondDegreeRay.Count;i++)
        {
            foreach(int first in secondDegreeRay[i])
            {
                Debug.Log("Checking for third degree when second degree was " + i  + " And first degree was " + first);
                Vector3 newPos = GetPlayerPositionFromCenter(playerPosition, playerDirection, dirarray[first]);
                Vector3 newDir = new Vector3(0, playerDirection.y + dirarray[first], 0);
                thirdDegreeRay.Add(CheckHittingRays(newPos, newDir));
            }
        }
    }



}
