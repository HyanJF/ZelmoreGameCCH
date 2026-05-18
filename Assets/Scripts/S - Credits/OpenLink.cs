using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void OpenWebPage(string url)
    {
        Debug.Log("Boton Precionado");
        Application.OpenURL(url);
    }
}
