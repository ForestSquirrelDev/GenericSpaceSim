using System.Collections;
using UnityEngine;

public class ShipParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            particles.startSpeed -= Time.deltaTime * 10;
        else
            particles.startSpeed += Time.deltaTime * 5;

        particles.startSpeed = Mathf.Clamp(particles.startSpeed, -7, -4);
    }
    private IEnumerator UpdateEmission()
    {

        while(true)
        {
            particles.startSpeed = -Input.GetAxis("Vertical") * 5;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
