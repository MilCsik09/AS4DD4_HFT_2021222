﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Repository
{
    public interface IComputerRepairRepository<T>
    {
        public void Create(T t);
        public T ReadOne(int id);
        public IQueryable<T> ReadAll();
        public void Update(T t);
        public void Delete(int id);
    }
}
