using System.Threading.Tasks;
using UnityEngine;

public class GarageAnimation : MonoBehaviour
{
    bool opened = false;
    public float liftSpeed = 1.0f; // скорость подъема
    public Vector3 liftOffset = new Vector3(0, 5, 0); // смещение для подъема

    private void OnTriggerEnter(Collider other)
    {
        if (!opened && other.gameObject.CompareTag("Player"))
        {
            opened = true;
            LiftGarageAsync();
        }
    }

    private async void LiftGarageAsync()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + liftOffset;

        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            float distCovered = (Time.time - startTime) * liftSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);

            await Task.Yield(); // ждем следующего кадра
        }

        transform.position = targetPosition; // гарантируем, что объект достигнет конечной позиции
    }
}
