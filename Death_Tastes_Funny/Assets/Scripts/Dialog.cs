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

<<<<<<< HEAD
=======
    [System.Serializable]
    public class Level {
        public Conversation[] one;
        public Conversation[] two;
        public Conversation[] three;
        public Conversation[] four;
        public Conversation[] five;
        public Conversation[] six;
        public Conversation[] seven;

        public Conversation[] getLevel(int i) {
            switch (i) {
                case 0:
                    return one;
                case 1:
                    return two;
                case 2:
                    return three;
                case 3:
                    return four;
                case 4:
                    return five;
                case 5:
                    return six;
                case 6:
                    return seven;
                default:
                    return null;
            }
        }
    }

    public Level levels;
>>>>>>> 6f5490198f27af69cf502051bf64c865fda500d2
}
