using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogParser : MonoBehaviour
{
    public TextAsset[] dialogs;

    private List<List<string>> parsedDialogs;

    void Start()
    {
        for (int i = 0; i < dialogs.Length; i++) {
            string dialogTxt = dialogs[i].text;
            string[] lines = dialogTxt.Split(new char[] { '~' }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < lines.Length; j++) {
                string line = lines[j];
                if(line.Trim() != "") {

                }
            }
        }
    }

    public string GetDialogLine(int dialogId, int lineId) {

        return "";
    }
}
