using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NumberSorting.Services;
using NumberSorting.Services.Interfaces;

namespace NumberSorting.Controllers
{
    [ApiController]
    public class NumbersController : ControllerBase
    {
        private readonly IMergeSorter _mergeSorter;

        public NumbersController(IMergeSorter mergeSorter)
        {
            _mergeSorter = mergeSorter;
        }

        [HttpPost]
        [Route("numbers")]
        public async Task<List<int>> EnterNumbers([FromBody] int[] numbers)
        {
            var result = await _mergeSorter.DoMergeSort(numbers);
            await _mergeSorter.Save(new List<string>(result.Select(x => x.ToString())));
            return result;

        }

        [HttpGet]
        [Route("numbers")]
        public async Task<string[]> LoadLastNumbers()
        {
            return await _mergeSorter.Load();

        }
    }
}
