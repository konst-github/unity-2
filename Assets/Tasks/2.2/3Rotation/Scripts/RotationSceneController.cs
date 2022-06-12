using UnityEngine;

public class RotationSceneController : BreakingSceneController
{
    [SerializeField] 
    [Range(1000, 5000)] 
    private float torqueDelta = 1000f;

    protected override void Update()
    {
        base.Update();

        float horizontalInput = Input.GetAxis(Tags.axisHorizontal);
        float verticalInput = Input.GetAxis(Tags.axisVertical);

        if (horizontalInput != 0 || verticalInput != 0)
        {
            MoveCubes(horizontalInput, verticalInput);
        }
    }

    private void MoveCubes(float horizontalInput, float verticalInput)
    {
        float delta = torqueDelta * Time.deltaTime;
        foreach (BreakableCube cube in cubes)
        {
            Vector3 newTorque = new Vector3(verticalInput * delta, 0, -horizontalInput * delta);
            cube.AddTorque(newTorque);
        }
    }
}
