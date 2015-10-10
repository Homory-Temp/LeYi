using System;
using System.Linq;

namespace Homory.Model
{
    public static class ModelExtension
    {
        public static void Operated(this Entities db, OperationType operationType, User user, Department campus)
        {
            try
            {
                if (user == null || campus == null)
                    return;
                var ol = new OperationLog();
                ol.Id = db.GetId();
                ol.Time = DateTime.Now;
                ol.Type = operationType;
                ol.UserId = user.Id;
                ol.CampusId = campus.Id;
                db.OperationLog.Add(ol);
                db.SaveChanges();
            }
            catch
            { }
        }
    }
}
