using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogParser : MonoBehaviour
{
    public TextAsset[] dialogs;

    private List<List<string>> parsedDialogs;

    void Start() {
        parsedDialogs = new List<List<string>>();
        for (int i = 0; i < dialogs.Length; i++) {
            string dialogTxt = dialogs[i].text;
            string[] lines = dialogTxt.Split(new char[] { '~' }, System.StringSplitOptions.RemoveEmptyEntries);
            List<string> d = new List<string>();
            parsedDialogs.Add(d);
            for (int j = 0; j < lines.Length; j++) {
                string line = lines[j];
                if (line.Trim(' ', '\r', '\n') != "") {
                    parsedDialogs[i].Add(line.Trim(' ', '\r', '\n'));
                }
            }
        }
    }

    public List<string> GetDialog(int dialogId) {

        return parsedDialogs[dialogId];
    }

    public int GetDialogCount() {
        return parsedDialogs.Count;
    }
}
