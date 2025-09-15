using BusinessEntities;
using BusinessEntities.Base;
using Core.Services.Users;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ICreateUserService _createUserService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IGetUserService _getUserService;
        private readonly IUpdateUserService _updateUserService;

        public UsersController(ICreateUserService createUserService, IDeleteUserService deleteUserService, IGetUserService getUserService, IUpdateUserService updateUserService)
        {
            _createUserService = createUserService;
            _deleteUserService = deleteUserService;
            _getUserService = getUserService;
            _updateUserService = updateUserService;
        }

        [Route("{userId:guid}/create")] //LendingpPad
        [HttpPost]
        public ActionResult CreateUser(Guid userId, [FromBody] UserModel model)
        {
            if (model.Email == null) { return BadRequest("Email Cannot be null "); }
            if (model.Name == null) { return BadRequest("Name Cannot be null "); }

            var user = _createUserService.Create(userId, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
            return Ok(user);
        }

        [Route("{userId:guid}/update")] //LendingpPad
        [HttpPost]
        public ActionResult UpdateUser(Guid userId, [FromBody] UserModel model)
        {
            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values
                  .SelectMany(v => v.Errors)
                  .Select(e => e.ErrorMessage)
                  .ToList();
                return BadRequest(errors);
            }


            User user = _getUserService.GetUser(userId);
            if (user == null)
            {
                return NotFound($"User with id {userId}, Not Found!");
            }
            _updateUserService.Update(ref user, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
            return Ok(user);
        }

        [Route("{userId:guid}/delete")] //LendingpPad
        [HttpDelete]
        public ActionResult DeleteUser(Guid userId)
        {
            var user = _getUserService.GetUser(userId);
            if (user == null)
            {
                return NotFound(user);
            }
            _deleteUserService.Delete(user);
            return Ok("User Deleted!");
        }

        [Route("{userId:guid}")]//LendingpPad
        [HttpGet]
        public ActionResult GetUser(Guid userId)
        {
            var user = _getUserService.GetUser(userId);
            return Ok(user);
        }

        [Route("get")] //LendingpPad
        [HttpGet]
        public ActionResult GetUsers([FromQuery] int skip, int take, UserTypes? type = null, string name = null, string email = null)
        {
            var users = _getUserService.GetUsers(name: name, email: email, userType: type)
                                       .Skip(skip).Take(take)
                                       .ToList();
            return Ok(users);
        }

        [Route("list")] //LendingpPad
        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var users = _getUserService.GetAllUsers();
            return Ok(users);
        }


        [Route("clear")] //LendingpPad
        [HttpDelete]
        public ActionResult DeleteAllUsers()
        {
            int userCount = _getUserService.GetCount();
            _deleteUserService.DeleteAll();
            return Ok($"{userCount} Users Deleted! ");
        }

        [Route("tag")] //LendingpPad
        [HttpGet]
        public ActionResult GetUsersByTag([FromQuery] string[] tag)
        {
            var users = _getUserService.GetUsers(tag);
            return Ok(users);
        }
    }
}
