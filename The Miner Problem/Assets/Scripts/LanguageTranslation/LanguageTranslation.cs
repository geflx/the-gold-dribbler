using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

class LanguageTranslation {

    public static Dictionary<String, String> fields {
        get;
        private set;
    }

    static LanguageTranslation() {
        LoadLanguage();
    }

    public static void LoadLanguage() {
        if (fields == null) 
            fields = new Dictionary<string, string>();

        fields.Clear();

        string language = PlayerPrefs.GetString("language", "english");
        var textAsset = Resources.Load<TextAsset>(@"Languages/" + language);
        string words = textAsset.text;

        string[] lines = words.Split(new string[] {"\r\n", "\n"}, StringSplitOptions.None);
        DefineKeywords(lines);
    }

    private static void DefineKeywords(string[] lines) {

        string key, value;
        for (int i = 0; i < lines.Length; i++) {
            if (lines[i].IndexOf("=") >= 0 && !lines[i].StartsWith("#")) {
                key = lines[i].Substring(0, lines[i].IndexOf("="));
                
                value = lines[i].Substring(lines[i].IndexOf("=") + 1,
                        lines[i].Length - lines[i].IndexOf("=") - 1).Replace("\\n", Environment.NewLine);
                
                fields.Add(key, value);
            }
        } 
    }
}