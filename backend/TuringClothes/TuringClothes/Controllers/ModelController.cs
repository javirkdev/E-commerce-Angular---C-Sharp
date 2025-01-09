using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using TuringClothes.Database;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _model;

        public ModelController(PredictionEnginePool<ModelInput, ModelOutput> model)
        {
            _model = model;
        }

        [HttpGet]
        public ModelOutput Predict(string text)
        {
            ModelInput input = new ModelInput
            {
                Text = text
            };
            ModelOutput output = _model.Predict(input);
            return output;


        }


    } }
