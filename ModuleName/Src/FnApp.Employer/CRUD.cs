using GOI.Seeker.Master.Shared.DTOs;
using GOI.Seeker.Master.Shared.Response;
using GOI.Seeker.Master.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace GOI.Seeker.Master.FnApp.Employer
{
    /// <summary>
    /// The CRUD class.
    /// Contains all methods for performing CRUD operations on Employer. 
    /// </summary>
    public class CRUD
    {
        
        private readonly IEmployerService _employerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CRUD"/> class.
        /// </summary>
        /// <param name="employerService"> Instance of Employer service. </param>
        public CRUD(IEmployerService employerService)
        {
            _employerService = employerService;
        }

        /// <summary>
        /// Gets the Employer by Id.
        /// </summary>
        /// <param name="req">The HttpRequest object.</param>
        /// <param name="id">Employer Id</param>
        /// <param name="log">The Logger object.</param>
        /// <returns></returns>
        [FunctionName("GetEmployer")]
        public async Task<IActionResult> GetEmployer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "employers/{id}")] HttpRequest req,
            string id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                Guid employerId = Guid.Parse(id);
                EmployerDTO employer = await _employerService.Find(employerId);
                Response<EmployerDTO> response = new Response<EmployerDTO>()
                {
                    Data = employer
                };

                return new OkObjectResult(response);
            }
            catch (Exception exception)
            {
                log.LogDebug(exception.Message);
                Response<EmployerDTO> response = new Response<EmployerDTO>()
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
        /// Creates a new Employer.
        /// </summary>
        /// <param name="req">The HttpRequest object.</param>
        /// <param name="log">The Logger object.</param>
        /// <returns></returns>
        [FunctionName("CreateEmployer")]
        public async Task<IActionResult> CreateEmployer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "employers")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                EmployerDTO employer = JsonConvert.DeserializeObject<EmployerDTO>(requestBody);
                ValidateEmployer(employer);
                employer = await _employerService.Add(employer);
                Response<EmployerDTO> response = new Response<EmployerDTO>()
                {
                    Data = employer
                };

                return new OkObjectResult(response);
            }
            catch (Exception exception)
            {
                log.LogDebug(exception.Message);
                Response<EmployerDTO> response = new Response<EmployerDTO>()
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
        /// Validates that employer can not be null.
        /// </summary>
        /// <param name="employer">EmployerDTO object.</param>
        private void ValidateEmployer(EmployerDTO employer)
        {
            if (employer.Name == null)
            {
                throw new ValidationException("Name cannot be null");
            }
        }

        /// <summary>
        /// Updates an existing Employer.
        /// </summary>
        /// <param name="req">The HttpRequest object.</param>
        /// <param name="log">The Logger object.</param>
        /// <returns></returns>
        [FunctionName("UpdateEmployer")]
        public async Task<IActionResult> UpdateEmployer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "employers")] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                EmployerDTO employer = JsonConvert.DeserializeObject<EmployerDTO>(requestBody);
                ValidateEmployer(employer);
                employer = await _employerService.Update(employer);
                Response<EmployerDTO> response = new Response<EmployerDTO>()
                {
                    Data = employer
                };

                return new OkObjectResult(response);
            }
            catch (Exception exception)
            {
                log.LogDebug(exception.Message);
                Response<EmployerDTO> response = new Response<EmployerDTO>()
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
        /// Deletes an Employer.
        /// </summary>
        /// <param name="req">The HttpRequest object.</param>
        /// <param name="id">Employer Id</param>
        /// <param name="log">The Logger object.</param>
        /// <returns></returns>
        [FunctionName("DeleteEmployer")]
        public async Task<IActionResult> DeleteEmployer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "employers/{id}")] HttpRequest req,
            string id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                Guid employerId = Guid.Parse(id);
                await _employerService.Delete(employerId);
                return new OkResult();
            }
            catch (Exception exception)
            {
                log.LogDebug(exception.Message);
                Response<EmployerDTO> response = new Response<EmployerDTO>()
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
