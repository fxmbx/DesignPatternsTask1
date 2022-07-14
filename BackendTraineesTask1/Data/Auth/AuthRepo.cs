    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using BackendTraineesTask1.Models;
    using BackendTraineesTask1.Models.Dto;
    using BackendTraineesTask1.Response;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;

    namespace BackendTraineesTask1.Data.Auth
    {
        public class AuthRepo : IAuthRepo
        {
            private readonly IConfiguration configuration;
            private readonly ApplicationDataContext dbcontext;
            public AuthRepo(ApplicationDataContext _dbcontext,IConfiguration _configuration)
            {
                dbcontext = _dbcontext;
                configuration = _configuration;
            }
            public async Task<ServiceResponse<AuthenticationResponse>> Login(UserLoginDto req)
            {
                var response = new ServiceResponse<AuthenticationResponse>();
                try
                {
                      string  jwtToken;
                      var userData = await dbcontext.User.FirstOrDefaultAsync(x=>x.UserName == req.UserName);
                      
                      if(userData != null ){

                          if(!IsValidPassword(req.Password, userData.Password)){

                              response.Success = false;
                              response.Message ="username or password incorrect 😞";
                              return response;

                          }
                          jwtToken = GenerateJwtToken(userData);
                          response.Message = "Login Succes 😄";
                          response.Data = new AuthenticationResponse(userData, jwtToken);
                          response.Success = true;
                      }else{
                          response.Message = "username or password incorrect 😞";
                          response.Success = false;
                      }

                }
                catch (System.Exception ex)
                {
                    response.Success =false;
                    response.Message = ex.Message;
                }
                return response;
            }

            public async Task<ServiceResponse<AuthenticationResponse>> Register(UserRegisterDto req)
            {
                ServiceResponse<AuthenticationResponse> response = new ServiceResponse<AuthenticationResponse>();

                try
                {
                    string  jwtToken;

                    if(req.Role.ToLower() != "RegularUser".ToLower() || req.Role.ToLower() !="BigBoyUser".ToLower()){
                        response.Message= "calm down chief, the role wey you  system 😅 ";
                        response.Success = false;
                        return response;
                    }

                    if(!await UserExist(req.UserName))
                    {
                            var user = new User
                            {
                                UserName = req.UserName,
                                Password =BCrypt.Net.BCrypt.EnhancedHashPassword(req.Password),
                                Role = req.Role
                            };
                            await dbcontext.User.AddAsync(user);
                            await dbcontext.SaveChangesAsync(true);
                            jwtToken = GenerateJwtToken(user);
                    
                            response.Message = "Account Created and Signed In 😉";
                            response.Data = new AuthenticationResponse(user, jwtToken);
                    }else{
                          response.Message = "Username Exist";
                          response.Success= false;
                    }

                }catch (System.Exception ex)
                {
                    
                    response.Message= ex.Message;
                    response.Success = false;
                }
                return response;
            }

            private async Task<bool> UserExist(string username)
            {
                if( await dbcontext.User.AnyAsync(x=> x.UserName.ToLower().Trim() ==username.ToLower().Trim())){
                return true;
            }
            return false;
            }
            private bool IsValidPassword(string inputedPassword, string storedPassword){
                return BCrypt.Net.BCrypt.EnhancedVerify(inputedPassword, storedPassword);
            }
            private string GenerateJwtToken(User user){
                var tokenHandler = new JwtSecurityTokenHandler();

                SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));

                SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity( new Claim[]{
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role),
                    
                    }),
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = credentials
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }