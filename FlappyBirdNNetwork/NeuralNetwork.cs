namespace FlappyBirdNNetwork;

public class NeuralNetwork
{
    public Layer[] Layers;

    public NeuralNetwork(int inputCount, int[] hiddenLayers, int outputCount)
    {
        var allLayers = new List<Layer>();
        int previousCount = inputCount;
        foreach (int count in hiddenLayers)
        {
            allLayers.Add(new Layer(count, previousCount));
            previousCount = count;
        }
        
        allLayers.Add(new Layer(outputCount, previousCount)); // output layer
        
        Layers = allLayers.ToArray();
    }

    public double[] Predict(double[] inputs)
    {
        double[] output = inputs;
        foreach (Layer layer in Layers)
        {
            output = layer.FeedForward(output);
        }
        return output;
    }
}