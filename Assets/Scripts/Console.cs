using System;
using UnityEngine;

public class Console : MonoBehaviour
{
    private static Console _console;

    private void Awake()
    {
        if (_console == null)
        {
            _console = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ConsoleInput(string input)
    {
        
    }
}
