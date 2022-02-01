using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface IProjectsScreenUseCase
    {
        Task<IEnumerable<Project>> ViewProjects();
    }
}