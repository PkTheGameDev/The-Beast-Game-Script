using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_CheckPl : MonoBehaviour
{
    public Transform PlTransform, NPCTransform;
    public Player_Controller PlayerCtl;
    public LayerMask PlayerLayer, Enemy;
    public Transform GizmoTr;
    public GameObject Prompt, CastleBlock;
    public ParticleSystem FireWall, SmokeEffect;
    public Animator NpcAnim;
    int NearReached = 1, CurrentIndex = 0;
    public float Speed = 2, Gizmosize = 1f;
    public PatrolPath PatrolPts { get; set; }

    private void Start()
    {
        Prompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Gets Dialog Component for NPC
        var Dialog = GetComponent<Dialogue>();

        //With Raycast Checks for player
        Collider[] Player = Physics.OverlapSphere(GizmoTr.position, Gizmosize, PlayerLayer);

        //Default Animation for Npc
        NpcAnim.SetFloat("Blend", 0);
        int j = Player.Length;
        if(j > 0)
        { 
            //if Player is in Range Button Prompt is set active
            Prompt.SetActive(true);
            
            //Checking Npc GameObject with Name and when player presses input opens Dialog panel
            if (this.gameObject.name == "Kachujin G Rosales")
            {
                NPCTransform.LookAt(PlTransform.position);
                NpcAnim.SetFloat("Blend", 1);
                if (Dialog.enabled)
                {
                    //when Dialog panel is enabled in NPC Button prompt is set false
                    Prompt.SetActive(false);
                    NpcAnim.SetFloat("Blend", 1);
                }
            }

            if (this.gameObject.name == "Mimo Prefab")
            {
                NPCTransform.LookAt(PlTransform.position);
                NpcAnim.SetFloat("Blend", 1);
                if (Dialog.enabled)
                {
                    Prompt.SetActive(false);
                }
            }

            //Checks NPC Wizard
            if (this.gameObject.name == "Wizard")
            {
                //sets default Animation state if dialog component is not enabled
                if (!Dialog.enabled)
                {
                    NPCTransform.LookAt(PlTransform);
                    NpcAnim.SetFloat("Blend", 0);
                    
                }
                /*if dialog panel is enabled and dialog is completed - set prompt to inactive
                 Checks Patrol point and moves to the node position */
                if(Dialog.enabled && !Dialog.DialogPanel.activeSelf)
                {
                    Prompt.SetActive(false);
                    if (CurrentIndex < PatrolPts.PathsNodes.Length)
                    {
                        Motion(1);
                        NPCTransform.LookAt(NextNodePosition());
                    }

                    //When Final place is Reached look at the specified obj and do an Animation
                    if (CurrentIndex == PatrolPts.PathsNodes.Length)
                    {
                        Motion(0);
                        NPCTransform.LookAt(CastleBlock.transform);
                        NpcAnim.SetBool("Attack", true);
                    }
                    else
                    {
                        NpcAnim.SetBool("Attack", false);
                    }

                    //when the object is destroyed look at player or another positon and do an animation and disappear
                    if (!CastleBlock.activeSelf)
                    {
                        NPCTransform.LookAt(Vector3.zero);
                        NpcAnim.SetBool("Attack", false);
                        NpcAnim.SetTrigger("castSpell");
                    }
                }                              
            }            

            //other NPC with saying dialog
            if(this.gameObject.name == "Wizard M2" || this.gameObject.name == "Wizard M3")
            {
                NPCTransform.LookAt(PlTransform.position);
                NpcAnim.SetFloat("Blend", 0);
                if (Dialog.enabled)
                {
                    Prompt.SetActive(false);
                }
            }

            //Input to enable and open dialog panel
            if (Input.GetKeyDown(KeyCode.E))
            {
                Dialog.enabled = true; 
            }            
        }
        else
        {
            Prompt.SetActive(false);
        }
        //if the GameObjects Prompt is active it deactivates it
        if (this.gameObject.name == "Kachujin G Rosales")
        {
            var Dist = Vector3.Distance(NPCTransform.position, PlTransform.position);
            if(Dist > Gizmosize)
            {
                NPCTransform.LookAt(PlTransform);
            }
            if (PlayerCtl.quest.IsActive)
            {
                NpcAnim.SetFloat("Blend", 1);
            }
        }
    }

    //Applies Movement and Animation for NPC
    public void Motion(float SpeedMultiplier)
    {
        NpcAnim.SetFloat("Blend", SpeedMultiplier);

        transform.Translate(0, 0, SpeedMultiplier * Speed * Time.deltaTime);
    }

    //Wizard Anim Event to destroy castle block during quest
    public void FireEffectWiz()
    {
        FireWall.gameObject.SetActive(true);
    }

    public void OnWizardAttack()
    {
        StartCoroutine(DisableInSec());
    }

    //Make NPC Wizard Disappear during an animation event
    public void WizardDisappear()
    {
        SmokeEffect.gameObject.SetActive(true);
        Destroy(gameObject, 0.5f);
    }

    //Patrol Points for NPC
    public bool IsPathValid()
    {
        return PatrolPts && PatrolPts.PathsNodes.Length > 0;
    }

    //Gets next node position and adds the Current index after reaching each positon and gets the next position
    public Vector3 NextNodePosition()
    {
        int PathLength = PatrolPts.PathsNodes.Length - 1;

        if (IsPathValid())
        {            
            if (NearReached > Vector3.Distance(PatrolPts.PathsNodes[CurrentIndex].position, this.transform.position))
            {
                if (PathLength > CurrentIndex)
                {
                    CurrentIndex++;
                }
                else
                {
                    CurrentIndex = PatrolPts.PathsNodes.Length;
                    PatrolPts.NPC = null;
                }                
            }
        }
        return PatrolPts.PathsNodes[CurrentIndex].position;
    }
       
    //Disables the Castle Block during quest after few seconds
    IEnumerator DisableInSec()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("0.05f");
        CastleBlock.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GizmoTr.position, Gizmosize);
    }
}
