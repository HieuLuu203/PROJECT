using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayScreen playScreen;
    public HomeScreen homeScreen;
    public InstructionScreen instructionScreen;
    public CompleteScreen completeScreen;

    public void Start()
    {
        OpenHomeScreen();

    }
    public void OpenHomeScreen()
    {
        playScreen.Hide();
        instructionScreen.Hide();
        completeScreen.Hide();
        homeScreen.Appear();
    }
    public void OpenPlayScreen()
    {
        homeScreen.Hide();
        instructionScreen.Hide();
        completeScreen.Hide();
        playScreen.Appear();
    }
    public void OpenInstructionScreen()
    {
        playScreen.Hide();
        homeScreen.Hide();
        completeScreen.Hide();
        instructionScreen.Appear();
    }
    public void OpenCompleteScreen()
    {
        playScreen.Hide();
        homeScreen.Hide();
        instructionScreen.Hide();
        completeScreen.Appear();
    }

}
