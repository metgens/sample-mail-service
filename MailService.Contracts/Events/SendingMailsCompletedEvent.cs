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
        public int MailsSentNo { get; set; }
        /// <summary>
        /// Number of rejected (failed) mails
        /// </summary>
        public int MailsRejectedNo { get; set; }
        /// <summary>
        /// Mail Id and reason for rejected mails
        /// </summary>
        public Dictionary<Guid, string> RejectionReasons { get; set; }


        public SendingMailsCompletedEvent(int mailsSentNo, int mailsRejectedNo, Dictionary<Guid, string> rejectionReasons = null)
        {
            MailsSentNo = mailsSentNo;
            MailsRejectedNo = mailsRejectedNo;
            RejectionReasons = rejectionReasons;
        }
    }
}
