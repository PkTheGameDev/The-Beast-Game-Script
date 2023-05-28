using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mutant_Controller : MonoBehaviour
{
    GameObject MPlayer;
    public Transform Mutant_Transform, PlayerTransform, AttackPt;
    public Animator Mutant_Anim;
    public Rigidbody M_Rb;
    public AudioSource M_AudioSc;
    public float ViewDistance = 20, AttackRange = 2, RoamingDist = 7f, SRadius = 0.5f, EDamage = 10;
    int NearReached = 1, CurrentIndex = 0;
    public float Speed = 2;
    public float MaxHealth = 120, CurrentHealth;
    public Slider HealthSlider;
    public LayerMask PlayerLayer, Ground;
    public Player_Controller plController;
    public GameManager gamemanager;
    public Transform[] HealthPickups;
    public ParticleSystem BloodFx;
    public PatrolPath PatrolPts { get; set; }

    [HideInInspector] public Vector3 PlayerPos;
    [HideInInspector] public bool PlayerSpotted;
    bool IsGrounded;
    public bool isDead = false;

    Mutant_Motions MutantMotion;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();        
    }

    private void OnEnable()
    {
        Initialization();
    }

    void Initialization()
    {
        if (PlayerTransform == null)
        {
            MPlayer = GameObject.FindGameObjectWithTag("Player");
            PlayerTransform = MPlayer.transform;
            plController = MPlayer.GetComponent<Player_Controller>();
        }

        CurrentHealth = MaxHealth;
        HealthSlider.value = CurrentHealth;

        MutantMotion = Mutant_Anim.GetBehaviour<Mutant_Motions>();

        MutantMotion.MutantControls = this;
        MutantMotion.MutantTransform = Mutant_Transform;
        MutantMotion.PlayerTransform = PlayerTransform;

        BloodFx.Stop();

        M_AudioSc = GetComponent<AudioSource>();

        int j = HealthPickups.Length;
        for (int i = 0; i < j; i++)
        {
            HealthPickups[i].gameObject.SetActive(false);
        }

        //set Current index of patrol path to the nearest node
        CurrentIndex = NearNode();
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHealth > 0)
        {
            PlayerSpotted = PlayerSpotting();
            IsGrounded = 0 < Physics.OverlapSphere(Mutant_Transform.position, 0.2f, Ground).Length;
        }
        else
        {
            HealthSlider.value = 0;
            Mutant_Anim.SetBool("Die", true);
        }       
    }

    bool PlayerSpotting()
    {
        PlayerPos = Mutant_Transform.InverseTransformPoint(PlayerTransform.position);

        return (PlayerPos.z > 0 && (Mathf.Abs(PlayerPos.x) < ViewDistance) && (PlayerPos.z < ViewDistance));
    }

    //Moving Enemy and giving it animation
    public void Motion(float SpeedMultiplier)
    {
        Mutant_Anim.SetFloat("Forward", SpeedMultiplier);

        Mutant_Transform.Translate(0, 0, SpeedMultiplier * Speed * Time.deltaTime);
    }

    //Enemy Taking Damage from Player
    public void TakeDamage(float Damage)
    {
        if(CurrentHealth > 0)
        {
            CurrentHealth -= Damage;
            HealthSlider.value = CurrentHealth;
            Mutant_Anim.SetTrigger("GotHit");
            BloodFx.Play();
            Mutant_Transform.LookAt(PlayerTransform);
        }
    }

    //Enemy Doing Damage to Player
    public void EnemyDealDamage(float MDamage)
    {
        Collider[] Player = Physics.OverlapSphere(AttackPt.position, SRadius, PlayerLayer);

        int j = Player.Length;
        if (j > 0)
        {
            plController.TakeDamage(MDamage);
        }       
    }

    public void DyingSound()
    {
        var Sounds = this.GetComponent<AudioManager>();
        Sounds.DeathSound();
    }

    //After Enemy Dies Spawn Health pack 
    public void DieAnimationEvent()
    {
        var HealthPick = HealthPickups[Random.Range(0, 3)];
        Instantiate(HealthPick, HealthPick.position, Quaternion.identity).transform.gameObject.SetActive(true);

        gameObject.SetActive(false);
        if(gameObject.tag == "Patrol enemy")
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        if(gamemanager == null)
        {
            gamemanager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        }
        if (this.gameObject.tag == "Enemy 1")
        {
            gamemanager.EnqueToQue(gameObject, tag);
            Debug.Log("Added to Queue");
        }
        
        isDead = true;
    }

    //Checks Whether Patrol Path is Assigned and Valid
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
            if (NearReached > Vector3.Distance(PatrolPts.PathsNodes[CurrentIndex].position, Mutant_Transform.position))
            {
                if (PathLength > CurrentIndex)
                {
                    CurrentIndex++;
                }
                else
                {
                    CurrentIndex = 0;
                }
            }
        }
        return PatrolPts.PathsNodes[CurrentIndex].position;
    }

    /*Calculates the nearest node distance from enemy position, compare the distance of origion node and 
      nearest node and moves towards it */
    public int NearNode()
    {
        int ClosestNode = 0;
        if (IsPathValid())
        {
            int PathlengtH = PatrolPts.PathsNodes.Length;
            for (int i = 0; i < PathlengtH; i++)
            {
                float CurrentDist = Vector3.Distance(PatrolPts.PathsNodes[i].position, Mutant_Transform.position);
                if (CurrentDist < Vector3.Distance(PatrolPts.PathsNodes[ClosestNode].position, Mutant_Transform.position))
                {
                    ClosestNode = i;
                }
            }
        }

        return ClosestNode;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(AttackPt.position, SRadius);
    }
}
