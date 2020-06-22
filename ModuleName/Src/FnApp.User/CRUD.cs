using System.ComponentModel.DataAnnotations;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GOI.Seeker.Master.Shared.Services;
using GOI.Seeker.Master.Shared.DTOs;
using GOI.Seeker.Master.Shared.Response;

namespace GOI.Seeker.Master.FnApp.User
{
    /// <summary>
    /// The UserAPI class.
    /// Contains all methods for performing CRUD operations on User. 
    /// </summary>
    public class CRUD
    {

        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CRUD"/> class.
        /// </summary>
        /// <param name="userService">Instance of Employer service.</param>
        public CRUD(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets the User by Id.
        /// </summary>
        /// <param name="req">The HttpRequest object.</param>
        /// <param name="id">Employer Id</param>
        /// <param name="log">The Logger object.</param>
        /// <returns></returns>
        [FunctionName("GetUser")]
        public async Task<IActionResult> GetUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id}")] HttpRequest req,
            string id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                Guid userId = Guid.Parse(id);
                UserDTO user = await _userService.Find(userId);
                Response<UserDTO> response = new Response<UserDTO>()
                {
                    Data = user
                };

                return new OkObjectResult(response);
            }
            catch (Exception exception)
            {
                log.LogDebug(exception.Message);
                Response<UserDTO> response = new Response<UserDTO>()
                {
                    Error = new Error()
                    {
                        Type = "Unknown Error",
                        Message = "Something went wrong"
                    }
                };

                return new BadRequestObjectResult(response);
            }
        }

        /// <summary>
        /// Creates a new User.
        /// </summary>
        /// <param name="req">The HttpRequest object.</param>
        /// <param name="log">The Logger object.</param>
        /// <returns></returns>
        [FunctionName("CreateUser")]
        public async Task<IActionResult> CreateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                UserDTO user = JsonConvert.DeserializeObject<UserDTO>(requestBody);
                ValidateUser(user);
                user = await _userService.Add(user);
                Response<UserDTO> response = new Response<UserDTO>()
                {
                    Data = user
                };

                return new OkObjectResult(response);
            }
            catch (Exception exception)
            {
                log.LogDebug(exception.Message);
                Response<UserDTO> response = new Response<UserDTO>()
                {
                    Error = new Error()
                    {
                        Type = "Unknown Error",
                        Message = "Something went wrong"
                    }
                };

                return new BadRequestObjectResult(response);
            }
        }

        /// <summary>
        /// Validates that user can not be null.
        /// </summary>
        /// <param name="employer">UserDTO object.</param>
        private static void ValidateUser(UserDTO user)
        {
            if (user.Name == null || user.Age < 18)
            {
                throw new ValidationException("Invalid request");
            }
        }

        /// <summary>
        /// Updates an existing User.
        /// </summary>
        /// <param name="req">The HttpRequest object.</param>
        /// <param name="log">The Logger object.</param>
        /// <returns></returns>
        [FunctionName("UpdateUser")]
        public async Task<IActionResult> UpdateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users")] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                UserDTO user = JsonConvert.DeserializeObject<UserDTO>(requestBody);
                ValidateUser(user);
                user = await _userService.Update(user);
                Response<UserDTO> response = new Response<UserDTO>()
                {
                    Data = user
                };
                return new OkObjectResult(response);
            }
            catch (Exception exception)
            {
                log.LogDebug(exception.Message);
                Response<UserDTO> response = new Response<UserDTO>()
                {
                    Error = new Error()
                    {
                        Type = "Unknown Error",
                        Message = "Something went wrong"
                    }
                };

                return new BadRequestObjectResult(response);
            }
        }

        /// <summary>
        /// Deletes an User.
        /// </summary>
        /// <param name="req">The HttpRequest object.</param>
        /// <param name="id">Employer Id</param>
        /// <param name="log">The Logger object.</param>
        /// <returns></returns>
        [FunctionName("DeleteUser")]
        public async Task<IActionResult> DeleteUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "users/{id}")] HttpRequest req,
            string id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                Guid userId = Guid.Parse(id);
                await _userService.Delete(userId);
                return new OkResult();
            }
            catch (Exception exception)
            {
                log.LogDebug(exception.Message);
                Response<UserDTO> response = new Response<UserDTO>()
                {
                    Error = new Error()
                    {
                        Type = "Unknown Error",
                        Message = "Something went wrong"
                    }
                };

                return new BadRequestObjectResult(response);
            }
        }
    }
}
