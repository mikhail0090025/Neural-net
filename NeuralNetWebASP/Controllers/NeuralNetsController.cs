using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neural_net;
using NeuralNetWebASP.Models;

namespace NeuralNetWebASP.Controllers
{
    [Route("api/[controller]")]
    public class NeuralNetsController : Controller
    {
        static Generation Generation { get; set; }
        static LearningDatabase LearningDatabase { get; set; }
        ILogger<NeuralNetsController> Logger { get; set; }
        public NeuralNetsController(ILogger<NeuralNetsController> _logger)
        {
            Logger = _logger;
            //Generation = new Generation(5, 5, 5, 5, 30, RoundType.DontRound, RoundType.ZeroAndOne, RoundType.Tanh, 0.0001);
            //LearningDatabase = new LearningDatabase(5, 5);
            //Generation.SetDatabase(LearningDatabase);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("newgeneration")]
        public IActionResult CreateNewGeneration([FromForm] GenerationParameters parameters)
        {
            Logger.LogInformation(Json(parameters).ToString());
            Logger.LogInformation("Creating new generation started!");
			try
			{
                parameters.learning_factor = Math.Pow(parameters.learning_factor, -1);
                Generation = new Generation(parameters.inputs_count, parameters.outputs_count, parameters.hidden_layers_count, parameters.neurals_in_hidden_layer_count, parameters.gen_size, (RoundType)parameters.inp_round, (RoundType)parameters.neu_round, (RoundType)parameters.out_round, parameters.learning_factor);
                LearningDatabase = new LearningDatabase(parameters.inputs_count, parameters.outputs_count);
                Generation.SetDatabase(LearningDatabase);
			}
            catch(Exception ex)
            {
                return Json(new { text = $"Error: {ex.Message}"});
            }
            Logger.LogInformation("End of creating generation");
            return Json(new { text = $"Generation was created!" });
        }
        [HttpPost("addtodb")]
        public IActionResult AddElementToDB([FromBody] DataDB data)
        {
            try
            {
                LearningDatabase.AddItem(data.LearningInputs, data.ExpectedOutputs);
            }
            catch (Exception ex)
            {
                return BadRequest($"<span style='color: red;'>Error: {ex.Message}</span>");
            }
            return Json("<span style='color: green;'>Example was successfully added!</span>");
        }
        private GenerationParameters GenerationParameters()
        {
            if(Generation == null)
            {
                throw new Exception("Generation is null");
            }
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
            return Json(GenerationParameters());
        }
        [HttpGet("GetAddingToDBview")]
        public IActionResult GetAddingToDBview()
        {
            if (Generation == null)
            {
                return BadRequest("<p style='color: red;'><b>Generation was not created! Create it firstly.</b></p>");
            }
			var parameters = GenerationParameters();
            Logger.LogInformation($"Parameters are null: {parameters == null}");
            return View("AddToDBView", parameters);
        }
        [HttpGet("generationinjson")]
        public IActionResult GenerationInJson()
        {
            //return Json(Generation);
            return Json(new { gen = Generation });
        }
	}
}
public class DataDB
{
    public List<double> LearningInputs { get; set; }
    public List<double> ExpectedOutputs { get; set; }
}