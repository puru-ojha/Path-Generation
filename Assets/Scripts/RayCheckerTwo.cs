using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCheckerTwo : MonoBehaviour
{
    // Start is called before the first frame update
  
    public Vector3 playerPosition;
    public Vector3 playerDirection;
    int wallLength = 0; // Initial wall length was 20. Taken as zero so that no walls are being spawned
    //int wallLength = 20;
    int wallWidth = 8;
    int[] degreeIndex = new int[5] { -120, -60, 0, 60, 120 };
    Vector3 currentPlayerPosition;
    Vector3 currentPlayerDirection;
    public Vector3 center;
    public float hexLength;
    public float raylength;
    
    public int indexChosen;
    public int rotationChosen;
    void Start()
    {
        playerPosition = PlayerSpawner.Instance.playerPosition;
        playerDirection = PlayerSpawner.Instance.playerDirection;
        hexLength = wallWidth * Mathf.Sqrt(3) / 2;
        raylength = wallLength + 3 * hexLength;
        Debug.Log("The raylenmght is " + raylength);
        currentPlayerPosition = playerPosition;
        currentPlayerDirection = playerDirection;
        center = playerPosition + Quaternion.Euler(playerDirection) * Vector3.forward * hexLength;
        //mainFunction();
    }
    bool rayCastWrap(int index)
    {
        //PlayerDirection is rotation around y axis so center will be playerPosition + quaternion.eular(playerDirection)*vector3.forward*hexLength
        Vector3 currentCenter = currentPlayerPosition + Quaternion.Euler(currentPlayerDirection) * Vector3.forward * hexLength;
        Vector3 direction = Quaternion.Euler(new Vector3(0, currentPlayerDirection.y + degreeIndex[index], 0)) * Vector3.forward;
        Debug.DrawRay (currentCenter, direction* raylength);
        return !Physics.Raycast(currentCenter, direction,raylength);
    }

    public void mainFunction()
    {
        Node root = new Node(00);
        //List<Node> firstDegree = new List<Node>();
        List<Node> secondDegree = new List<Node>();
        List<Node> thirdDegree = new List<Node>();
        //These loops are to check for ray casts
        for (int i = 0; i<5 ; i++)
        {
            currentPlayerPosition = playerPosition;
            currentPlayerDirection = playerDirection;
            //Debug.Log("Checking for the first degree");
            //Debug.Log("In the first loop The current player position is " + currentPlayerPosition + " The current direction is " + currentPlayerDirection);
            if (rayCastWrap(i))
            {
                Debug.Log("First degree hit on " + i);
                Node first = new Node(i);
                //firstDegree.Add(first);
                root.add_child(first);
                
                for (int j = 0; j<5; j++)
                {
                    currentPlayerPosition = center + Quaternion.Euler(new Vector3(0, playerDirection.y + degreeIndex[i], 0)) * Vector3.forward * (wallLength +  hexLength);
                    currentPlayerDirection = playerDirection + Vector3.up * degreeIndex[i];
                    //Debug.Log("Checking for the second degree");
                    //Debug.Log("The current player position is " + currentPlayerPosition + " The current direction is " + currentPlayerDirection);
                    
                    if (rayCastWrap(j))
                    {

                        Debug.Log("For the first degree "+i+ " seond degree hit on " + j);
                        Node second = new Node(j);
                        secondDegree.Add(second);
                        first.add_child(second);
                        currentPlayerPosition = center + Quaternion.Euler(new Vector3(0, playerDirection.y + degreeIndex[j] + degreeIndex[i], 0)) * Vector3.forward * (wallLength +  hexLength);
                        currentPlayerDirection = playerDirection + Vector3.up * (degreeIndex[j] + degreeIndex[i]);
                        //Debug.Log(" In the third loop The current player position is " + currentPlayerPosition + " The current direction is " + currentPlayerDirection);
                        for (int k = 0; k < 5; k++)
                        {
                            //Debug.Log("Checking for the third degree");
                            if (rayCastWrap(k))
                            {
                                Debug.Log("For the first degree " + i + " seond degree hit on " + j+ " Third degree hit on " + k); 
                                Node third = new Node(k);
                                thirdDegree.Add(third);
                                second.add_child(third);
                            }
                        }
                    }
                }
            }
        }
        Debug.Log("Checking loop completed");
        //root.Print_tree();
        //Node chosenFirst;
        List<int> indexChosenList = new List<int>();
        if (thirdDegree.Count == 0)
        {
            Debug.Log("There were no more third children of this chosen node so we take second children and hope it works");
            foreach (Node element in secondDegree)
            {
                indexChosenList.Add(element.parent.data);
            }
        }
        else
        {
            foreach(Node element in thirdDegree)
            {
                indexChosenList.Add(element.parent.parent.data);
            }
            //Node chosenThird = thirdDegree[Random.Range(0, thirdDegree.Count)];
            //chosenFirst = chosenThird.parent.parent;
        }
        //indexChosen = chosenFirst.data;
        if (indexChosenList.Contains(2))
        {
            indexChosen = 2;
        }
        else
        {
            indexChosen = indexChosenList[Random.Range(0, indexChosenList.Count)];
            rotationChosen = degreeIndex[indexChosen];
        }
        //root.parent_jump(indexChosen);
        Debug.Log("The chosen index is " + indexChosen);

    }
    public void MoveForward()
    {
        playerPosition = center + Quaternion.Euler(new Vector3(0, playerDirection.y + degreeIndex[indexChosen], 0)) *Vector3.forward*(hexLength+wallLength);
        playerDirection.y = playerDirection.y + degreeIndex[indexChosen];
        center = playerPosition + Quaternion.Euler(playerDirection) * Vector3.forward * hexLength;
    }
}