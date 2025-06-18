using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] public Color _hoverColor = Color.white;
    [SerializeField] public Color _toggleColor = Color.white;
    
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
