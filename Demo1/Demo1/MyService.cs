using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1
{
    public class MyService: IService
    {
        private Guid guid;
        public MyService()
        {
            guid = Guid.NewGuid();
        }

        public Guid ServiceId { get { return guid; } }
    }

    public class MyService2 : IService
    {
        private Guid guid;
        public MyService2()
        {
            guid = Guid.NewGuid();
        }

        public Guid ServiceId { get { return guid; } }
    }


    public interface IService
    {
        public Guid ServiceId { get; }
    }
}
