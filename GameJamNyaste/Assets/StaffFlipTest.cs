using UnityEngine;

public class StaffFlipTest : MonoBehaviour
{
    void Update()
    {
        // Get the current z-axis rotation of the GameObject.
        float currentAngle = transform.eulerAngles.z;

        // Call the function to check and flip the angle if necessary.
        FlipIfLessThan90(currentAngle);
    }

    void FlipIfLessThan90(float currentAngle)
    {
        if (currentAngle < 90f)
        {
            Flip();

            Debug.Log($"The angle is less than 90 degrees. Flipping.");
        }
        else
        {
            Debug.Log("The angle is 90 degrees or greater. No flipping needed.");
        }
    }

    void Flip()
    {
        // Implement your flipping logic here.
        // For example, you can rotate the GameObject by 180 degrees.
        transform.Rotate(new Vector3(0f, 0f, 180f));
    }
}