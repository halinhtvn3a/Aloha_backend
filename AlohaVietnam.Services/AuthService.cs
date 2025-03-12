using AlohaVietnam.Services.Interfaces;
using AlohaVietnam.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using AlohaVietnam.Repositories.Models;

namespace AlohaVietnam.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenGenerator _jwtToken;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, ITokenGenerator jwtToken)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtToken = jwtToken;
        }



        public async Task<ApiResponse> Login(LoginModel loginModel)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);

                if (user == null)
                {
                    return new ApiResponse { StatusCode = StatusCodes.Status400BadRequest, Data = "Does not have that account in the Application" };
                }

                var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: true);

                if (!result.Succeeded)
                {
                    if (result.IsLockedOut) return new ApiResponse { StatusCode = StatusCodes.Status401Unauthorized, Message = "Your account is locked. Please contact support." };
                    if (result.IsNotAllowed) return new ApiResponse { StatusCode = StatusCodes.Status401Unauthorized, Message = "Your account is not allowed to login. Please contact support." };
                    if (result.RequiresTwoFactor)
                        return new ApiResponse
                        {
                            StatusCode = StatusCodes.Status200OK,
                            Message = await _userManager.GetAuthenticatorKeyAsync(user)
                        };
                    return new ApiResponse { StatusCode = StatusCodes.Status401Unauthorized, Data = "Invalid login attempt." };
                }
                
                var roles = await _userManager.GetRolesAsync(user);
                //if (await _userManager.GetTwoFactorEnabledAsync(user) is true) return new ApiResponse { StatusCode = StatusCodes.Status200OK, Data = new { QrCode = await _userManager.GetAuthenticatorKeyAsync(user) } };
                var claims = _userManager.GetClaimsAsync(user);
                var token = _jwtToken.GenerateJwtToken(user, user.Fullname, roles[0].ToString());
                await Task.WhenAll(claims, token);
                var claimsasync = await claims;
                var tokenasync = await token;


                var firstNameClaim = claimsasync.FirstOrDefault(u => u.Type == "FirstName");
                if (firstNameClaim != null)
                {
                    await _userManager.RemoveClaimAsync(user, firstNameClaim);
                }
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Username", user.UserName));


                var refreshToken = await _jwtToken.GenerateRefreshToken();

                return new ApiResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Login successful.",
                    Data = new
                    {
                        AccessToken = tokenasync,
                        RefreshToken = refreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error during registration: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse> Register(RegisterModel model)
        {
            try
            {
                if (model == null)
                {
                    return new ApiResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Invalid request."
                    };
                }

                if (model.Password != model.ConfirmPassword)
                {
                    return new ApiResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Passwords do not match."
                    };
                }

                var user = await _userManager.FindByEmailAsync(model.Email);
                // Email confirm is not set
                if (user != null)
                {
                    return new ApiResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "User already exists"
                    };
                }

                var newUser = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);
               
                

                if (!result.Succeeded)
                {
                    return new ApiResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = $"Registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                    };
                }
                await _userManager.AddToRoleAsync(newUser, "Customer");

                await _userManager.SetTwoFactorEnabledAsync(newUser, false);
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);


                var userLogin = await _userManager.FindByEmailAsync(model.Email);
                await _userManager.ResetAuthenticatorKeyAsync(newUser);


                var token = await _jwtToken.GenerateJwtToken(userLogin, model.Name, "Customer");
                var refreshToken = await _jwtToken.GenerateRefreshToken();




                return new ApiResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "User registered successfully.",
                    Data = new
                    {
                        AccessToken = token,
                        RefreshToken = refreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error during registration: {ex.Message}"
                };
            }
        }
    }
}
