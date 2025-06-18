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

    public void Mutate(double mutationRate)
    {
        var rand = new Random();

        foreach (var layer in Layers)
        {
            foreach (var neuron in layer.Neurons)
            {
                for (int i = 0; i < neuron.Weights.Length; i++)
                {
                    if(rand.NextDouble() < mutationRate)
                    {
                        // Mutate weight
                        neuron.Weights[i] += (rand.NextDouble() * 2 - 1) * 0.1; // pequena mutação
                        
                    }
                }

                if (rand.NextDouble() < mutationRate)
                {
                    neuron.Bias += (rand.NextDouble() * 2 - 1) * 0.1;
                }
            }
        }
    }

    public NeuralNetwork Crossover(NeuralNetwork partner)
    {
        var child = new NeuralNetwork(
            Layers[0].Neurons[0].Weights.Length,
            Layers.Take(Layers.Length - 1).Select(l => l.Neurons.Length).ToArray(), 
            Layers.Last().Neurons.Length);
        
        var rand = new Random();

        for (int l = 0; l < Layers.Length; l++)
        {
            for (int n = 0; n < Layers[l].Neurons.Length; n++)
            {
                var neuron = Layers[l].Neurons[n];
                var partnerNeuron = partner.Layers[l].Neurons[n];
                var childNeuron = child.Layers[l].Neurons[n];


                for (int w = 0; w < neuron.Weights.Length; w++)
                {
                    childNeuron.Weights[w] = rand.NextDouble() < 0.5 
                        ? neuron.Weights[w] 
                        : partnerNeuron.Weights[w];
                }
                
                childNeuron.Bias = rand.NextDouble() < 0.5 ? neuron.Bias : partnerNeuron.Bias;
            }
        }

        return child;
    }
}