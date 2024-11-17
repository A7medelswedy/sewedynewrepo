using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for QuizPage.xaml
    /// </summary>
    public partial class QuizPage : Window
    {
        private List<string> questions; 
        private List<List<string>> answerOptions; 
        private List<int> correctAnswers; 
        private int Questionnumber;
        private int score;

        public QuizPage()
        {
            InitializeComponent();
            LoadQuestions();
            DisplayQuestion();
        }

        private void LoadQuestions()
        {
            questions = new List<string>
            {
                "What does CPU stand for?",
                "Which language is used for web apps?",
                "What is the capital of France?"
            };

            answerOptions = new List<List<string>>
            {
                new List<string> { "Central Processing Unit", "Computer Personal Unit", "Central Print Unit" },
                new List<string> { "Python", "JavaScript", "C#" },
                new List<string> { "Berlin", "Madrid", "Paris" }
            };

            correctAnswers = new List<int> { 0, 1, 2 }; 

            Questionnumber = 0;
            score = 0;
        }

        private void DisplayQuestion()
        {
            var question = questions[Questionnumber];
            QuestionDisplay.Text = question;
            AnswerOptions.Children.Clear();

            // Add new answers
            foreach (var option in answerOptions[Questionnumber])
            {
                RadioButton radioButton = new RadioButton
                {
                    Content = option,
                    Tag = answerOptions[Questionnumber].IndexOf(option) // Use IndexOf to get the index
                };
                AnswerOptions.Children.Add(radioButton);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (RadioButton radioButton in AnswerOptions.Children)
            {
                if (radioButton.IsChecked == true && (int)radioButton.Tag == correctAnswers[Questionnumber])
                {
                    score++;
                }
            }

            Questionnumber++;
            if (Questionnumber < questions.Count)
            {
                DisplayQuestion();
            }
            else
            {
                ShowResults();
            }
        }

        private void ShowResults()
        {
            ResultsPage resultsPage = new ResultsPage(score, questions.Count);
            resultsPage.Show();
            this.Close();
        }
    }
}
