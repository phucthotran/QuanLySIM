using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Data.EntityRepositories;
using QuanLySIM.Entities;

namespace QuanLySIM.Tests.FakeRepositories
{
    class FakeSimRepository : ISimRepository
    {
        IList<SIM> sims;

        public FakeSimRepository()
        {
            sims = new List<SIM>
            {
                new SIM { SimId = 1, MaSIM = "1234567891234567", SoThueBao = "111111111", GiaTien = 450, TinhTrang = SIM.AVAILABLE },
                new SIM { SimId = 2, MaSIM = "2234567891234567", SoThueBao = "211111111", GiaTien = 600, TinhTrang = SIM.AVAILABLE },
                new SIM { SimId = 3, MaSIM = "3234567891234567", SoThueBao = "311111111", GiaTien = 700, TinhTrang = SIM.AVAILABLE },
                new SIM { SimId = 4, MaSIM = "4234567891234567", SoThueBao = "411111111", GiaTien = 200, TinhTrang = SIM.AVAILABLE },
                new SIM { SimId = 5, MaSIM = "3234567891234567", SoThueBao = "311111111", GiaTien = 300, TinhTrang = SIM.AVAILABLE },
                new SIM { SimId = 6, MaSIM = "6234567891234567", SoThueBao = "611111111", GiaTien = 400, TinhTrang = SIM.AVAILABLE },
                new SIM { SimId = 7, MaSIM = "7234567891234567", SoThueBao = "711111111", GiaTien = 660, TinhTrang = SIM.AVAILABLE }
            };
        }

        public IEnumerable<Entities.SIM> GetNewest()
        {
            int topThree = 3;
            int endIdx = sims.Count - 1;
            int beginIdx = endIdx - (topThree - 1);

            return sims.Skip(beginIdx).Take(sims.Count - beginIdx);
        }

        public bool IsNumExist(string PhoneNumber)
        {
            return sims.Where(x => x.SoThueBao == PhoneNumber).SingleOrDefault() != null;
        }

        public bool IsSerialExist(string Serial)
        {
            return sims.Where(x => x.MaSIM == Serial).SingleOrDefault() != null;
        }

        public int Count
        {
            get { return sims.Count; }
        }

        public IEnumerable<Entities.SIM> All
        {
            get { return sims; }
        }

        public Entities.SIM Find(int id)
        {
            return sims.Where(x => x.SimId == id).SingleOrDefault();
        }

        public int Save(Entities.SIM Entity)
        {
            if (Entity.SimId != 0)
            {
                SIM original = Find(Entity.SimId);
                original.MaSIM = Entity.MaSIM;
                original.SoThueBao = Entity.SoThueBao;
                original.GiaTien = Entity.GiaTien;
                original.TinhTrang = Entity.TinhTrang;

                return 1;
            }

            int countBefore = sims.Count;

            sims.Add(Entity);

            return sims.Count - countBefore;
        }

        public int Delete(int id)
        {
            int countBefore = sims.Count;

            sims.Remove(Find(id));

            return countBefore - sims.Count;
        }
    }
}
