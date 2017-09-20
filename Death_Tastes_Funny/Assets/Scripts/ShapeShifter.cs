using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifter : MonoBehaviour {

    public int currentLevel = 3;
    public Sprite[] shapes;
    public SpriteRenderer[] renderers;
    public float fadeTime = 5f;
    SpriteRenderer activeRenderer;
    bool switching = false;

	// Use this for initialization
	void Start () {
        activeRenderer = renderers[0];
        activeRenderer.sprite = shapes[currentLevel];
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.INSTANCE.Debug) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                SetShape(0);
            }
            else if (Input.GetKeyDown(KeyCode.W)) {
                SetShape(1);
            }
            else if (Input.GetKeyDown(KeyCode.E)) {
                SetShape(2);
            }
            else if (Input.GetKeyDown(KeyCode.R)) {
                SetShape(3);
            }
            else if (Input.GetKeyDown(KeyCode.T)) {
                SetShape(4);
            }
            else if (Input.GetKeyDown(KeyCode.Y)) {
                SetShape(5);
            }
            else if (Input.GetKeyDown(KeyCode.U)) {
                SetShape(6);
            }
            else if (Input.GetKeyDown(KeyCode.I)) {
                SetShape(7);
            }
        }
	}

    public void SetShape(int level) {
        if (level == currentLevel) {
            return;
        }
        if (level < 0 || level >= shapes.Length) {
            Debug.LogError(string.Format("{0} is not a valid shape level",level));
            return;
        } if (shapes[level] == null) {
            Debug.LogError(string.Format("There is no sprite for shape level {0}", level));
            return;
        }
        if (switching) {
            return;
        }
        switching = true;
        Debug.Log(string.Format("Changing to level {0}...", level));
        SpriteRenderer oldRenderer = activeRenderer;
        activeRenderer = oldRenderer == renderers[0] ? renderers[1] : renderers[0];
        activeRenderer.sprite = shapes[level];
        currentLevel = level;
        StartCoroutine(SwitchShapes(activeRenderer,oldRenderer));
    }

    IEnumerator SwitchShapes(SpriteRenderer active, SpriteRenderer old) {
        StartCoroutine(Grow(active));
        yield return StartCoroutine(Fade(old));
        switching = false;
    }

    IEnumerator Fade(SpriteRenderer r) {
        Color c = r.color;
        float t = 0;
        while (t <= fadeTime) {
            c.a = 1 - t / fadeTime;
            r.color = c;
            t += Time.deltaTime;
            yield return null;
        }
        r.gameObject.SetActive(false);
    }

    IEnumerator Grow(SpriteRenderer r) {
        Vector3 scale = Vector3.zero;
        Vector3 position = new Vector3(0, -2f, 0);
        r.gameObject.transform.localScale = scale;
        r.gameObject.transform.localPosition = position;
        r.color = new Color(r.color.r, r.color.g, r.color.b, 1);
        r.gameObject.SetActive(true);
        float t = 0;
        while (t <= fadeTime) {
            t += Time.deltaTime;
            scale.x = scale.y = scale.z = t / fadeTime;
            position.y = -2f + 2*t / (fadeTime);
            r.gameObject.transform.localScale = scale;
            r.gameObject.transform.localPosition = position;
            yield return null;
        }
        r.gameObject.transform.localScale = Vector3.one;
        r.gameObject.transform.localPosition = Vector3.zero;
    }
}
