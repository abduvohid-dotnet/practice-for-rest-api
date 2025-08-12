using Microsoft.AspNetCore.Mvc;
using PracticeForRestApi.Data;
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
            return PracticeStore.practiceList;
        }

        [HttpGet("id")]
        public PracticeDTO GetPractice(int id)
        {
            return PracticeStore.practiceList.FirstOrDefault(u => u.Id == id);
        }
    }
}