using Human;
using System.Collections.Generic;
using UnityEngine;

public class BleedSpotManager : MonoBehaviour
{
    Dictionary<string, ParticleSystem> _bleedSpots;
    private string[] _bleedSpotNames = { "ArmLeft", "ArmRight", "RightLeg", "LeftLeg", "Thorax" };
    private int _currentdetachedOrgans;

    private void Awake()
    {
        _bleedSpots = new Dictionary<string, ParticleSystem>();

        for (int i = 0; i < transform.childCount; i++)
        {
            _bleedSpots.Add(_bleedSpotNames[i], transform.GetChild(i).GetComponent<ParticleSystem>());
        }
        BodyPart.OnToolUsedEvent += ToolUsed;
    }

    private void OnDestroy()
    {
        BodyPart.OnToolUsedEvent = null;
    }

    private void ToolUsed(BodyPart bp, string tool)
    {

        if (bp.name == "Ribcage") return;

        if (bp.tag == "Organ")
        {
            if (tool == "Scalpel")
            {
                _currentdetachedOrgans++;
                _bleedSpots["Thorax"].Play();
                return;
            }
            else if (tool == "Sewingkit")
            {
                if (_currentdetachedOrgans - 1 == 0)
                {
                    _currentdetachedOrgans--;
                    _bleedSpots["Thorax"].Stop();
                    return;
                }
                _currentdetachedOrgans--;
                return;
            }
        }
        else
        {
            if (tool == "Sewingkit")
            {
                _bleedSpots[bp.name].Stop();
                return;
            }
            else
            {
                if (!_bleedSpots[bp.name].isPlaying)
                    _bleedSpots[bp.name].Play();
                return;
            }
        }

    }

}
