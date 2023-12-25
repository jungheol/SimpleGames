using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class FindCardManager : MonoBehaviour {
    
    public static FindCardManager instance;
    public int openCount = 0;
    public Text timeText;
    public Text thisScoreText;
    public Text bestScoreText;
    public GameObject endPanel;
    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;
    public AudioSource audioSource;
    public AudioClip match;
    
    private float cardAlive = 30.0f;
    private bool isRunning = true;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Time.timeScale = 1.0f;
        int[] cards = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        cards = cards.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();
        
        for (int i = 0; i < 16; i++) {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("Duck").transform;
            
            float x = (i / 4) * 1.4f - 2.1f;
            float y = (i % 4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            string cardName = "card" + cards[i].ToString();
            newCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);
        }
    }

    private void Update() {
        if(isRunning) {
            cardAlive -= Time.deltaTime;
            timeText.text = cardAlive.ToString("N2");
            
            if(cardAlive < 0) {
                isRunning = false;
                GameOver();
            }
        }
    }

    public void IsMatched() {
        string firstCardImage = firstCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage) {
            audioSource.PlayOneShot(match);
            firstCard.GetComponent<Card>().DestroyCard();
            secondCard.GetComponent<Card>().DestroyCard();

            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            if (cardsLeft == 2) {
                isRunning = false;
                Invoke("GameOver", 0.55f);
            }
        } else {
            firstCard.GetComponent<Card>().CloseCard();
            secondCard.GetComponent<Card>().CloseCard();
        }
        
        firstCard = null;
        secondCard = null;
        Invoke("ResetClickCount", 0.5f);
    }

    private void ResetClickCount() {
        openCount = 0;
    }

    private void GameOver() {
        // AudioSourceManager.audioSourceManager.StopBG();
        thisScoreText.text = cardAlive.ToString("N2");
        endPanel.SetActive(true);
        if (PlayerPrefs.HasKey("CardBest") == false) {
            PlayerPrefs.SetFloat("CardBest", cardAlive);
        } else {
            if (PlayerPrefs.GetFloat("CardBest") < cardAlive) PlayerPrefs.SetFloat("CardBest", cardAlive);  
        }

        bestScoreText.text = PlayerPrefs.GetFloat("CardBest").ToString("N2");
        Time.timeScale = 0f;
    }

    public void RetryGame() {
        SceneManager.LoadScene((int)TopManager.SceneName.FINDCARD);
    }
    
    public void MoveIntroScene() {
        SceneManager.LoadScene((int)TopManager.SceneName.INTROSCENE);
    }
}
