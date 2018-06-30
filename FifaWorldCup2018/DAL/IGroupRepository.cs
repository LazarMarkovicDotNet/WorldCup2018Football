using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FifaWorldCup2018.Models;

namespace FifaWorldCup2018.DAL
{
    public interface IGroupRepository : IDisposable
    {
        IEnumerable<Group> GetGroups();
        Group GetStudentByID(int groupId);
        void InsertGroup(Group group);
        void DeleteGroup(int groupID);
        void UpdateGroup(Group group);
        void Save();
    }
}
