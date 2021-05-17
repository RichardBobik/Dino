using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC3 : MonoBehaviour
{
    private Collider2D coll;
    [SerializeField] private GameObject speechBubble;

    private void Start() {
        coll = GetComponent<Collider2D>();
        speechBubble = GameObject.Find("SpeechBubbleNPC3");
        speechBubble.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            speechBubble.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            speechBubble.gameObject.SetActive(false);
        }
    }
}
