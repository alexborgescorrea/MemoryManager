using static System.Console;
using MemoryManager.ProcessMemories;

WriteLine("Iniciando");

//52428827
var _id = 22452;
var baseAddress = 0x25CCB27C068;
using var processMemory = new ProcessMemory(22452);

for (short i = 1; i <= 180; i++)
{
    WriteLine($"[{i}] Address {baseAddress:X}");
    var a = 2L;
    a.ToString();
    var item = BitConverter.GetBytes(i);
    var quant = BitConverter.GetBytes((short)i);

    var bytes = item.Concat(quant).ToArray();
    processMemory.Write(bytes, baseAddress, 4);
    baseAddress += 4;
}

WriteLine("Terminou!!!!!!!!!");