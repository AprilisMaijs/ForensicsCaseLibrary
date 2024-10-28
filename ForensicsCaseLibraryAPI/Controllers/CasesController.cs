using ForensicsCaseLibrary;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsCaseLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly ForensicsCaseLibrary.ForensicsCaseLibrary _forensicsLibrary;

        public CasesController(ForensicsCaseLibrary.ForensicsCaseLibrary forensicsLibrary) 
        {
            _forensicsLibrary = forensicsLibrary;
        }

        // GET: api/cases
        [HttpGet]
        public ActionResult<IEnumerable<Case>> GetAllCases()
        {
            var cases = _forensicsLibrary.ListAllCases();
            return Ok(cases);
        }

        // GET: api/cases/{caseNumber}
        [HttpGet("{caseNumber}")]
        public ActionResult<Case> GetCaseByNumber(string caseNumber)
        {
            var caseItem = _forensicsLibrary.GetCaseByNumber(caseNumber);
            return caseItem != null ? Ok(caseItem) : NotFound();
        }

        // POST: api/cases
        [HttpPost]
        public ActionResult<Case> CreateCase([FromBody] CaseRequest request)
        {
            var newCase = _forensicsLibrary.CreateCase(
                request.CustomerId,
                request.ResponsiblePerson,
                request.CaseType
            );
            return CreatedAtAction(nameof(GetCaseByNumber), new { caseNumber = newCase.CaseNumber }, newCase);
        }

        // PUT: api/cases/{caseNumber}/approve
        [HttpPut("{caseNumber}/approve")]
        public IActionResult ApproveCase(string caseNumber)
        {
            var caseToApprove = _forensicsLibrary.GetCaseByNumber(caseNumber);
            if (caseToApprove == null) return NotFound();

            try
            {
                _forensicsLibrary.ApproveCase(caseNumber);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT: api/cases/{caseNumber}/reject
        [HttpPut("{caseNumber}/reject")]
        public IActionResult RejectCase(string caseNumber)
        {
            var caseToReject = _forensicsLibrary.GetCaseByNumber(caseNumber);
            if (caseToReject == null) return NotFound();

            try
            {
                _forensicsLibrary.RejectCase(caseNumber);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // POST: api/cases/{caseNumber}/exhibits
        [HttpPost("{caseNumber}/exhibits")]
        public IActionResult AddExhibitToCase(string caseNumber, [FromBody] ExhibitRequest exhibitRequest)
        {
            var caseItem = _forensicsLibrary.GetCaseByNumber(caseNumber);
            if (caseItem == null) return NotFound();

            var exhibit = new Exhibit(exhibitRequest.Type, exhibitRequest.DateCollected);
            _forensicsLibrary.AddExhibitToCase(caseNumber, exhibit);
            return NoContent();
        }

        // GET: api/cases/{caseNumber}/cost
        [HttpGet("{caseNumber}/cost")]
        public ActionResult<decimal> GetCaseCost(string caseNumber)
        {
            var caseItem = _forensicsLibrary.GetCaseByNumber(caseNumber);
            if (caseItem == null) return NotFound();

            var cost = caseItem.CalculateTotalCost();
            return Ok(cost);
        }
    }

    public class CaseRequest
    {
        public int CustomerId { get; set; }
        public required string ResponsiblePerson { get; set; }
        public required string CaseType { get; set; }
    }

    public class ExhibitRequest
    {
        public required string Type { get; set; }
        public DateTime DateCollected { get; set; }
    }
}
