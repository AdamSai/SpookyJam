using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatManager : MonoBehaviour
{

    [SerializeField] private List<BatController> bats;

    FMOD.Studio.EventInstance batSound;
    float audio = 100;
    bool isPlayingAudio;
    // Start is called before the first frame update
    void Start()
    {
        batSound = FMODUnity.RuntimeManager.CreateInstance("event:/Characters/bat_swarming");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayingAudio)
        {
            // TODO: Fix up
            audio -= 40f * Time.deltaTime;
            batSound.setVolume(audio);

        }

        if (bats.Count == 0)
        {
            Debug.Log(audio);
            if (audio <= 0)
            {
                batSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                Destroy(gameObject);
            }
        }
    }

    public void AttackPlayer(Vector3 playerPos)
    {
        isPlayingAudio = true;
        batSound.start();
        foreach (var bat in bats)
        {
            bat.AttackPlayer(playerPos);
        }
    }

    public void MarkBatDead(BatController bat)
    {
        bats.Remove(bat);
    }
}
