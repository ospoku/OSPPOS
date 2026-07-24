
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Interfaces;
using OSPPOS.Models;
using OSPPOS.ViewModels;
using System;
using System.Drawing;

namespace OSPPOS.Services
{
  

    public class ReportService(XContext db) : IReportService
    {
        private readonly XContext ctx = db;

   
        public async Task<DashboardVM> GetDashboardAsync()
        {
          



            return new DashboardVM
            {
                
          
            };
        }

    } }



