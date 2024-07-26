using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;

public class Speech : MonoBehaviour
{
    public bool _isPaused = false;
    public float _textSpeed = 0.05f, _pitchMin = 0.975f, _pitchMax = 1.025f;
    private static Speech _speech;
    public static Speech instance { get { return _speech; } }
    public Type _thisType;
    public NPC _thisNPC;
    private TextMeshProUGUI _currentTextMesh;
    [HideInInspector] public bool _isPrinting = false;

    public delegate void OnPrintFinished();
    public OnPrintFinished _onPrintFinished;
    private float _pauseTimeout = 0, _pauseTime = 0;
    private Coroutine _currentCoroutine;
    private Dictionary<string, System.Action<string>> funcDictionary = new Dictionary<string, Action<string>>();
    private Dictionary<string, string> commandDictionary = new Dictionary<string, string>();
    private Dictionary<string, bool> commandFoundState = new Dictionary<string, bool>();

    private Regex _regex = new Regex(@"\w");
    private Regex _allSymbols = new Regex(@"[|\{}\<>\()]");
    private Regex _commandRegex = new Regex(@"[|\{}]");
    private Regex _targetCommandRegex = new Regex(@"[\{}]");
    private Regex _speechCommandRegex = new Regex(@"[|]");
    private Regex _commandParamsRegex = new Regex(@"[\()]");
    private Regex _richTextRegex = new Regex(@"[\<>]");
    private object[] _methodParams = null;




    private void Start()
    {
        init();
    }



    public void PrintText(string str, TextMeshProUGUI thisTextMesh, float dialogueSpeed = 0.35f, AudioClip audioClip = null)
    {
        _textSpeed = dialogueSpeed;
        _currentCoroutine = StartCoroutine(PrintLetter(str, thisTextMesh, audioClip));
    }



    private IEnumerator PrintLetter(string str, TextMeshProUGUI thisTextMesh, AudioClip thisAudioClip = null)
    {
        char currentSymbol = '\0'; // \0 equals null in unicode
        int charCount = 0;

        _currentTextMesh = thisTextMesh;
        string strWithoutCommands = str;


        _isPrinting = true;

        strWithoutCommands = Regex.Replace(strWithoutCommands, @" ?\{.*?\}", "");
        strWithoutCommands = Regex.Replace(strWithoutCommands, @" ?\|.*?\|", "");
        strWithoutCommands = Regex.Replace(strWithoutCommands, @" ?\(.*?\)", "");
        strWithoutCommands = Regex.Replace(strWithoutCommands, @" ?\<.*?\>", ""); //removing symbols and wrapped content from string


        Debug.Log(strWithoutCommands);

        foreach (char cr in str)
        {
            if (_allSymbols.IsMatch(cr.ToString()))
            {
                currentSymbol = cr;
                Debug.Log(cr);

                if (!commandFoundState[commandFoundState.GetFullKey(cr.ToString())]) commandFoundState[commandFoundState.GetFullKey(cr.ToString())] = true; //we got command symbol for the first time
                else
                {
                    Debug.Log(commandDictionary[commandDictionary.GetFullKey(cr.ToString())]);
                    funcDictionary[funcDictionary.GetFullKey(cr.ToString())]?.Invoke(funcDictionary.GetFullKey(cr.ToString())); //invoking the command (sending the dictionary key as param)
                    commandFoundState[commandFoundState.GetFullKey(cr.ToString())] = false; //we got the command symbol for the second time
                    commandDictionary[commandDictionary.GetFullKey(cr.ToString())] = ""; //cleaning the command string
                    currentSymbol = '\0'; //clear current symbol after invoking the function
                }
            }
            else if (currentSymbol != '\0') //prevent recording text as command
            {
                commandDictionary[commandDictionary.GetFullKey(currentSymbol.ToString())] += cr.ToString(); //recording command
            }

            else //handle typing
            {
                if (Time.time < _pauseTimeout) yield return new WaitForSeconds(_pauseTime); //pause if needed

                thisTextMesh.text += cr;
                if (_regex.IsMatch(cr.ToString())) //if char is text and not symbol do things
                {

                    if (charCount % 2 == 0 && thisAudioClip != null)
                    {
                        // !!!
                        // soundManager.doPlaySingleSound(interactor.globalAudioSource, thisAudioClip, pitchMin, pitchMax); // !!! handle audio later !!!
                        // !!!

                        // AudioManager.instance.PlaySoundClip(thisAudioClip);

                    }
                    charCount++;
                }

                if (strWithoutCommands.Length == thisTextMesh.text.Length)
                {
                    _onPrintFinished?.Invoke();
                }
                yield return new WaitForSeconds(_textSpeed);
            }
        }
        _isPrinting = false;
    }



