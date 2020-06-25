using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductStore.Models
{
    public interface ITalentRepository
    {
        IEnumerable<Talent> GetAll();
        Talent Get(int id);
        Talent Add(Talent talent);
        void Remove(int id);
        bool Update(Talent student);
    }
}