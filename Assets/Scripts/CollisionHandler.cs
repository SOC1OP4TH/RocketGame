using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{ 
    
    AudioSource AudioSource;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    bool collisonDisabled = false;
    bool isTransitioning = false;
   void Start(){
    AudioSource = GetComponent<AudioSource>();
   }
    void Update()
    {
            RespondToDebugKeys();  
    }
    void RespondToDebugKeys(){
        if(Input.GetKeyDown(KeyCode.L)){
            StartSuccessScene();
        }
        else if(Input.GetKeyDown(KeyCode.C)){
            collisonDisabled = !collisonDisabled;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
       if(isTransitioning || collisonDisabled){return;}
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is friendly");
                break;
            case "Finish":
                StartSuccessScene();
                Debug.Log("You finished the level");
                break;
            case "Fuel":
                Debug.Log("You got fuel");
                break;
            default:
                Debug.Log("You died");
              StartCrashSequence();
           
                break;
        }
    }
     
    void ReloadLevel(){
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   void StartCrashSequence(){
    isTransitioning = true;
    crashParticles.Play();
    AudioSource.Stop();
    AudioSource.PlayOneShot(crash);
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", 4f);
       
    }
    void StartSuccessScene(){
    isTransitioning = true;
    successParticles.Play();
    AudioSource.Stop();
    AudioSource.PlayOneShot(success);
    GetComponent<Movement>().enabled = false;
    Invoke("NextLevel", 4f);
        
    }
    void NextLevel(){   
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       if(currentSceneIndex == SceneManager.sceneCountInBuildSettings-1){
            SceneManager.LoadScene(0);
        }else{
            SceneManager.LoadScene(currentSceneIndex + 1);  
       }
    }
}
