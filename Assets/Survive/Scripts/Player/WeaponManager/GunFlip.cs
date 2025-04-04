using System;
using UnityEngine;

public class GunFlip : MonoBehaviour
{
    bool IsFlipped = false;
    
    void Update()
    {
        Flip();
    }

    private void Flip()
    {
        float gunRotation = transform.eulerAngles.z;
        
        if (!IsFlipped && ((gunRotation >= 0 && gunRotation <= 90) || (gunRotation > 270 && gunRotation < 360)))
        {
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
            IsFlipped = true;
        }
        else if (IsFlipped && gunRotation > 90 && gunRotation < 270)
        {
            transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
            IsFlipped = false;
        }
    }
}
