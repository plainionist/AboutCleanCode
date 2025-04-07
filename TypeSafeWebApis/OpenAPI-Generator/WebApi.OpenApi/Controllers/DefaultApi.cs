/*
 * Shared Types
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApi.OpenApi.Attributes;
using WebApi.OpenApi.Models;

namespace WebApi.OpenApi.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public abstract class DefaultApiController : ControllerBase
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <response code="200">List of todos</response>
        [HttpGet]
        [Route("/todos")]
        [ValidateModelState]
        [ProducesResponseType(statusCode: 200, type: typeof(List<TodoItem>))]
        public abstract IActionResult GetTodos();
    }
}
