using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    
	public Animator anim;
	public AudioSource audioSource;
	public AudioClip flip;

	public void OpenCard() {
		if (FindCardManager.instance.openCount != 2) {
			audioSource.PlayOneShot(flip);
			anim.SetBool("isOpen", true);
			transform.Find("Front").gameObject.SetActive(true);
			transform.Find("Back").gameObject.SetActive(false);
			if (FindCardManager.instance.firstCard == null) {
				FindCardManager.instance.firstCard = gameObject;
				FindCardManager.instance.openCount++;
			} else {
				FindCardManager.instance.secondCard = gameObject;
				FindCardManager.instance.IsMatched();
				FindCardManager.instance.openCount++;
			}
		}
	}
    
	public void DestroyCard() {
		Invoke("DestroyCardInvoke", 0.5f);
	}

	private void DestroyCardInvoke() {
		Destroy(gameObject);
	}

	public void CloseCard() {
		Invoke("CloseCardInvoke", 0.5f);
	}

	private void CloseCardInvoke() {
		anim.SetBool("isOpen", false);
		transform.Find("Back").gameObject.SetActive(true);
		transform.Find("Front").gameObject.SetActive(false);
	}
}