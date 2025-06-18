using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class quizpanel : MonoBehaviour
{
    [SerializeField] private TMP_Text QuestionNumber;
    [SerializeField] private TMP_Text QuestionText;
    [SerializeField] private string AnswerOrgan;
    [SerializeField] private AudioSource QuizAudio;
    
    private AudioClip CorrectAnswer;
    private AudioClip IncorrectAnswer;
    private AudioClip End;

    private List<QuizQuestion> _answers = new List<QuizQuestion>();
    private int QuestionIndex = 0;

    private void OnEnable()
    {
        _answers = JsonConvert.DeserializeObject<List<QuizQuestion>>(Resources.Load<TextAsset>("Quiz/questions").text);
        
        CorrectAnswer = Resources.Load<AudioClip>("Quiz/correct");
        IncorrectAnswer = Resources.Load<AudioClip>("Quiz/wrong");
        End = Resources.Load<AudioClip>("Quiz/end");

        QuestionIndex = 0;
    }

    public void StartQuiz()
    {
        if (_answers == null || _answers.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        AskQuestion();
    }

    public void SubmitAnswer(string answer)
    {
        if (_answers[QuestionIndex].answer == answer)
        {
            QuizAudio.clip = CorrectAnswer;
            QuizAudio.Play();
            
            QuestionIndex++;
            AskQuestion();
        }
        else
        {
            QuizAudio.clip = IncorrectAnswer;
            QuizAudio.Play();
        }
    }

    public void AskQuestion()
    {
        if (QuestionIndex > _answers.Count)
        {
            QuizAudio.clip = End;
            QuizAudio.Play();
            
            GameManager.instance.EndQuiz();
        }
        else
        {
            QuestionNumber.text = $"Question {QuestionIndex+1}";
            QuestionText.text = _answers[QuestionIndex].question;
        }
    }
    
    
}

[Serializable]
public class QuizQuestion
{
    public string question;
    public string answer;
}