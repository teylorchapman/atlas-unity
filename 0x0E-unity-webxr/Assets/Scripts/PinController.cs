using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour
{
    private Quaternion initRot;
    public float knockdownThreshold = 10f;
    private bool knockedDown = false;
    public ScoreKeeper scoreKeeper;
    

    // Start is called before the first frame update
    void Start()
    {
        initRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        CheckKnockDown();
    }

    private void CheckKnockDown()
    {
        
        if (!knockedDown)
        {
            float diffAngle = Quaternion.Angle(transform.rotation, initRot);

            if (diffAngle > knockdownThreshold)
            {
                knockedDown = true;

                HandleKnockDown();
            }
        }
    }

    private void HandleKnockDown()
    {
        if (scoreKeeper != null)
        {
            scoreKeeper.ScoreIncrease();
        }
    }
}
