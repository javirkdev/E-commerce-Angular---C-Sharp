using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuringClothes.Database;
using TuringClothes.Services;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartSearchController : ControllerBase
    {
        private readonly MyDatabase _myDatabase;
        public SmartSearchController(MyDatabase myDatabase)
        {
            _myDatabase = myDatabase;
        }

        [HttpGet]
        public IEnumerable<Product> Search([FromQuery] string query) 
        {
            SmartSearchService smartSearchService = new SmartSearchService(_myDatabase);
            return smartSearchService.Search(query);
        }

    }
}
