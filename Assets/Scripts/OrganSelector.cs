using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Outline))]
public class OrganSelector : XRBaseInteractable
{ 
    AudioSource _audioSource = null;
    Outline _outline = null;
    [SerializeField] private bool Toggled = false;
    [Header("Organ Information")]
    [SerializeField] private OrganInfo _OrganInfo;
    
    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        _audioSource.spatialBlend = 1f;
        
        _outline = gameObject.GetComponent<Outline>();
        _outline.enabled = false;

        var clip = _OrganInfo.OrganName.ToLower().Replace(" ", "-");
        _audioSource.clip = Resources.Load<AudioClip>("Explanations/" + clip);
    }


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        ToggleSelection();
    }

    public void ToggleSelection()
    {
        Toggled = !Toggled;
        if (Toggled)
        {
            _outline.enabled = true;
            _outline.OutlineColor = GameManager.instance._toggleColor;
        }
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        if (!Toggled)
        {
            _outline.enabled = true;
            _outline.OutlineColor = GameManager.instance._hoverColor;
        }
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        if (!Toggled)
        {
            _outline.enabled = false;
        }
    }


    void Update()
    {
        
    }
}

[Serializable]
public class OrganInfo
{
    public string OrganName;
    public string OrganDescription;
}
