using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Service;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_PersonController : ControllerBase
    {
        PersonService _personService = new PersonService();
        [HttpGet("Get All the person")]
        public IActionResult getAll()
        {
            var persons = _personService.GetAll();
            return Ok(persons);
        }
        [HttpGet("SearchByID/{id}")]
        public IActionResult SearchID(String id)
        {
            Object person = _personService.GetById(id);
            if(person != null)
            {
                return Ok(person);

            }else return BadRequest("Not fond!");
        }
        [HttpPost("Add person")]
        public IActionResult Add([FromBody] Person person)
        {
            try
            {
                _personService.Add(person);
            }
            catch (Exception)
            {
                return BadRequest("Dulicated key Add!");
            }
            return Ok("Added!");
        }
        [HttpDelete("DeleteByID/{id}")]
        public IActionResult Delete(String id)
        {
            bool isAdded = _personService.DeleteById(id);
            if (isAdded)
            {
                return Ok("Person with ID " + id + " is deleted!");

            }
            else return BadRequest("Id not be found");
        }
        [HttpGet("SearchByName/{namekeyword}")]
        public IActionResult SearchByName(String namekeyword)
        {
            Func<Person, string> keywordSelector = person => person.Name;
            List<Person> searchResults = _personService.SearchByPartOfValue(keywordSelector, namekeyword);
            if(searchResults.Count == 0)
            {
                return NotFound("That person could'n be found!");
            }
            return Ok(searchResults);
        }
    }
}
