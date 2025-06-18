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
        if (GameManager.instance.IsQuiz)
        {
            GameManager.instance._quizpanel.SubmitAnswer(_OrganInfo.OrganName);
            return;
        }
        if(GameManager.instance._organSelector == null) ToggleSelection();
    }


    public void ToggleSelection()
    {
        Toggled = !Toggled;
        
        if (Toggled)
        {
            _outline.enabled = true;
            _outline.OutlineColor = GameManager.instance._toggleColor;
            _audioSource.Play();
            
            GameManager.instance.ShowInformation(_OrganInfo, this);
        }
        else
        {
            GameManager.instance.HideInformation();
        }
    }

    public void StopSelection()
    {
        Toggled = false;
        _outline.enabled = false;
        _audioSource.Stop();
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
