using Human;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftArm, rightArm, leftLeg, rightLeg, inBody;
    private int _currentAttachedInBodyParts, _totalInBodyParts;

    private void Awake()
    {
        _totalInBodyParts = FindObjectOfType<BodyPartManager>().GetTotalInBodyParts();
        _currentAttachedInBodyParts = _totalInBodyParts;
    }
    
    private void Start() => BodyPart.OnToolUsedEvent += OnToolUsedEvent;
    private void OnDestroy() => BodyPart.OnToolUsedEvent = null;
    private void OnToolUsedEvent(BodyPart bodyPart, string toolName)
    {
        if (bodyPart.IsInBodyBodyPart())
        {
            if (bodyPart.IsAttached()) 
                _currentAttachedInBodyParts++;
            else 
                _currentAttachedInBodyParts--;
            
            if (_currentAttachedInBodyParts == _totalInBodyParts) 
                inBody.Stop();
            else 
                inBody.Play();
            return;
        }
        switch (bodyPart.GetPartName())
        {
            case "Left Arm": if (bodyPart.IsAttached()) leftArm.Stop(); else leftArm.Play(); break;
            case "Right Arm": if (bodyPart.IsAttached()) rightArm.Stop();else rightArm.Play(); break;
            case "Left Leg": if (bodyPart.IsAttached()) leftLeg.Stop();else leftLeg.Play(); break;
            case "Right Leg": if (bodyPart.IsAttached()) rightLeg.Stop();else rightLeg.Play(); break;
        }
    }
}