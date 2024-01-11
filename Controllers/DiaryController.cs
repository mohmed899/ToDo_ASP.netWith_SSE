using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using task_sr_2.Models;
using task_sr_2.Repo;

namespace task_sr_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiaryController : ControllerBase
    {
        private readonly IDiaryRepository _repository;

        public DiaryController(IDiaryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetRecentEntries()
        {
            return Ok(_repository.GetRecentEntries());

        }

        [HttpPost]
        public IActionResult AddEntry(DiaryEntryDTO data)
        {
            DiaryEntry entry = new DiaryEntry()
            {
                Id = data.Id,
                Title = data.Title,
                DateTime = data.DateTime
            };
            _repository.UpdateLastNEWentry();
            _repository.AddEntry(entry);
            return Ok(entry);
        }

        [HttpPost("upload/{entryId}")]
        public async Task<IActionResult> UploadPicture(int entryId, IFormFile file)
        {
            var entry = _repository.GetEntryById(entryId);

            if (entry == null)
            {
                return NotFound("Entry not found.");
            }
            try
            {
                if (file != null && file.Length > 0)
                {
                    byte[] photoData = _repository.UploadPhoto(file).Result;
                    var picture = new Photo { PhotoData = photoData, DiaryEntryId = entryId };
                    entry.Pictures.Add(picture);
                    _repository.UpdateEntry();
                    return Ok();
                }
                else
                {
                    return BadRequest("Invalid file.");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpDelete("{entryId}")]
        public IActionResult DeleteEntry(int entryId)
        {
            var entry = _repository.GetEntryById(entryId);

            if (entry == null)
            {
                return NotFound("Entry not found.");
            }
            _repository.DeleteEntry(entryId);
            return Ok();
        }
    }

}
