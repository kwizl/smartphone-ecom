using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public Guid _ID { get; set; }
        public DateTime _CreationDate { get; private set; }

        public IntegrationBaseEvent()
        {
            _ID = Guid.NewGuid();
            _CreationDate = DateTime.UtcNow;
        }

        public IntegrationBaseEvent(Guid ID, DateTime CreateDate)
        {
            _ID = ID;
            _CreationDate = CreateDate;
        }
    }
}
