using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FifaWorldCup2018.Models;

namespace FifaWorldCup2018.DAL
{
    public class GroupRepository : IGroupRepository, IDisposable
    {
        private FifaDbContext dbContext;

        public GroupRepository(FifaDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public void DeleteGroup(int groupID)
        {
            Group group = dbContext.Groups.Find(groupID);
            dbContext.Groups.Remove(group);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Group> GetGroups()
        {
            return dbContext.Groups.ToList();
        }

        public Group GetStudentByID(int groupId)
        {
            return dbContext.Groups.Find(groupId);
        }

        public void InsertGroup(Group group)
        {
            dbContext.Groups.Add(group);
        }

        public void Save()
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex) {
                string message = ex.Message;
                string message_ = ex.Message;
            }
        }

        public void UpdateGroup(Group group)
        {
            dbContext.Entry(group).State = EntityState.Modified;
        }
    }
}