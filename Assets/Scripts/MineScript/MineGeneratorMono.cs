using UnityEngine;

public class MineGeneratorMono : MonoBehaviour
{
    [SerializeField] private MineGenerationSettings settings;
    [SerializeField] private MineRenderer renderers;
    [SerializeField] private BlockDiggingController blockDiggingController; 

    private void Start()
    {
        IMineGenerator generator = new BasicMineGenerator(settings);
        var mineData = generator.Generate();

        renderers.Render(mineData);
        blockDiggingController.SetMineData(mineData); 
    }
}
