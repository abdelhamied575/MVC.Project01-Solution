﻿using MVC.Project01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Project01.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {

        IEnumerable<T> GetAll();

        T Get(int Id);

        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);



    }
}