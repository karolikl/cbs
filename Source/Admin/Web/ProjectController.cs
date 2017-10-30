/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2017 International Federation of Red Cross. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Domain;
using Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Read.ProjectFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.AspNet;

namespace Web
{
    [Route("api/project")]
    public class ProjectController : BaseController
    {
        readonly ILogger<ProjectController> _logger;

        readonly IProjects _projects;

        public ProjectController(
            IProjects projects,
            ILogger<ProjectController> logger)
        {
            _logger = logger;
            _projects = projects;
        }

        [HttpGet]
        public IEnumerable<Project> Get()
        {
            return _projects.GetAll();
        }

        //// Ref #288, we're not supporting async methods yet
        //// We'll take this back when this issue is solved
        //[HttpGet]
        //public async Task<IEnumerable<Project>> Get()
        //{
        //    return await _projects.GetAllASync();
        //}

        [HttpGet("{id}")]
        public Project Get(Guid id)
        {
            return _projects.GetById(id);
        }

        [HttpPost]
        public void Post([FromBody] CreateProject command)
        {
            Apply(command.Id, new ProjectCreated
            {
                Name = command.Name,
                Id = command.Id,
                NationalSocietyId = command.NationalSocietyId,
                OwnerUserId = command.OwnerUserId
            });
        }

        [HttpDelete("items/{id}")]
        public void Remove(Guid id)
        {
        }
    }
}