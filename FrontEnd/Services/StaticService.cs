using FrontEnd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public class StaticService
    {
        public List<Status> GetStatus()
        {
            List<Status> stats = new List<Status>
            {
                new Status()
                {
                    Value = "0",
                    Label = "In-Active"
                },
                new Status()
                {
                    Value = "1",
                    Label = "Active"
                }
            };
            return stats;
        }
    }
}
