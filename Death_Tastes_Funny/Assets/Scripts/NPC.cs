using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public TextAsset dialogFile = null;
    public GameObject interactTooltip;
    private Dialog dialog;
    private Dialog activeDialog;

    // Use this for initialization
    void Start() {
        if (dialogFile != null) {
            dialog = JsonUtility.FromJson<Dialog>(dialogFile.text);
        }
    }

    // Update is called once per frame
    void Update() {
        //TODO animate?
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //TODO Display interaction tooltip
    }

    public void Interact() {
        Debug.Log("Interaction Started");
        AdvanceDialog(dialog);
    }

    private void endDialog() {
        Debug.Log("Dialog Over");
    }

    public void Select(int selection) {
        switch (selection) {
            case 0:
                if (activeDialog.good != null) {
                    if (activeDialog.good.continuation == null) {
                        endDialog();
                    }
                    else {
                        AdvanceDialog(activeDialog.good.continuation);
                    }
                }
                else {
                    Debug.LogError("a good selection does not exist for this activeDialog");
                }
                break;
            case 1:
                if (activeDialog.neutral != null) {
                    if (activeDialog.good.continuation == null) {
                        endDialog();
                    }
                    else {
                        AdvanceDialog(activeDialog.neutral.continuation);
                    }
                }
                else {
                    Debug.LogError("a neutral selection does not exist for this activeDialog");
                }
                break;
            case 2:
                if (activeDialog.bad != null) {
                    if (activeDialog.good.continuation == null) {
                        endDialog();
                    }
                    else {
                        AdvanceDialog(activeDialog.bad.continuation);
                    }
                }
                else {
                    Debug.LogError("a bad selection does not exist for this activeDialog");
                }
                break;
            default:
                Debug.LogError("a valid selection was not made");
                break;
        }
    }

    private void AdvanceDialog(Dialog dialog) {
        //todo random order and gui
        activeDialog = dialog;
        Debug.Log(dialog.statement);
        if (dialog.good.statement != null) {
            Debug.LogWarning(dialog.good.statement);
        }
        if (dialog.neutral.statement != null) {
            Debug.LogWarning(dialog.neutral.statement);
        }
        if (dialog.bad.statement != null) {
            Debug.LogWarning(dialog.bad.statement);
        }
    }
}
