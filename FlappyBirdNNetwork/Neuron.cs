namespace FlappyBirdNNetwork;

public class Neuron
{
    public double[] Weights;
    public double Bias;
    public double Output;
    
    private static Random random = new Random();

    public Neuron(int inputCount)
    {
        Weights = new double[inputCount];
        for(int i = 0; i < inputCount; i++)
        {
            Weights[i] = random.NextDouble() * 2 - 1; // intervalo [-1,1]
            
        }

        Bias = random.NextDouble() * 2 - 1; // intervalo [-1,1]
    }

    public double Activate(double[] inputs)
    {
        double sum = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            sum += inputs[i] * Weights[i];
        }
        sum += Bias;
        Output = Sigmoid(sum);
        return Output;
    }
    
    private double Sigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));
    public double SigmoidDerivada() => Output * (1 - Output);
}