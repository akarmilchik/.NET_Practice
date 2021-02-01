using System.Collections.Generic;
using System.Threading;

namespace Core
{
    public class Locker
    {
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();

        private readonly List<string> _innerCache = new List<string>();

        public string Read(int key)
        {
            _cacheLock.EnterReadLock();
            try
            {
                return _innerCache[key];
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }

        public void Add(string value)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                _innerCache.Add(value);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        public void Delete(string value)
        {
            _cacheLock.EnterWriteLock();

            try
            {
                _innerCache.Remove(value);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        ~Locker()
        {
            if (_cacheLock != null) _cacheLock.Dispose();
        }
    }
}