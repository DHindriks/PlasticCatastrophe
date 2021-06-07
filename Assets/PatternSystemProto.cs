using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSystemProto : MonoBehaviour
{
    //this list tracks the pattern being drawn
    public List<GameObject> selectedObjs;

    //this function adds the given gameobject to the selectedObjs list
    void AddToList(GameObject obj)
    {
        selectedObjs.Add(obj);
    }

    //this function will check which pattern has been drawn 
    void CheckPatterns()
    {
        //Nodes tracks which nodes have been selected
        bool[] Nodes = new bool[8];
        foreach (GameObject obj in selectedObjs)
        {
            //Each node object should have a component class called Node, which just contains a public int called ID
            //Nodes[obj.GetComponent<Node>().ID] = true;
        }
        //all nodes that have been selected can now be checked

        //if nodes with IDs 0, 5 and 8 have been selected
        if (Nodes[0] && Nodes[5] && Nodes[8])
        {
            //do a spell here
        }

        //if nodes 1, 5 and 7 have been selected
        if (Nodes[1] && Nodes[5] && Nodes[7])
        {
            //do another spell here
        }

        //resets all the nodes back to false, ready for next pattern
        for (int i = 0; i < Nodes.Length; i++)
        {
            Nodes[i] = false;
        }


    }
}
