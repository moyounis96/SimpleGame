using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using System.IO;
#endif
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
namespace RTLTMPro {
    public class LanguageManager : MonoBehaviour {
        public static LanguageManager Instance { get; private set; }
        //This is where the current loaded language will go
        private static Hashtable textTable;

        public string[] languages = { "english", "arabic" };
        public Font[] arabicFont;
        [HideInInspector]
        public string[] textNumbers;
        private void Awake () {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy (gameObject);
                return;
            }
            CurrentLanguage = PlayerPrefs.GetString ("Language", "english");
            LoadLanguage (CurrentLanguage);
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad (gameObject);
        }
        [SerializeField]
        private string _currentLanguage = "english";
        public string CurrentLanguage {
            get {
                return _currentLanguage;
            }
            set {
                if (_currentLanguage == value)
                    return;
                foreach (string language in languages) {
                    if (language == value) {
                        _currentLanguage = value;
                        LoadLanguage (value);
                        break;
                    }
                }
            }
        }


    #if UNITY_EDITOR
        private void OnValidate () {
            foreach (string language in languages) {
                if (language == _currentLanguage)
                    return;
            }
            _currentLanguage = languages[0];
        }
        [MenuItem ("Translation/Update Scene POT REF")]
        public static void UpdatePOTRefs () {
            string refText = "";
            RTLTextMeshPro[] temp = Resources.FindObjectsOfTypeAll<RTLTextMeshPro> ();
            foreach (RTLTextMeshPro text_box in temp) {
                string text = text_box.GetComponent<RTLTextMeshPro> ().text;
                if (!string.IsNullOrEmpty(text)) {
                    text = text.Replace("\\","\\\\").Replace ("\n", "\\n").Replace("\r", "\\r").Replace("\"", "\\\"") ;
                    refText += "GetString(\"" + text + "\");\r\n";
                }
            }
            File.WriteAllText ("POTRefs/" + SceneManager.GetActiveScene ().name + ".cs", refText);
        }
#endif
        void OnSceneLoaded (Scene scene, LoadSceneMode loadSceneMode) {
            UpdateAllTextBoxes ();
        }
        //You call this when you want to update all text boxes with the new translation.
        //Run this after Init
        //Run this whenever you run LoadLanguage
        //Run this whenever you load a new scene and want to translate the new UI
        public void UpdateAllTextBoxes () {
            //Find all active and inactive text boxes and loop through 'em
            RTLTextMeshPro[] temp = GameObject.FindObjectsOfType<RTLTextMeshPro> (true);
            foreach (RTLTextMeshPro text_box in temp) {
                //Run the update translation function on each text 
                text_box.UpdateTranslation ();
            }
        }

        //Run this whenever a language changes, like in when a setting is changed - then run UpdateAllTextBoxes
        //This is based off of http://wiki.unity3d.com/index.php?title=TextManager, though heavily modified and expanded
        public void LoadLanguage (string lang) {
            textTable = null;
            if (lang != "english") {
                string fullpath = "Languages/" + lang + ".po"; // the file is actually ".txt" in the end

                TextAsset textAsset = (TextAsset)Resources.Load (fullpath);

                if (textAsset == null) {
                    Debug.Log ("[TextManager] " + fullpath + " file not found.");
                    return;
                } else {
                    //Debug.Log ("[TextManager] loading: " + fullpath);

                    if (textTable == null) {
                        textTable = new Hashtable ();
                    }

                    textTable.Clear ();

                    // StringReader reader = new StringReader (textAsset.text);
                    string[] lines = textAsset.text.Split (
                        new[] { "\r\n", "\r", "\n" },
                        System.StringSplitOptions.None
                    );
                    string key = null;
                    string val = null;
                    int status = 0;
                    //while ((line = reader.ReadLine ()) != null) {
                    foreach (string line in lines) {
                        if (line.StartsWith ("msgid \"")) {
                            status = 1;
                            key = line.Substring (7, line.Length - 8);
                        } else if (line.StartsWith ("msgstr \"")) {
                            status = 2;
                            val = line.Substring (8, line.Length - 9);
                        } else if (status == 1 && line != "") {
                            key += line.Substring (1, line.Length - 2);
                        } else if (status == 2 && line != "") {
                            val += line.Substring (1, line.Length - 2);
                        }
                        if (line == "" && key != null && val != null) {
                            // TODO: add error handling here in case of duplicate keys
                            key = key.Replace ("\\n", "\n");
                            val = val.Replace ("\\n", "\n");
                            if (val != "" && key != "")
                                textTable.Add (key, val);
                            key = val = null;
                        }
                    }
                }
            }
            UpdateTextNumbers ();
            UpdateAllTextBoxes ();
        }
        private void UpdateTextNumbers () {
            textNumbers = new string[10];
            textNumbers[0] = GetString ("Zero");
            textNumbers[1] = GetString ("First");
            textNumbers[2] = GetString ("Second");
            textNumbers[3] = GetString ("Third");
            textNumbers[4] = GetString ("Fourth");
            textNumbers[5] = GetString ("Fifth");
            textNumbers[6] = GetString ("Sixth");
            textNumbers[7] = GetString ("Seventh");
            textNumbers[8] = GetString ("Eighth");
            textNumbers[9] = GetString ("Ninth");
        }
        public string GetNumberText (int number) {
            if (number < 9)
                return textNumbers[number];
            return number + "";
        }

        //This handles selecting the value from the translation array and returning it, the UILocalizeText calls this
        public static string GetString (string key) {
            string result = key;
            if (key != null && textTable != null && textTable.ContainsKey (key)) {
                return (string)textTable[key];
            }
            return result;
        }
    }
}