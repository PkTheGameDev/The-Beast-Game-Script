using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Projectile : MonoBehaviour
{
    //Reference to Projectile Collider GameObject
    public GameObject ColliderObj;

    //Particle system to play on Collision
    public ParticleSystem ParticleonCollision;

    //LayerMask to Detect which layers are affected
    public LayerMask AllLayers = Physics.AllLayers;

    //Force of the projectile
    public float Force = 200f;

    //Damage to player
    public float PlDamage = 15f;

    //Components to Destroy on Collision
    public ParticleSystem[] DestroyOncollision;

    //Rigidbody of Projectile
    public Rigidbody FireballRb;

    //Playyer Controller to Assign Damage to Player
    Player_Controller plcontroller;
    //bool to check whether collision happened
    bool IsCollided;

    private void Awake()
    {
        //Gets Rigid body of projectile and stops particles on collision from playing
        FireballRb = GetComponent<Rigidbody>();
        ParticleonCollision.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {        
        //Adds Impulse Force to Projectile when Instantiated 
        FireballRb.AddForce(ColliderObj.transform.forward * Force, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision collision)
    {
        //When collision happens Runs the Below Methos and destroys Game object
        FireBallCollision(collision);
        Destroy(gameObject, 0.25f);
    }

    public void FireBallCollision(Collision col)
    {
        if (IsCollided)
        {
            return;
        }
        else
        {
            Destroy(gameObject, 6f);
            Debug.Log(gameObject.name);
        }

        IsCollided = true;

        //checks whether Destroy on collision has partilces and destroys it first
        if (DestroyOncollision != null)
        {
            foreach(ParticleSystem p in DestroyOncollision)
            {
                Destroy(p, 0.02f);
            }                        
        }

        //Position of Particle Collider
        Vector3 pos = ColliderObj.transform.position;
        if (col.contacts.Length > 0)
        {
            //On Collision checks with Raycast and Applies force to all rigid bodies 
            Collider[] collider = Physics.OverlapSphere(pos, .3f, AllLayers);
            int j = collider.Length;
            foreach (Collider c in collider)
            {
                Rigidbody r = c.GetComponent<Rigidbody>();

                //If Rigidbody is not null applies explosive Force
                if (r != null)
                {
                    ParticleonCollision.Play();
                    r.AddExplosionForce(100f, pos, 2f);

                    //Applies Damage to player
                    if (c.TryGetComponent<Player_Controller>(out plcontroller))
                    {
                        plcontroller.TakeDamage(PlDamage);
                    }
                }
            }
        }
    }
}
