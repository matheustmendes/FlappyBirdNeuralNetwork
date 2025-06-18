namespace FlappyBirdNNetwork;

public class Layer
{
    public Neuron[] Neurons;

    public Layer(int neuronCount, int inputCountPerNeuron)
    {
        Neurons = new Neuron[neuronCount];
        for(int i = 0; i < neuronCount; i++)
        {
            Neurons[i] = new Neuron(inputCountPerNeuron);
        }

        double[] FeedForward(double[] inputs)
        {
            return Neurons.Select(n => n.Activate(inputs)).ToArray();
        }
        
        
    }
}