using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using Waes.App.DTO;
using Waes.App.Models;
using Waes.Core.Interfaces;
using Waes.Core.Models;

namespace Waes.App.Controllers
{
    public class DiffController : ApiController
    {
        private readonly IDiffService _diffService;
        public DiffController(IDiffService diffService)
        {
            _diffService = diffService;
        }
        
        [HttpGet]
        [Route("v1/diff/{id}")]
        public JsonResult<DiffResultDto> GetDiffResult(string id)
        {
            DiffResult diffedFiles = _diffService.GetStoredDiffResult(id);
            if (diffedFiles != null)
                return Json(new DiffResultDto(diffedFiles));

            diffedFiles = _diffService.CompareLeftAndRightFiles(id);
            _diffService.StoreDiffResult(id, diffedFiles);
            return Json(new DiffResultDto(diffedFiles));
        }

        [HttpPost]
        [Route("v1/diff/{id}/left")]
        public IHttpActionResult PostLeft(string id, [FromBody]DefaultRequest value)
        {
            if (string.IsNullOrWhiteSpace(value?.Base64))
                return BadRequest("A value must be informed");

            _diffService.AddLeftFileToCompare(id, value.Base64);
            return Ok();
        }

        [HttpPost]
        [Route("v1/diff/{id}/right")]
        public IHttpActionResult PostRight(string id, [FromBody]DefaultRequest value)
        {
            if (string.IsNullOrWhiteSpace(value?.Base64))
                return BadRequest("A value must be informed");

            _diffService.AddRightFileToCompare(id, value.Base64);
            return Ok();
        }
    }
}
