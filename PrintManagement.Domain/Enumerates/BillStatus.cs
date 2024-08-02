﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Enumerates
{
	public enum BillStatus
	{
        Waiting = 0,
        Delivering = 1,
        NotReceived = 2,
        Refuse = 3,
        Received = 4,
    }
}
