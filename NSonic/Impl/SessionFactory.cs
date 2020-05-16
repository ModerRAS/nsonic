﻿using NSonic.Impl.Net;

namespace NSonic.Impl
{
    class SessionFactory : ISessionFactory
    {
        public ISession Create(IClient tcpClient)
        {
            return new Session(tcpClient);
        }
    }
}
