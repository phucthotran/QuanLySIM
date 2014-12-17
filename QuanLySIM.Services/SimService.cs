using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Entities;
using QuanLySIM.Data.EntityRepositories;

namespace QuanLySIM.Services
{
    public class SimService : ISimService
    {
        private readonly ISimRepository _simRepository;

        public SimService(ISimRepository SimRepository)
        {
            this._simRepository = SimRepository;
        }

        public int Count
        {
            get { return _simRepository.Count; }
        }

        public IEnumerable<SIM> All
        {
            get { return _simRepository.All; }
        }        

        public int Delete(int id)
        {
            return _simRepository.Delete(id);
        }

        public int Save(SIM SIM)
        {
            return _simRepository.Save(SIM);
        }

        public SIM Find(int id)
        {
            return _simRepository.Find(id);
        }

        public IEnumerable<SIM> GetNewest()
        {
            return _simRepository.GetNewest();
        }

        public bool IsNumExist(string PhoneNumber)
        {
            return _simRepository.IsNumExist(PhoneNumber);
        }

        public bool IsSerialExist(string Serial)
        {
            return _simRepository.IsSerialExist(Serial);
        }        
    }
}
