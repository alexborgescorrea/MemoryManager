using MemoryManager.Libs;
using MemoryManager.ProcessHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryManager.Writers
{
    internal class WriteMemory
    {
        private readonly ProcessHandle _processHandle;

        public WriteMemory(ProcessHandle processHandle)
        {
            _processHandle = processHandle;
        }

        public int Write(byte[] bytes, long baseAddress, uint size)
        {
            if (Kernel32.WriteProcessMemory(_processHandle.Handle, new IntPtr(baseAddress), bytes, size, out var bytesWrite))                
                return bytesWrite;

            throw new Exception($"Não foi possível escrever na memória {baseAddress}.");
        }
    }
}
