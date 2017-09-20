using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public TextAsset dialogFile = null;
    public GameObject interactTooltip;
    Vector3 tooltipOScale;
	public AudioClip character_theme;
	public AudioSource NPC_Source;
    int level = 3;

    private Dialog dialog;
    private Dialog.Conversation activeConversation;
    ShapeShifter ss;
    Interaction interaction;

    // Use this for initialization
    void Start() {
        if (dialogFile != null) {
            dialog = JsonUtility.FromJson<Dialog>(dialogFile.text);
        }
        tooltipOScale = interactTooltip.transform.localScale;
        interactTooltip.transform.localScale = Vector3.zero;

        ss = GetComponent<ShapeShifter>();

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

    public void refresh(Vector3 location) {
        transform.position = location;
        ss.SetShape(level);
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

    public void Interact(Interaction i) {
        Debug.Log("Interaction Started");
        AdvanceDialog(dialog);
        interaction = i;
    }

    private void endDialog() {
        Debug.Log("Dialog Over");
        interaction.endInteraction();
    }

    public void Select(int selection) {
        switch (selection) {
            case 0:
                level += activeConversation.good.value;
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
                level += activeConversation.neutral.value;
                endDialog();
                break;
            case 2:
                level += activeConversation.bad.value;
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

    public int getLevel() {
        return level;
    }
}
