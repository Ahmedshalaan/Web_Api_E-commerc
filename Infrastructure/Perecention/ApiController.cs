﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;

namespace Presentation
{
   
    [ApiController]
    //varabel Sgment =>(("api/[controller]"))
    [Route("api/[controller]")]  // BaseUrl/api/Basket
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
    public class ApiController : ControllerBase
    {
    }
}
