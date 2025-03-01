using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
   public float fadeDuration = 1f;
   public GameObject player;
   public CanvasGroup exitBackgroundImageCanvasGroup;
   public CanvasGroup caughtBackgroundImageCanvasGroup;

   public AudioSource exitAudio;
   public AudioSource caughtAudio;
   private bool m_hasAudioPlayed;
   
   bool m_IsPlayerAtExit;
   bool m_IsPlayerCaught;
   float m_Timer;
   public float displayImageDuration = 1f;
   
   void OnTriggerEnter(Collider other)
   {
      if (other.gameObject == player)
      {
         m_IsPlayerAtExit = true;
      }
   }
   
   public void CaughtPlayer()
   {
      m_IsPlayerCaught = true;
   }

   void Update()
   {
      if (m_IsPlayerAtExit)
      {
         EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
      } else if (m_IsPlayerCaught)
      {
         EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
      }
   }

   void EndLevel(CanvasGroup cg, bool restart, AudioSource audio)
   {
      if (!m_hasAudioPlayed)
      {
         audio.Play();
         m_hasAudioPlayed = true;
      }
      
      m_Timer += Time.deltaTime;
      
      cg.alpha = m_Timer / fadeDuration;
      if (m_Timer > fadeDuration + displayImageDuration)
      {
         if (restart)
         {
            SceneManager.LoadScene(0);
         } else
         {
            Application.Quit();
         }
      }
   }
}
