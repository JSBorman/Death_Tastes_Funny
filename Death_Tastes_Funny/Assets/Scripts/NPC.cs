using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public TextAsset dialogFile = null;
    public GameObject interactTooltip;
	public AudioClip character_theme;
	public AudioSource NPC_Source;
    int level = 4;

    private Dialog dialog;
    private Dialog.Conversation activeConversation;

    // Use this for initialization
    void Start() {
        if (dialogFile != null) {
            dialog = JsonUtility.FromJson<Dialog>(dialogFile.text);
        }

		NPC_Source.clip = character_theme;
		NPC_Source.Play ();
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
                level -= activeConversation.good.value;
                endDialog();
                //if (activeConversation.good != null) {
                //    if (activeConversation.good.continuation == null) {
                //        endDialog();
                //    }
                //    else {
                //        AdvanceDialog(activeConversation.good.continuation);
                //    }
                //}
                //else {
                //    Debug.LogError("a good selection does not exist for this activeDialog");
                //}
                break;
            case 1:
                level -= activeConversation.neutral.value;
                endDialog();
                break;
            case 2:
                level -= activeConversation.bad.value;
                endDialog();
                break;
            default:
                Debug.LogError("a valid selection was not made");
                break;
        }
    }

    private void AdvanceDialog(Dialog dialog) {
        //todo random order and gui
        Dialog.Conversation[] c = dialog.levels.getLevel(level);
        activeConversation = c[Random.Range(0,c.Length)];
        if (activeConversation.good.statement != null) {
            Debug.LogWarning(activeConversation.good.statement);
        }
        if (activeConversation.neutral.statement != null) {
            Debug.LogWarning(activeConversation.neutral.statement);
        }
        if (activeConversation.bad.statement != null) {
            Debug.LogWarning(activeConversation.bad.statement);
        }
    }
}