    public static List<char> TextToCharacters(string str)
    {
        List<char> charList = new List<char>();
        Regex regex = new Regex(str);
        foreach (char cr in str)
        {
            Debug.Log(cr);
        }
        return charList;
    }



    private void SpeechCommand(string dictionaryKey)
    {
        MethodInfo thisMethod = this.GetType().GetMethod(commandDictionary[commandDictionary.GetFullKey(dictionaryKey)]);

        thisMethod.Invoke(instance, _methodParams); //access a method from string
        _methodParams = null;
    }



    private void NpcCommand(string dictionaryKey)
    {
        MethodInfo thisMethod = _thisType.GetMethod(commandDictionary[commandDictionary.GetFullKey(dictionaryKey)]);
        thisMethod.Invoke(_thisNPC, _methodParams); //access a method from string
        _methodParams = null;
    }



    private void ParamCommand(string dictionaryKey)
    {
        _methodParams = commandDictionary[dictionaryKey].Split(";"); //second time we find symbol. convert all params to array
    }



    private void RichTextCommand(string dictionaryKey)
    {
        _currentTextMesh.text += "<" + commandDictionary[commandDictionary.GetFullKey(dictionaryKey)] + ">";
    }



    private void init()
    {
        funcDictionary.Add("|", SpeechCommand);
        funcDictionary.Add("{}", NpcCommand);
        funcDictionary.Add("()", ParamCommand);
        funcDictionary.Add("<>", RichTextCommand);
        commandDictionary.Add("|", "");
        commandDictionary.Add("{}", "");
        commandDictionary.Add("()", "");
        commandDictionary.Add("<>", "");
        commandFoundState.Add("|", false);
        commandFoundState.Add("{}", false);
        commandFoundState.Add("()", false);
        commandFoundState.Add("<>", false);
        if (_speech != null && _speech != this)
        {
            Destroy(this);
        }
        else
        {
            _speech = this;
        }
    }



    public void StopCoroutine()
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        _isPrinting = false;
    }



    public void ReduceSpeed(string speed = "0,05")
    {
        float s = float.Parse(WebGLCheck(speed));
        _textSpeed += s;
    }



    public void IncreaseSpeed(string speed)
    {
        float s = float.Parse(WebGLCheck(speed));
        _textSpeed -= s;
    }


    public void ReducePitch(string pmin = "0,05", string pmax = "0,05")
    {
        float max = float.Parse(WebGLCheck(pmax));
        float min = float.Parse(WebGLCheck(pmin));
        _pitchMax -= max;
        _pitchMin -= min;
    }



    public void IncreasePitch(string pmin = "0,05", string pmax = "0,05")
    {
        float max = float.Parse(WebGLCheck(pmax));
        float min = float.Parse(WebGLCheck(pmin));
        _pitchMax += max;
        _pitchMin += min;
    }



    public void Pause(string pauseTimeString = "1") //pauses for a second
    {
        float t = float.Parse(WebGLCheck(pauseTimeString));
        _pauseTimeout = Time.time + t;
        _pauseTime = t;
    }



    public string WebGLCheck(string val) { //this can cause problems with float conversion.
        if (GameSystems.instance._isWebGL) //webGL version can't float.parse string with "," properly so we replace "," with "."
        {
            return val.Replace(",", ".");
        }
        return val;
    }
}