using System.Collections.Generic;
using System.Threading;

namespace Core
{
    public class Locker
    {
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

        private List<string> innerCache = new List<string>();

        public string Read(int key)
        {
            cacheLock.EnterReadLock();
            try
            {
                return innerCache[key];
            }
            finally
            {
                cacheLock.ExitReadLock();
            }
        }

        public void Add(string value)
        {
            cacheLock.EnterWriteLock();
            try
            {
                innerCache.Add(value);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public void Delete(string value)
        {
            cacheLock.EnterWriteLock();

            try
            {
                innerCache.Remove(value);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        ~Locker()
        {
            if (cacheLock != null) cacheLock.Dispose();
        }
    }
}
