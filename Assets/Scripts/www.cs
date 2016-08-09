using UnityEngine;
using System.Collections;

public class www : MonoBehaviour
{
    public Water m_water;
   
    void Update()
    {
        int index = m_water.GetWaterStripIndex(transform.position.x);
        Vector3 position = transform.position;
        position.y = m_water.GetWaterHeight(index);
        transform.position = position;
    }
}
