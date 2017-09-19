using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog{

    [System.Serializable]
    public class Response {
        public int value;
        public string statement;
        public Dialog continuation;
    }

    public string statement;

    public Response good;
    public Response bad;
    public Response neutral;

}
