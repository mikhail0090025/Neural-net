namespace NeuralNetWebASP.Models
{
    public class GenerationParameters
    {
        public int inputs_count {  get; set; }
        public int outputs_count { get; set; }
        public int hidden_layers_count { get; set; }
        public int neurals_in_hidden_layer_count { get; set; }
        public int inp_round { get; set; }
        public int neu_round { get; set; }
        public int out_round { get; set; }
        public int gen_size { get; set; }
        public double learning_factor { get; set; }
    }
}
