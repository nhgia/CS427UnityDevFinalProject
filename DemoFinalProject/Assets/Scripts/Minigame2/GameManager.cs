using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject camera;

    public float numberOfRound = 3;
    
    public TextMesh NoticeNote;
    public TextMesh notify;

    public TextMesh playerText;
    public TextMesh enemyText;

    public Ball ball;
    public GameObject guide;

    int playerScore = 0;
    int enemyScore = 0;

    bool actionStop = true;


    private void Start()
    {
        DialogueSystem.ds.Display(new int[] { 0, 1});
        guide.SetActive(true);
    }

    private void Update()
    {
        if (actionStop && Input.anyKeyDown)
        {
            actionStop = false;
            AudioManager.am.PlayAgain();

            ResetAll();
        }

        if (actionStop)
        {
            ball.GetComponent<Rigidbody2D>().Sleep();
            FindObjectOfType<Player>().enabled = false;
        }
        else
        {
            ball.GetComponent<Rigidbody2D>().WakeUp();
            FindObjectOfType<Player>().enabled = true;
        }


    }

    public void UpdateRatio(bool win)
    {
        if (!win)
        {
            enemyScore++;
            
        }
        else
        {
            playerScore++;
            
        }

        if (enemyScore > numberOfRound / 2) RestartGame();
        else if (playerScore > numberOfRound / 2) { CompleteGame(); return; }
        else
        {
          
            NewRound(win);

        }
    }

    public void RestartGame()
    {
        playerScore = 0;
        enemyScore = 0;

        AudioManager.am.EndMusic();
        NoticeNote.text = "The left win!";
        NoticeNote.gameObject.SetActive(true);
        notify.gameObject.SetActive(true);

        DialogueSystem.ds.Display(new int[] { 6, 7 });

        actionStop = true;


    }

    public void NewRound(bool win)
    {

        ball.Restart(win);        
        UpdateScore();

        if (!win) DialogueSystem.ds.Display(new int[] {2,3});
        else DialogueSystem.ds.Display(new int[] { 4, 5 });


    }

    void UpdateScore()
    {
       // playerText = FindObjectsOfType<TextMesh>()[0];
       // enemyText = FindObjectsOfType<TextMesh>()[1];

        playerText.text = playerScore.ToString();
        enemyText.text = enemyScore.ToString();
    }

    
    public void CompleteGame()
    {
        // run cutscene
        AudioManager.am.EndMusic();
        NoticeNote.text = "The right win!";
        NoticeNote.gameObject.SetActive(true);
        notify.gameObject.SetActive(true);

        camera.GetComponent<Animator>().SetTrigger("Cutscene");
        DialogueSystem.ds.Display(new int[] {8,9});

        actionStop = true;
        // run UI
        //Destroy(this);
    }

    void ResetAll()
    {
        Debug.Log("Did reset all");

        ball.gameObject.SetActive(true);
        NoticeNote.gameObject.SetActive(false);
        notify.gameObject.SetActive(false);

        ball.Start();
        ball.ResetVel();

        playerScore = 0;
        enemyScore = 0;

        playerText.text = "0";
        enemyText.text = "0";
    }






}
