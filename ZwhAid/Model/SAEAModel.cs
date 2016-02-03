using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ZwhAid.Model
{
    public class SAEAModel:SocketModel
    {
        private SocketAsyncEventArgs saea;
        public SocketAsyncEventArgs Saea
        {
            set { saea = value; }
            get { return saea; }
        }

        private SocketAsyncEventArgs saeaReceive;
        public SocketAsyncEventArgs SaeaReceive
        {
            set { saeaReceive = value; }
            get { return saeaReceive; }
        }

        private SocketAsyncEventArgs saeaSend;
        public SocketAsyncEventArgs SaeaSend
        {
            set { saeaSend = value; }
            get { return saeaSend; }
        }

        private List<SocketAsyncEventArgs> saeas;
        public List<SocketAsyncEventArgs> Saeas
        {
            set { saeas = value; }
            get { return saeas; }
        }

        private ConcurrentQueue<SocketAsyncEventArgs> saeacq;
        public ConcurrentQueue<SocketAsyncEventArgs> Saeacq
        {
            set { saeacq = value; }
            get { return saeacq; }
        }

        private ConcurrentStack<SocketAsyncEventArgs> saeacs;
        public ConcurrentStack<SocketAsyncEventArgs> Saeacs
        {
            set { saeacs = value; }
            get { return saeacs; }
        }

    }
}
