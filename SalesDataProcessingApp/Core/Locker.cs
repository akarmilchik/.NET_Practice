using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace Core
{
    public class Locker
    {
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();

        private readonly List<string> _innerCache = new List<string>();

        public string Read(string value)
        {
            _cacheLock.EnterReadLock();
            try
            {
                return _innerCache.Where(c => c == value).FirstOrDefault();
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