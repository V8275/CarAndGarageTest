using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InteractableObject>())
        {
            score++;
            scoreText.text = $"Score: {score}";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InteractableObject>())
        {
            score--;
            scoreText.text = $"Score: {score}";
        }
    }
}
