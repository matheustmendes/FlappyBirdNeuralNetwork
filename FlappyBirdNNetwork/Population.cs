namespace FlappyBirdNNetwork;

public class Population
{
    public List<BirdAI> Birds;
    private int populationSize;
    private Random rand = new Random();

    public Population(int size)
    {
        populationSize = size;
        Birds = new List<BirdAI>();
        for (int i = 0; i < size; i++)
            Birds.Add(new BirdAI());
    }

    public void Evaluate()
    {
        double totalScore = Birds.Sum(b => b.Score);
        foreach (var bird in Birds)
            bird.Fitness = bird.Score / totalScore;
    }

    public BirdAI SelectParent()
    {
        double r = rand.NextDouble();
        double sum = 0;
        foreach (var bird in Birds)
        {
            sum += bird.Fitness;
            if (sum > r)
                return bird;
        }
        return Birds[0];
    }

    public List<BirdAI> NaturalSelection()
    {
        Evaluate();

        List<BirdAI> newBirds = new List<BirdAI>();
        for (int i = 0; i < populationSize; i++)
        {
            var parentA = SelectParent();
            var parentB = SelectParent();
            var child = new BirdAI();
            child.Brain = parentA.Brain.Crossover(parentB.Brain);
            child.Brain.Mutate(0.05); // mutação leve
            newBirds.Add(child);
        }

        return newBirds;
    }
}
