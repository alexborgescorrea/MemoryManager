using MemoryManager.Libs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryManager.ProcessHandles
{
    internal class ProcessHandle : IDisposable
    {
        private readonly IntPtr _processHandle;
        private readonly Process _process;
        private bool _disposed;

        public IntPtr Handle => _processHandle;

        public ProcessHandle(int id)
            :this(Process.GetProcessById(id) ?? throw new Exception($"Processo '{id}' não encontrado."))
        {
        }

        public ProcessHandle(string processName)
            :this(Process.GetProcessesByName(processName).FirstOrDefault() ?? throw new Exception($"Processo '{processName}' não encontrado."))
        {            
        }

        public ProcessHandle(Process process)
        {
            _process = process;            
            _processHandle = Kernel32.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            if (_processHandle == IntPtr.Zero)
                throw new Exception($"Não foi possível abrir o processo '{process.ProcessName}' de ID '{_process.Id}'.");
        }

        ~ProcessHandle() => Dispose(false);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_process is not null)
                    _process.Dispose();
            }

            if (_processHandle != IntPtr.Zero)
                Kernel32.CloseHandle(_processHandle);

            _disposed = true;
        }

        public void Dispose() => Dispose(true);
    }
}
