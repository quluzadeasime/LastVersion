using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ITeamService
    {
        void Add(Team team);
        void Delete(int id);
        void Update(int id, Team team);
        Team Get(Func<Team, bool> func = null);
        List<Team> GetAll(Func<Team, bool> func = null);
    }
}
