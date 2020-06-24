using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1
{
    public class MySecondService
    {
        private Guid guid;
        private IService myService;
        public MySecondService(IService myService)
        {
            guid = Guid.NewGuid();
            this.myService = myService;
        }

        public Guid ServiceId { get { return guid; } }

        public Guid FirstServiceId { get { return myService.ServiceId; } }
    }
}
