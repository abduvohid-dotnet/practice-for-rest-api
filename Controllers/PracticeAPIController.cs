using Microsoft.AspNetCore.Mvc;
using PracticeForRestApi.Models;
using PracticeForRestApi.Models.Dto;

namespace PracticeForRestApi.Controllers
{
    // [Route("api/[controller]")] shunchaki [controller] deb yozib quysa PracticeAPI avtomatik yozib quyiladi
    [Route("api/PracticeAPI")]
    [ApiController]
    public class PracticeAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<PracticeDTO> GetPractices()
        {
            return new List<PracticeDTO>{
            new PracticeDTO { Id = 1, Name = "Pool View" },
            new PracticeDTO {Id = 2, Name = "Beach View" }
            };
        }
    }
}