using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorLogger : MonoBehaviour
{
    class Error
    {
        public string message;
        public float timeStamp;

        public Error(string _message)
        {
            message = _message;
            timeStamp = Time.time;
        }
    }

    static List<Error> errors = new List<Error>();
    static bool refresh;

    [SerializeField] float errorClearTime = 5f;
    [SerializeField] TextMeshProUGUI textBox;

    void Update()
    {
        if (refresh) Refresh();

        for (int i = 0; i < errors.Count; i++)
        {
            if (Time.time - errors[i].timeStamp > errorClearTime)
            {
                errors.RemoveAt(i);
                Refresh();
            }
        }
    }
    public static void ThrowErrorMessage(string message)
    {
        errors.Add(new Error("ERROR: " + message + "\n"));
        refresh = true;
    }
    public static void ThrowSuccessMessage(string message)
    {
        errors.Add(new Error("SUCCESS: " + message + "\n"));
        refresh = true;
    }
    public void Refresh() //NOT called by NodeEditor.Refresh()
    {
        textBox.text = string.Empty;
        for (int i = 0; i < errors.Count; i++)
            textBox.text += errors[i].message;
        refresh = false;
    }
}