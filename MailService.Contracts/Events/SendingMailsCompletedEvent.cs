using MailService.Common.Bus.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Contracts.Events
{
    public class SendingMailsCompletedEvent: IEvent
    {
        /// <summary>
        /// Number of sent mails
        /// </summary>
        public int MailsSentNo { get; private set; }
        /// <summary>
        /// Number of rejected (failed) mails
        /// </summary>
        public int MailsRejectedNo { get; private set; }
        /// <summary>
        /// Mail Id and reason for rejected mails
        /// </summary>
        public Dictionary<Guid, string> RejectionReasons { get; private set; }


        public SendingMailsCompletedEvent()
        {
            RejectionReasons = new Dictionary<Guid, string>();
        }

        public void ReportSucceded()
        {
            MailsSentNo++;
        }

        public void ReportRejected(Guid mailId, string rejectionMessage)
        {
            MailsRejectedNo++;
            RejectionReasons.Add(mailId, rejectionMessage);
        }
    }
}
