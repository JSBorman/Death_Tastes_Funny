using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public TextAsset dialogFile = null;
    public GameObject interactTooltip;
    Vector3 tooltipOScale;
	public AudioClip character_theme;
	public AudioSource NPC_Source;

    private Dialog dialog;
    private Dialog activeDialog;

    // Use this for initialization
    void Start() {
        if (dialogFile != null) {
            dialog = JsonUtility.FromJson<Dialog>(dialogFile.text);
        }
        tooltipOScale = interactTooltip.transform.localScale;
        interactTooltip.transform.localScale = Vector3.zero;

		NPC_Source.clip = character_theme;
		NPC_Source.Play ();
    }

    // Update is called once per frame
    void Update() {
        //TODO animate?
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("FullScreenMovieScalingMode");
        StopAllCoroutines();
        StartCoroutine(ScaleTooltip(1));
    }

    private void OnTriggerExit2D(Collider2D collision) {
        StopAllCoroutines();
        StartCoroutine(ScaleTooltip(-1));
    }

    IEnumerator ScaleTooltip(int dir) {
        float t = 0;
        Vector3 startScale = interactTooltip.transform.localScale;
        Vector3 destScale = dir == 1 ? tooltipOScale : Vector3.zero;
        Debug.Log(startScale); Debug.Log(destScale);
        while (t <= .25f) {
            interactTooltip.transform.localScale = Vector3.Lerp(startScale, destScale, t / .25f);
            t+= Time.deltaTime;
            yield return null;
        }
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
<<<<<<< HEAD
        activeDialog = dialog;
        Debug.Log(dialog.statement);
        if (dialog.good.statement != null) {
            Debug.LogWarning(dialog.good.statement);
=======
        Dialog.Conversation[] c = dialog.levels.getLevel(level);
        activeConversation = c[Random.Range(0,c.Length)];
        if (activeConversation.good.statement != null) {
            Debug.LogWarning(activeConversation.good.statement);
>>>>>>> 6f5490198f27af69cf502051bf64c865fda500d2
        }
        if (dialog.neutral.statement != null) {
            Debug.LogWarning(dialog.neutral.statement);
        }
        if (dialog.bad.statement != null) {
            Debug.LogWarning(dialog.bad.statement);
        }
    }
}
