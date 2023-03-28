using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SetTransparency : MonoBehaviour
{
     
    public float defaultTransparency = 0f;
    private float fadeDuration = 1000f;
    public float laterTransparency = 1f;
     
    float currentTransparency;  
    float toFadeTo;
    float tempDist;
    bool isFadingUp;
    bool isFadingDown;
    private float delayer = 10f;
    private int myTime;
     
     
     
    void Start()
    {
        currentTransparency = defaultTransparency;
        ApplyTransparency();
        myTime = PlayerPrefs.GetInt("MyInt");
        fadeDuration = (float)myTime;
        Invoke("FadeT", delayer);
        //FadeT(laterTransparency);
    }
     
    void FixedUpdate(){
        if(isFadingUp){
            if(currentTransparency < toFadeTo){
                currentTransparency += (tempDist/fadeDuration) * Time.deltaTime;
                ApplyTransparency();
            }else{
                isFadingUp = false;
            }
        }
        else if(isFadingDown){
            if(currentTransparency > toFadeTo){
                currentTransparency -= (tempDist/fadeDuration) * Time.deltaTime;
                ApplyTransparency();
            }else{
                isFadingDown = false;
            }
        }
    }
     
    void ApplyTransparency(){
        GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, currentTransparency);
    }
 
 
 
    /*public void SetT(float newT){
        currentTransparency = newT;
        ApplyTransparency();
    }*/
     
    //public void FadeT(float newT){
    public void FadeT(){
        toFadeTo = laterTransparency;
        if(currentTransparency < toFadeTo){
            tempDist = toFadeTo - currentTransparency;
            isFadingUp = true;
        }else{
            tempDist = currentTransparency - toFadeTo;
            isFadingDown = true;
        }
    }
     
     
}
