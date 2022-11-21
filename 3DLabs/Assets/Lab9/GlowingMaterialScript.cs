using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GlowingMaterialScript : MonoBehaviour
{

    [Tooltip("The material we are accessing (drag it here)")]
    [SerializeField] private Material material;
    [SerializeField] private Vector4 emissionColor;
    [SerializeField] private float emissionIntensity = 3f;
    private float emissionMin = 1f;

    private int emissiveID;
    // Start is called before the first frame update
    void Awake()
    {
        emissiveID = Shader.PropertyToID("_Emission");
    }

    // Update is called once per frame
    void Update()
    {
        material.SetVector(emissiveID, emissionColor *( (Mathf.Sin(Time.time) * ((emissionIntensity-emissionMin) /2f)) + (emissionMin - (-1*(emissionIntensity - emissionMin) / 2f))));
        
    }
}
