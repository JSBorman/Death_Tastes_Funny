using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeShifter : MonoBehaviour {

    public int currentLevel = 3;
    public Sprite[] shapes;
    public SpriteRenderer[] renderers;
    public float fadeTime = 5f;
    SpriteRenderer activeRenderer;
    bool switching = false;
    Color originalColor;

    public Slider slider;
    public float maxDist;
    Vector3 start;
    float maxSat=0;

	// Use this for initialization
	void Awake () {
        activeRenderer = renderers[0];
        activeRenderer.sprite = shapes[currentLevel];
        originalColor = activeRenderer.color;
        start = transform.position;
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
        float dist = Vector3.Distance(start, transform.position);
        float saturation = dist / maxDist;
        if (saturation > maxSat) {
            Color c = new Color();
            c.r = newColor(originalColor.r, saturation);
            c.g = newColor(originalColor.g, saturation);
            c.b = newColor(originalColor.b, saturation);
            c.a = 1;
            activeRenderer.color = c;
            maxSat = saturation;
            if (slider != null) {
                slider.value = maxSat;
            }
        }
    }

    float newColor(float orig, float sat) {
        return (((1 - orig) / 1) * sat)+orig;
    }

    IEnumerator FadeDown(bool withSat) {
        Color c = new Color();
        c.a=1;
        float t = 0f;
        float mT = .25f;
        while (t < mT) {
            if (withSat) {
                c.r = newColor(originalColor.r, 1 - (t / mT));
                c.g = newColor(originalColor.g, 1 - (t / mT));
                c.b = newColor(originalColor.b, 1 - (t / mT));
                activeRenderer.color = c;
            }
            if (slider != null) {
                slider.value = 1 - (t / mT);
            }
            t += Time.deltaTime;
            yield return null;
        }
        if (withSat) {
            c.r = newColor(originalColor.r, Mathf.Min(1 - (t / mT), 0));
            c.g = newColor(originalColor.g, Mathf.Min(1 - (t / mT), 0));
            c.b = newColor(originalColor.b, Mathf.Min(1 - (t / mT), 0));
            activeRenderer.color = c;
        }
        if (slider != null) {
            slider.value = Mathf.Min(1 - (t / mT),0);
        }
    }

    public void SetShape(int level) {
        if (level == currentLevel) {
            maxSat = 0;
            start = transform.position;
            StartCoroutine(FadeDown(true));
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
        StartCoroutine(FadeDown(false));
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
        r.color = originalColor;
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
