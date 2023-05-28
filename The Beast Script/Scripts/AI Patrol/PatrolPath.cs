using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    //Enemy Controller to be Assigned
    public List<Mutant_Controller> Mutant = new List<Mutant_Controller>();
    public List<Minion_Controller> Minion = new List<Minion_Controller>();
    public List<NPC_CheckPl> NPC = new List<NPC_CheckPl>();

    //Transform of each gameobjects placed on the scene
    public Transform[] PathsNodes;

    //Assign Patrol path to each controller that is added to the list
    private void Start()
    {
        foreach(var Mut in Mutant)
        {
            Mut.PatrolPts = this;
        }
        
        foreach(var Min in Minion)
        {
            Min.PatrolPoint = this;
        }

        foreach(var Wiz in NPC)
        {
            Wiz.PatrolPts = this;
        }
        
    }

    //Gizmo line to show the path connected together
    private void OnDrawGizmos()
    {
        int j = PathsNodes.Length;

        for(int i = 0; i < j; i++)
        {
            int NextNode = i + 1;
            if(NextNode >= j)
            {
                NextNode -= j;
            }
            Gizmos.DrawLine(PathsNodes[i].position, PathsNodes[NextNode].position);
        }
    }
}
