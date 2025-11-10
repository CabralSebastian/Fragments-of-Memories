using UnityEngine;
using UnityEngine.Audio;
public class HealthAudioFeedback : MonoBehaviour
{
    public AudioMixer gameMixer;
    // Asumimos que tienen una variable 'playerHealth' que va de 0.0 a 1.0
    public float playerHealth = 1.0f;
    void Update()
    {
        // Mapeamos la vida (0-1) a un valor de frecuencia (400-22000Hz)
        // Mathf.Lerp interpola linealmente entre dos valores.
        float lowpassValue = Mathf.Lerp(400f, 22000f, playerHealth);
        // Usamos el nombre exacto que definimos para el parámetro expuesto.
        gameMixer.SetFloat("MasterLowPassFreq", lowpassValue);
    }
}