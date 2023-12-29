namespace NeuralNetWebASP.Models
{
    public class GenerationData
    {
        public int DatabaseSize {  get; set; }
        public int GenerationsPassed {  get; set; }
        public double CurrentError {  get; set; }
        public double ChangeError {  get; set; }
        public double LearningFactor {  get; set; }
        public GenerationData(int db_size, int gen_passed, double current_error, double change_error, double learningFactor)
        {
            DatabaseSize = db_size;
            GenerationsPassed = gen_passed;
            CurrentError = current_error;
            ChangeError = change_error;
            LearningFactor = learningFactor;
            if (GenerationsPassed == 1) ChangeError = 0;
        }
    }
}
