using Advance.NET7.MinimalApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Advance.NET7.MinimalApi.Services
{
    //有CompanyServic  可以增删改查这个Company
    //如果有User       可以增删改查这个user
    //   Teache....     可以增删改查这个Teache

    //
    public class CompanyService : BaseService, ICompanyService
    {
        public CompanyService(DbContext context) : base(context)
        {

        }

        //public int DeleteCompanyAndUser()
        //{

        //}
    }
}
