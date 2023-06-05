using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGlowButton : MonoBehaviour {
    private Material material;
    private float glowIntensity = 0.5f;

    void Start() {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        material.SetFloat("_GlowIntensity", glowIntensity);
    }

    void Update() {
        glowIntensity = Mathf.PingPong(Time.time, 1);
       
        material.SetFloat("_GlowIntensity", glowIntensity);
        Debug.Log(glowIntensity);
    }
}
