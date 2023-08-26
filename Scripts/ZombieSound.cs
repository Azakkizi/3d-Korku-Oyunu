using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    public GameObject player;
    public AudioSource enemyWalkingSound;
    
    void Start()
    {
        enemyWalkingSound.Play();
    }
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Bu kodda 3D moduna aldigimiz ses dosyamizin, dusman karaktere olan uzakligina gore artan azalan ve yok olan volume ozelligini manipule ediyor.
        float maxVolumeDistance = 10f;
        float minVolumeDistance = 20f;

        if (distance < maxVolumeDistance)
        {
            enemyWalkingSound.volume = 1;
        }
        else if (distance > minVolumeDistance)
        {
            enemyWalkingSound.volume = 0;
        }
        else
        {
            enemyWalkingSound.volume = 1 - ((distance - maxVolumeDistance) / (minVolumeDistance - maxVolumeDistance));
        }
    }
}