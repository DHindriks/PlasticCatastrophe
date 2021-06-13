using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullMiniRotator : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Seagull")
        {
            StartCoroutine(Rotate( other.transform.localEulerAngles.y * -1, other.gameObject ));
        }
    }

    IEnumerator Rotate(float Degrees, GameObject obj)
    {
        float startRotation = obj.transform.eulerAngles.y;
        float endRotation = startRotation + 180;
        float t = 0.0f;
        while (t < 1)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / 1) % 360;
            obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, yRotation,
            obj.transform.eulerAngles.z);
            yield return null;
        }
    }

}
