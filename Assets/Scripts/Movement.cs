using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{   
    AudioSource audioSource;
    Rigidbody rigidbody;
    float speed = 1000f;
    float rotationspeed = 300f;
    [SerializeField] AudioClip mator;

    
    [SerializeField] ParticleSystem thrustParticles;

    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;


   
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

            }

    // Update is called once per frame
 
    void Update()
    {
        Fly();
        Rotate();  
    }

    void Fly(){

       
     if(Input.GetKey(KeyCode.Space)){
                 rigidbody.AddRelativeForce(Vector3.up*speed*Time.deltaTime);
            if(!audioSource.isPlaying){
                audioSource.PlayOneShot(mator);
                   
            
        if(!thrustParticles.isPlaying){
            thrustParticles.Play();
        }
        }
     }
        else{
                audioSource.Stop();
                thrustParticles.Stop();
            }
         
        
    }
    void Rotate(){

        if(Input.GetKey(KeyCode.A)){
           rotateHere(rotationspeed);
           if(!rightThrustParticles.isPlaying)
           {
           rightThrustParticles.Play();
           }
             
        }
       else if(Input.GetKey(KeyCode.D)){
           rotateHere(-rotationspeed);
            if(!leftThrustParticles.isPlaying){

            leftThrustParticles.Play();
            }
          
      
      
    }
     else {
              leftThrustParticles.Stop();
              rightThrustParticles.Stop();
       }
      void rotateHere(float rotationspeed){
        rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationspeed * Time.deltaTime);
        rigidbody.freezeRotation = false;   
    }
    
  
}
}
