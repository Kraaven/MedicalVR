using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] public Color _hoverColor = Color.white;
    [SerializeField] public Color _toggleColor = Color.white;
    [SerializeField] public infopanel _infopanel;
    [SerializeField] public quizpanel _quizpanel;
    [SerializeField] public Transform _model;
    public OrganSelector _organSelector;
    
    public static GameManager instance;

    public bool IsQuiz;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _infopanel.gameObject.SetActive(false);
        _quizpanel.gameObject.SetActive(false);
    }

    public void ShowInformation(OrganInfo organInfo, OrganSelector organSelector)
    {
        _infopanel.gameObject.SetActive(true);
        _infopanel.ShowInfo(organInfo);
        _organSelector = organSelector;
    }

    public void HideInformation()
    {
        _infopanel.gameObject.SetActive(false);
        _organSelector.StopSelection();
        _organSelector = null;
    }

    public void Exit()
    {
        if (IsQuiz)
        {
            IsQuiz = false;
        }
        else
        {
            Application.Quit();
        }
    }

    public void StartQuiz()
    {
        if(IsQuiz) return;
        IsQuiz = true;
        _quizpanel.gameObject.SetActive(true);
        _quizpanel.StartQuiz();
    }

    public void EndQuiz()
    {
        if (!IsQuiz) return;
        IsQuiz = false;
        _quizpanel.gameObject.SetActive(false);
    }

    public void Rotate()
    {
        _model.Rotate(new Vector3(0, 0, 45));
    }


}
