using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionView : MonoBehaviour {
    [SerializeField] private float _width;
    
    public SkillView From => _from;
    public SkillView To => _to;
        
    private SkillView _from;
    private SkillView _to;

    public void Setup(SkillView from, SkillView to) {
        gameObject.name = $"ConnectionBetween{from.name}And{to.name}";
        _from = from;
        _to = to;

        Vector3 deltaVector = to.transform.localPosition - from.transform.localPosition;
        float angle = Vector3.SignedAngle(deltaVector, to.transform.right, to.transform.forward);
        
        transform.right = deltaVector;
        transform.localPosition = from.transform.localPosition;
        transform.localScale = new Vector3(deltaVector.magnitude, _width, _width);
    }
}
