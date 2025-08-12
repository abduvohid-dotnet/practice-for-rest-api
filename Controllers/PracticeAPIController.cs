using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        private readonly ILogger<PracticeAPIController> _logger;

        public PracticeAPIController(ILogger<PracticeAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PracticeDTO>> GetPractices()
        {
            _logger.LogInformation("Get all practices");
            return Ok(PracticeStore.practiceList);
        }

        [HttpGet("{id:int}", Name = "GetPractice")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]


        // [ProducesResponseType(200, Type = typeof(PracticeDTO))]
        // [ProducesResponseType(400)]
        // [ProducesResponseType(404)]
        public ActionResult<PracticeDTO> GetPractice(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Get Practice Error with Id", +id);
                return BadRequest();
            }
            var practice = PracticeStore.practiceList.FirstOrDefault(u => u.Id == id);

            if (practice == null)
            {
                return NotFound();
            }

            return Ok(practice);
        }

        [HttpPost]
        public ActionResult<PracticeDTO> CreatePractice([FromBody] PracticeDTO practiceDTO)
        {
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest();
            // }
            if (PracticeStore.practiceList.FirstOrDefault(u => u.Name.ToLower() == practiceDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Practice is already exist");
                return BadRequest(ModelState);
            }

            if (practiceDTO == null)
            {
                return BadRequest();
            }
            if (practiceDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            practiceDTO.Id = PracticeStore.practiceList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            PracticeStore.practiceList.Add(practiceDTO);

            return CreatedAtRoute("GetPractice", new { id = practiceDTO.Id }, practiceDTO);
        }

        [HttpDelete("{id:int}", Name = "DeletePractice")]
        public IActionResult DeletePractice(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var practice = PracticeStore.practiceList.FirstOrDefault(u => u.Id == id);

            if (practice == null)
            {
                return NotFound();
            }

            PracticeStore.practiceList.Remove(practice);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdatePractice")]
        public IActionResult UpdatePractice(int id, [FromBody] PracticeDTO practiceDTO)
        {
            if (practiceDTO == null || id != practiceDTO.Id)
            {
                return BadRequest();
            }

            var practice = PracticeStore.practiceList.FirstOrDefault(u => u.Id == id);
            practice.Name = practiceDTO.Name;
            practice.Occupancy = practiceDTO.Occupancy;
            practice.Sqft = practiceDTO.Sqft;

            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialPractice")]
        public IActionResult UpdatePartialPractice(int id, JsonPatchDocument<PracticeDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var practice = PracticeStore.practiceList.FirstOrDefault(u => u.Id == id);
            if (practice == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(practice, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}