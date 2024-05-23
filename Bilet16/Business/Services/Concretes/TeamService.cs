using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class TeamService : ITeamService
    {
        ITeamRepository _teamRepository;
        IWebHostEnvironment _environment;

        public TeamService(ITeamRepository teamRepository, IWebHostEnvironment environment = null)
        {
            _teamRepository = teamRepository;
            _environment = environment;
        }

        public void Add(Team team)
        {
            if (team == null) throw new TeamNullException("", "Team null ola bilmez");
            if (team.PhotoFile == null) throw new PhotoFileNullException("PhotoFile", "Photo null ola bilmez");
            if (!team.PhotoFile.ContentType.Contains("image/")) throw new ContentTypeException("PhotoFile", "Faylin tipi sehvdir");
            if (team.PhotoFile.Length > 2097152) throw new FileSizeException("", "Max olcu 2 mb ola biler");

            string path = _environment.WebRootPath + @"\uploads\" + team.PhotoFile.FileName;

            using(FileStream file=new FileStream(path, FileMode.Create))
            {
                team.PhotoFile.CopyTo(file);
            }
            team.ImgUrl = team.PhotoFile.FileName;


            _teamRepository.Add(team);
            _teamRepository.Commit();
        }

        public void Delete(int id)
        {
            var existTeam = _teamRepository.Get(x => x.Id == id);
            if (existTeam == null) throw new TeamNotFoundException("", "Team yoxdur");

            string path = _environment.WebRootPath + @"\uploads\" + existTeam.ImgUrl;

            if (!File.Exists(path)) throw new Exceptions.FileNotFoundException("", "Fayl yoxdur");

            File.Delete(path);
            _teamRepository.Delete(existTeam);
            _teamRepository.Commit();
        }

        public Team Get(Func<Team, bool> func = null)
        {
            return _teamRepository.Get(func);
        }

        public List<Team> GetAll(Func<Team, bool> func = null)
        {
            return _teamRepository.GetAll(func);
        }

        public void Update(int id, Team team)
        {
            if (team == null) throw new TeamNullException("", "Team null ola bilmez");
            var existTeam = _teamRepository.Get(x => x.Id == id);
            if (existTeam == null) throw new TeamNotFoundException("", "Team yoxdur");

            if (team.PhotoFile != null)
            {
                if (!team.PhotoFile.ContentType.Contains("image/")) throw new ContentTypeException("PhotoFile", "Faylin tipi sehvdir");
                if (team.PhotoFile.Length > 2097152) throw new FileSizeException("", "Max olcu 2 mb ola biler");

                string path = _environment.WebRootPath + @"\uploads\" + team.PhotoFile.FileName;

                using (FileStream file = new FileStream(path, FileMode.Create))
                {
                    team.PhotoFile.CopyTo(file);
                }
                team.ImgUrl = team.PhotoFile.FileName;
                existTeam.ImgUrl = team.ImgUrl;
            }
            existTeam.Fullname = team.Fullname;
            existTeam.Position = team.Position;
            existTeam.Description = team.Description;

            _teamRepository.Commit();

        }
    }
}
