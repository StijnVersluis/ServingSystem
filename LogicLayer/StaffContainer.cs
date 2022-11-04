﻿using InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class StaffContainer
    {
        private IStaffContainer sCont;

        public StaffContainer(IStaffContainer staffContainer)
        {
            sCont = staffContainer;
        }

        public int AttemptLogin(string uName, string password)
        {
            return sCont.AttemptLogin(uName.ToLower(), password);
        }
        public Staff GetLoggedInStaff(int id)
        {
            return new Staff(sCont.GetLoggedInStaff(id));
        }
    }
}
