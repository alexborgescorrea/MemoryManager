using MemoryManager.ProcessHandles;
using MemoryManager.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryManager.ProcessMemories
{
    internal class ProcessMemory : IDisposable
    {
        private bool _disposed;
        private readonly ProcessHandle _processHandle;
        private readonly WriteMemory _writeMemory;

        public ProcessMemory(int id)
        {
            _processHandle = new ProcessHandle(id);
            _writeMemory = new WriteMemory(_processHandle);
        }

        public int Write(byte[] bytes, long baseAddress, uint size)
        {
            return _writeMemory.Write(bytes, baseAddress, size);
        }

        public void Dispose(bool disposed)
        {
            if (_disposed)
                return;

            if (disposed)
            {
                if (_processHandle is not null)
                    _processHandle.Dispose();
            }

            _disposed = true;
        }

        public void Dispose() => Dispose(true);
    }
}
