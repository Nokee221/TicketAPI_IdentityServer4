using MyApp.Repository;
using Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class ProjectsScreenUseCase : IProjectsScreenUseCase
    {
        private readonly IProjectRepository projectRepository;

        public ProjectsScreenUseCase(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        public async Task<IEnumerable<Project>> ViewProjects()
        {
            return await projectRepository.GetAsync();
        }
    }
}
