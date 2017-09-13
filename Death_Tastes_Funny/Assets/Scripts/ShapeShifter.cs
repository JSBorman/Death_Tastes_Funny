using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifter : MonoBehaviour {

    public int startLevel = 3;
    public Sprite[] shapes;
    public SpriteRenderer[] renderers;
    public float fadeTime = 5f;
    SpriteRenderer activeRenderer;

	// Use this for initialization
	void Start () {
        activeRenderer = renderers[0];
        activeRenderer.sprite = shapes[startLevel];
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.INSTANCE.Debug) {
            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                SetShape(2);
            } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                SetShape(3);
            } else if (Input.GetKeyDown(KeyCode.Alpha6)) {
                SetShape(4);
            } else if (Input.GetKeyDown(KeyCode.Alpha8)) {
                SetShape(5);
            }
        }
	}

    public void SetShape(int level) {
        if (level < 0 || level >= shapes.Length) {
            Debug.LogError(string.Format("{0} is not a valid shape level",level));
            return;
        } if (shapes[level] == null) {
            Debug.LogError(string.Format("There is no sprite for shape level {0}", level));
            return;
        }
        SpriteRenderer oldRenderer = activeRenderer;
        int i = 0;
        while (oldRenderer == activeRenderer) {
            activeRenderer = renderers[i];
        }
        StartCoroutine(SwitchShapes(activeRenderer,oldRenderer));
    }

    IEnumerator SwitchShapes(SpriteRenderer active, SpriteRenderer old) {
        Fade(active,1);
        yield return new WaitForSeconds(.5f);
        Fade(old,-1);
    }

    IEnumerator Fade(SpriteRenderer r, int dir) {
        Color c = r.color;
        float t = 0;
        while (t < fadeTime) {
            c.a = (dir ==-1 ? 1 - t : t) / fadeTime;
            yield return null;
        }
    }
}
