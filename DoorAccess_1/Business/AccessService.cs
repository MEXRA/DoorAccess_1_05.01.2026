using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorAccess_1.DataAccess;
using DoorAccess_1.Models;

namespace DoorAccess_1.Business
{
    public class AccessService
    {
        private readonly DoorRepository _doorRepo = new DoorRepository();
        private readonly AccessRepository _accessRepo = new AccessRepository();

        public List<Door> GetDoors() => _doorRepo.GetAll();

        public void AddRecord(AccessRecord rec)
        {
            // Kurallar burada
            if (string.IsNullOrWhiteSpace(rec.PersonID))
                throw new Exception("PersonID boş olamaz.");

            if (string.IsNullOrWhiteSpace(rec.PersonName))
                throw new Exception("PersonName boş olamaz.");

            if (!rec.DoorId.HasValue)
                throw new Exception("Kapı seçmelisin.");

            _accessRepo.Insert(rec);
        }

        public AccessRecord SearchActiveById(int recordId)
            => _accessRepo.GetById(recordId);

        public bool UpdateRecord(AccessRecord rec)
        {
            if (rec.RecordID <= 0) throw new Exception("RecordID geçersiz.");
            return _accessRepo.Update(rec);
        }

        public List<AccessRecord> ListRecords(bool includeDeleted)
            => _accessRepo.List(includeDeleted);

        public bool DeleteSoft(int recordId)
            => _accessRepo.SoftDelete(recordId);
    }
}

