using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager INSTANCE;
    public bool debug;
    public GameObject[] NPCs;
    public GameObject[] NPCspawns;
    public ShapeShifter player;

    public GameObject heaven;
    public Transform heavenMidpoint;
    public GameObject hell;
    public Transform hellMidpoint;
    public float flyTime = 2f;

    public SpriteRenderer[] shapes;
    public Color shapeToolColor;

	// Use this for initialization
	void Awake () {
		if (INSTANCE != null) {
            enabled = false;
            return;
        } else {
            INSTANCE = this;
        }
	}

    private void Start() {
        RefreshNPCs();
        setActiveShape(1);
    }

    // Update is called once per frame
    void Update () {
        if (debug) {
            if (Input.GetKeyDown(KeyCode.Equals)) {
                GoToAfterlife(1);
            }
            else if (Input.GetKeyDown(KeyCode.Minus)) {
                GoToAfterlife(-1);
            }
        }
    }

    public void RefreshNPCs() {
        int totalLevel = 0;
        for (int i =0; i<NPCs.Length; i++) {
            NPC n = NPCs[i].GetComponent<NPC>();
            Transform[] spawns =  NPCspawns[i].GetComponentsInChildren<Transform>();
            n.refresh(spawns[Random.Range(0, spawns.Length)].position);
            totalLevel += n.getLevel();
        }
        player.SetShape(totalLevel / NPCs.Length);
        if (totalLevel/NPCs.Length == 6) {
            StartCoroutine(WaitAndAfterlife(.5f, 1));
        } else if (totalLevel/NPCs.Length == 0) {
            StartCoroutine(WaitAndAfterlife(.5f, -1));
        }
    }

    IEnumerator WaitAndAfterlife(float seconds, int afterlife) {
        yield return new WaitForSeconds(seconds);
        GoToAfterlife(afterlife);
    }

    void GoToAfterlife(int dir) {
        GameObject g = dir == 1 ? heaven : hell;
        List<Transform> spawns = new List<Transform>(g.GetComponentsInChildren<Transform>());
        spawns.Remove(g.transform);
        spawns.Remove(hellMidpoint);
        foreach (GameObject go in NPCs) {
            go.GetComponent<Rigidbody2D>().gravityScale = 0;
            go.GetComponent<Rigidbody2D>().velocity =Vector2.zero;
            Transform t = spawns[Random.Range(0, spawns.Count)];
            go.transform.position = t.position;
            spawns.Remove(t);
        }
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<TeleportIfBelowWorld>().enabled = false;
        player.GetComponent<Interaction>().SetPlayerMovementEnabled(false);
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(PlayerFlyToo(spawns[Random.Range(0, spawns.Count)].position));
        StartCoroutine(LerpCam(dir == 1 ? heavenMidpoint : hellMidpoint));
    }

    IEnumerator PlayerFlyToo(Vector3 dest) {
        GetComponent<CameraFollowPlayer>().enabled = false;
        float t = 0;
        Vector3 orig = player.transform.position;
        while (t < flyTime) {
            player.transform.position = Vector3.Lerp(orig, dest, t / flyTime);
            t += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator LerpCam(Transform tr) {
        float t = 0;
        Vector3 orig = transform.position;
        GetComponent<CameraFollowPlayer>().enabled = false;
        Vector3 lerp;
        while (t < flyTime) {
            lerp = Vector3.Lerp(orig, tr.position, t / flyTime);
            lerp.z = orig.z;
            transform.position = lerp;
            t += Time.deltaTime;
            yield return null;
        }
    }

    public void setActiveShape(float a) {
        int l = 0;
        foreach (GameObject n in NPCs) {
            l += n.GetComponent<ShapeShifter>().currentLevel;
        }
        int level = l / NPCs.Length;
        for (int i = 0; i<shapes.Length; i++) {
            Color c = new Color();
            if (i == level) {
                c = shapeToolColor;
            } else {
                c = Color.white; 
            }
            c.a = a;
            shapes[i].color = c;
        }
    }
}
