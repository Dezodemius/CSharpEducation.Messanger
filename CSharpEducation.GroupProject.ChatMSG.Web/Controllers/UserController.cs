using CSharpEducation.GroupProject.ChatMSG.Core.Entities;
using CSharpEducation.GroupProject.ChatMSG.Web.Contracts;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections;


namespace CSharpEducation.GroupProject.ChatMSG.Web.Controllers
{
    //лобавить разлогинивание 

    [ApiController]
  [Route("[controller]")]
  public class UserController : Controller
  {
    UserManager<UserEntity> userManager;
    IUserStore<UserEntity> userStore;
    SignInManager<UserEntity> signInManager;

    [HttpPost]
    public async Task<Results<Ok, ValidationProblem>> Registration([FromBody] UserRegisterRequest registration)
    {
      var userName = registration.UserName;
      var user = new UserEntity();

      await userStore.SetUserNameAsync(user, userName, CancellationToken.None);
      var result = await userManager.CreateAsync(user, registration.Password);

      if (!result.Succeeded)
      {
        return Validation.CreateValidationProblem(result);
      }

      return TypedResults.Ok();
    }

    [HttpPost("login")]
    public async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>> Login([FromBody] UserLoginRequest login)
    {
      var useCookieScheme = true;
      var isPersistent = true;

      signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

      var result = await signInManager.PasswordSignInAsync(login.UserName, login.Password, isPersistent, lockoutOnFailure: true);

      if (!result.Succeeded)
      {
        return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
      }

      return TypedResults.Empty;
    }

    [HttpGet("GetAllUsers")]
    public IEnumerable GetAllUsers()
    {
      if (userManager.Users.Count() > 0)
        return userManager.Users;
      else
        throw new Exception("Список пользвователей пуст");
    }

    public UserController(UserManager<UserEntity> userManager, IUserStore<UserEntity> userStore, SignInManager<UserEntity> signInManager)
    {
      this.userManager = userManager;
      this.userStore = userStore;
      this.signInManager = signInManager;
    }
  }
}
