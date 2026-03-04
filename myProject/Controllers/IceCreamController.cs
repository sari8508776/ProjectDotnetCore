using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 
using myProject.Interfaces;
using Microsoft.AspNetCore.Authorization; 

namespace myProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class IceCreamController : ControllerBase
    {
        private readonly IIceCreamService _iceCreamService; 
        public IceCreamController(IIceCreamService iceCreamService)
        {
            _iceCreamService = iceCreamService; 
        }

        [HttpGet()]
        public ActionResult<IEnumerable<IceCream>> Get()
        {
            return _iceCreamService.Get(); 
        }

        [HttpGet("{id}")]
        public ActionResult<IceCream> Get(int id)
        {
            var iceCream = _iceCreamService.Get(id); 
            if (iceCream == null)
                return NotFound();
            return iceCream; 
        }

        [HttpPost]
        public ActionResult Create(IceCream newIceCream)
        {
            var postedIceCream = _iceCreamService.Create(newIceCream); 
            return CreatedAtAction(nameof(Get), new { id = postedIceCream.Id }, postedIceCream);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, IceCream newIceCream)
        {
            var iceCream = _iceCreamService.Find(id);
            if (iceCream == null)
                return NotFound();
            newIceCream.Id = id;
            if (!_iceCreamService.Update(id, newIceCream))
                return BadRequest();
            return Ok(newIceCream);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var iceCream = _iceCreamService.Find(id);
            if (iceCream == null)
                return NotFound();
            if (!_iceCreamService.Delete(id))
                return NotFound();
            return Ok(iceCream);
        }
    }
}
