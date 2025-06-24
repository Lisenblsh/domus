using Domus.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domus.Models
{
    [ApiController]
    public abstract class DomusControllerBase<T>(IBaseService<T> service,
                                                UserService userService) 
                                            : ControllerBase where T : IBaseDto
    {
        private readonly UserService _userService = userService;
        private readonly IBaseService<T> _service = service;
        [HttpGet("list")]
        public IActionResult GetList()
        {
            CheckAccess();
            return Ok(_service.GetList());
        }
        [HttpGet("get")]
        public IActionResult GetById([FromQuery] int id)
        {
            
            CheckAccess();
            var entity = _service.GetById(id);
            return Ok(entity);
        }
        [HttpGet("find")]
        public IActionResult Find([FromQuery] string title)
        {
            CheckAccess();
            var entities = _service.Find(title);
            return Ok(entities);
        }
        [HttpPost("add")]
        public IActionResult Add([FromBody] T entity)
        {
            CheckAccess();
            _service.Add(entity);
            return Ok(entity.Id);
        }
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            CheckAccess();
            _service.Delete(id);
            return Ok();
        }
        [HttpPost("update")]
        public IActionResult Update([FromBody] T entity)
        {
            CheckAccess();
            _service.Update(entity);
            return Ok();
        }
        
        protected void CheckAccess()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            _userService.CheckUser(userId);
        }
    }
}
