﻿using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Handle.HandleEmail
{
	public class EmailMessage
	{
		public List<MailboxAddress> To { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
        public EmailMessage()
        {
            
        }

		public EmailMessage(IEnumerable<string> to, string subject, string content)
		{
			To = new List<MailboxAddress>();
			To.AddRange(to.Select(x => new MailboxAddress("Email", x)));
			Subject = subject;
			Content = content;
		}
	}
}
