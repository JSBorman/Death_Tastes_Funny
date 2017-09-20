﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject buttonContainer;
    public GameObject[] buttons;
    public Text speechBubble;

    Coroutine interactionCoroutine;

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
        StopCoroutine(interactionCoroutine);
        interactionCoroutine = StartCoroutine(ScaleTooltip(1));
    }

    private void OnTriggerExit2D(Collider2D collision) {
        StopCoroutine(interactionCoroutine);
        interactionCoroutine= StartCoroutine(ScaleTooltip(-1));
    }

    public void refresh(Vector3 location) {
        transform.position = location;
        ss.SetShape(level);
    }

    IEnumerator ScaleTooltip(int dir) {
        float t = 0;
        Vector3 startScale = interactTooltip.transform.localScale;
        Vector3 destScale = dir == 1 ? tooltipOScale : Vector3.zero;
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
        buttonContainer.SetActive(false);
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
        buttonContainer.SetActive(true);
        Dialog.Conversation[] c = dialog.levels.getLevel(level);
        activeConversation = c[Random.Range(0,c.Length)];
        speechBubble.text = activeConversation.statement;
        if (activeConversation.good.statement != null) {
            buttons[0].GetComponentInChildren<Text>().text = activeConversation.good.statement;
        }
        if (activeConversation.neutral.statement != null) {
            buttons[1].GetComponentInChildren<Text>().text = activeConversation.neutral.statement;
        }
        if (activeConversation.bad.statement != null) {
            buttons[2].GetComponentInChildren<Text>().text = activeConversation.bad.statement;
        }
        for (int i = 0; i<buttons.Length; i++) {
            int j = i;
            Button b = buttons[i].GetComponent<Button>();
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(delegate(){ Debug.Log(j); Select(j); });
        }
    }



    public int getLevel() {
        return level;
    }
}
