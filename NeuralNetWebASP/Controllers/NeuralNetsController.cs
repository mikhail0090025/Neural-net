using Microsoft.AspNetCore.Mvc;
using Neural_net;
using NeuralNetWebASP.Models;

namespace NeuralNetWebASP.Controllers
{
    [Route("api/[controller]")]
    public class NeuralNetsController : Controller
    {
        Generation Generation { get; set; }
        LearningDatabase LearningDatabase { get; set; }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("NewGeneration")]
        public IActionResult CreateNewGeneration([FromForm] GenerationParameters parameters)
        {
            try
            {
                parameters.learning_factor = 1 / parameters.learning_factor;
                Generation = new Generation(parameters.inputs_count, parameters.outputs_count, parameters.hidden_layers_count, parameters.neurals_in_hidden_layer_count, parameters.gen_size, (RoundType)parameters.inp_round, (RoundType)parameters.neu_round, (RoundType)parameters.out_round, parameters.learning_factor);
                LearningDatabase = new LearningDatabase(parameters.inputs_count, parameters.outputs_count);
                Generation.SetDatabase(LearningDatabase);
            }
            catch(Exception ex)
            {
                return Json(new { text = $"Error: {ex.Message}"});
            }
            return Json(new { text = $"Neural net population was successfully created! Generation is null: {Generation == null}" });
        }
        [HttpPost("AddToDB")]
        public IActionResult AddElementToDB([FromBody] object data)
        {
            dynamic gotten_data = data;
            return Json(gotten_data);
        }
        private GenerationParameters GenParams()
        {
			GenerationParameters parameters = new GenerationParameters();
			parameters.inputs_count = Generation.InputsCount;
			parameters.outputs_count = Generation.OutputsCount;
			parameters.neurals_in_hidden_layer_count = Generation.NeuralsInHiddenLayersAmount;
			parameters.hidden_layers_count = Generation.HiddenLayersCount;
			parameters.gen_size = Generation.Size;
			parameters.inp_round = (int)Generation.InputsRound;
			parameters.out_round = (int)Generation.OutputsRound;
			parameters.neu_round = (int)Generation.NeuralsRound;
			parameters.learning_factor = (int)Generation.LearningFactor;
            return parameters;
		}
		[HttpGet("GetData")]
        public IActionResult GetGenerationData()
        {
            return Json(GenParams());
        }
        [HttpGet("GetAddingToDBview")]
        public IActionResult GetAddingToDBview()
        {
            if (Generation == null)
            {
                return BadRequest("Generation was not created!");
            }
            var parameters = GenParams();
            return View("AddToDBView", parameters);
        }
	}
}
