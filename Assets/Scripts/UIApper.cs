using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeAction
{
    FadeInAndOut
}

public class UIApper : MonoBehaviour
{
    [SerializeField] private FadeAction fadeType;
    [SerializeField] private Image customImage;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            customImage.enabled = true;

            StartCoroutine(FadeInAndOut());
        }
    }
    IEnumerator FadeInAndOut()
    {
        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            customImage.color = new Color(1, 1, 1, i);
            yield return null;
        }

        //Temp to Fade Out
        yield return new WaitForSeconds(3);

        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            customImage.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
}
