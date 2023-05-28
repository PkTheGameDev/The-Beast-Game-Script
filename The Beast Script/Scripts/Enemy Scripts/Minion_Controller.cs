using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minion_Controller : MonoBehaviour
{
    GameObject MPlayer;
    public Transform plTransform, MinTransform, CAttackPt, FireballSpt;
    public float Speed = 2, MaxHealth = 80, CurrentHealth, SpRadius = 2f, ViewDist = 15f, ShootDist = 8f, CAttackR = 2f;
    int NearReached = 1, CurrentIndex = 0;
    public Animator MinAnim;
    public Slider HealthSlider;
    [HideInInspector] public bool PlayerSpotted;
    [HideInInspector] public Vector3 PlayerPos;
    public LayerMask PlayerLayer;
    public Player_Controller plController;
    public GameManager gamemanager;
    public Transform[] HealthPickups;
    public GameObject FireBall;
    public ParticleSystem BloodFx;
    public PatrolPath PatrolPoint { get; set; }
    Minion_Motions MiniMotions;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

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
        if(plTransform == null)
        {
            MPlayer = GameObject.FindGameObjectWithTag("Player");
            plTransform = MPlayer.transform;
            plController = MPlayer.GetComponent<Player_Controller>();
        }

        CurrentHealth = MaxHealth;
        HealthSlider.value = MaxHealth;

        BloodFx.Stop();

        MiniMotions = MinAnim.GetBehaviour<Minion_Motions>();

        MiniMotions.MiniTransform = MinTransform;
        MiniMotions.PLTransform = plTransform;
        MiniMotions.minionController = this;

        int j = HealthPickups.Length;

        for (int i = 0; i < j; i++)
        {
            HealthPickups[i].gameObject.SetActive(false);
        }

        //set Current index of patrol path to the nearest node
        CurrentIndex = NearbyNode();
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHealth > 0)
        {
            PlayerSpotted = PlayerSpot();
        }
        else
        {
            MinAnim.SetBool("Die", true);
            HealthSlider.value = 0;
        }

    }

    bool PlayerSpot()
    {
        PlayerPos = MinTransform.InverseTransformPoint(plTransform.position);

        return (PlayerPos.z > 0 && Mathf.Abs(PlayerPos.x) < ViewDist && PlayerPos.z < ViewDist);
    }

    public void Motion(float SpeedMultiplier)
    {
        MinAnim.SetFloat("State", SpeedMultiplier);
        MinTransform.Translate(0, 0, SpeedMultiplier * Speed * Time.deltaTime);        
    }

    public void TakeDamage(float Damage)
    {
        if(CurrentHealth > 0)
        {
            CurrentHealth -= Damage;
            HealthSlider.value = CurrentHealth;
            MinTransform.LookAt(plTransform);
            if(CurrentHealth > 25)
            {
                MinAnim.SetTrigger("Hit React");
            }
            BloodFx.Play();
        }        
    }

    public void MiniDealDamage(float DAmage)
    {
        Collider[] Player = Physics.OverlapSphere(CAttackPt.position, SpRadius, PlayerLayer);

        int j = Player.Length;
        if(j > 0)
        {
            plController.TakeDamage(DAmage);
        }
        
    }

    //After Enemy Dies Spaawn Health pack
    void OnDieAnimEvent()
    {
        var HealthPick = HealthPickups[Random.Range(0, 3)];

        Instantiate(HealthPick, HealthPick.position, Quaternion.identity).transform.gameObject.SetActive(true);

        gameObject.SetActive(false);
        if (gameObject.tag == "Patrol enemy")
        {
            Destroy(gameObject);
        }
    }

    public void FireballSpawn()
    {
        Instantiate(FireBall, FireballSpt.transform.position, FireballSpt.transform.rotation);
    }

    private void OnDisable()
    {
        if (gamemanager == null)
        {
            gamemanager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();            
        }
        if (this.gameObject.tag == "Enemy 2")
        {
            gamemanager.EnqueToQue(gameObject, tag);
        }
    }

    //Checks Whether Patrol Path is Assigned and Valid
    public bool IsPathValid()
    {
        return PatrolPoint && PatrolPoint.PathsNodes.Length > 0;
    }

    //Gets next node position and adds the Current index after reaching each positon and gets the next position
    public Vector3 NextNodePosition()
    {
        int PathLength = PatrolPoint.PathsNodes.Length - 1;

        if(IsPathValid())
        {
            if(NearReached > Vector3.Distance(PatrolPoint.PathsNodes[CurrentIndex].position, MinTransform.position))
            {
                if(PathLength > CurrentIndex)
                {
                    CurrentIndex++;
                }
                else
                {
                    CurrentIndex = 0;
                }
            }
        }
        return PatrolPoint.PathsNodes[CurrentIndex].position;
    }

    /*Calculates the nearest node distance from enemy position, compare the distance of origion node and 
      nearest node and moves towards it */
    public int NearbyNode()
    {
        int ClosestNode = 0;
        if (IsPathValid())
        {
            int PathlengtH = PatrolPoint.PathsNodes.Length;
            for (int i = 0; i < PathlengtH; i++)
            {
                float CurrentDist = Vector3.Distance(PatrolPoint.PathsNodes[i].position, MinTransform.position);
                if (CurrentDist < Vector3.Distance(PatrolPoint.PathsNodes[ClosestNode].position, MinTransform.position))
                {
                    ClosestNode = i;
                }
            }
        }

        return ClosestNode;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(CAttackPt.position, SpRadius);
    }
}
