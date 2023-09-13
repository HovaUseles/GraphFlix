using GraphFlix.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly Neo4J neo;
        public TestController(Neo4J neo4J)
        {
            neo = neo4J;
        }
        [HttpGet("Test")]
        public async Task<Dictionary<long,IReadOnlyList<string>>> TestAsync()
        {
            var testdata = await neo.ExecuteReadAsync("MATCH (n) RETURN n LIMIT 10");
            return testdata;
        }
        [HttpPost("CreateMovie")]
        public async Task Create()
        {

        }
    }
}
