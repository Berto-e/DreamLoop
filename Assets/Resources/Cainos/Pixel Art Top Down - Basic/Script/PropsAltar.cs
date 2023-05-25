
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//when something get into the alta, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{

    public class PropsAltar : MonoBehaviour
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed;

        private Color curColor;
        private Color targetColor;
        private AudioSource runesSound;
        

        private void Start()
        {
            runesSound = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null)
            {
                targetColor = new Color(1, 1, 1, 1);
                runesSound.PlayOneShot(runesSound.clip);
            }

            CheckPointsStatic.playerCurrentCheckpoint = other.transform.position;
            CheckPointsStatic.checkPointScene  = SceneManager.GetActiveScene().buildIndex;

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 0);
        }

        private void Update()
        {
            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

            foreach (var r in runes)
            {
                r.color = curColor;
            }

            
        }
    }
}
