using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', b => { write((char)b.Memory[b.MemoryPointer]); });
            vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = (byte)read(); });
            RegisterPlusCommand(vm);
            RegisterMinusCommand(vm);
            RegisterGreaterLessCommands(vm);
            RegisterAlphabet(vm);
        }

        private static void RegisterPlusCommand(IVirtualMachine vm)
        {
            vm.RegisterCommand('+', b =>
            {
                if (b.Memory[b.MemoryPointer] == 255)
                    b.Memory[b.MemoryPointer] = 0;
                else
                    b.Memory[b.MemoryPointer]++;
            });
        }

        private static void RegisterMinusCommand(IVirtualMachine vm)
        {
            vm.RegisterCommand('-', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.Memory[b.MemoryPointer] = 255;
                else
                    b.Memory[b.MemoryPointer]--;
            });
        }

        private static void RegisterGreaterLessCommands(IVirtualMachine vm)
        {
            vm.RegisterCommand('>', b =>
            {
                if (b.MemoryPointer == b.Memory.Length - 1)
                    b.MemoryPointer = 0;
                else
                    b.MemoryPointer++;
            });

            vm.RegisterCommand('<', b =>
            {
                if (b.MemoryPointer == 0)
                    b.MemoryPointer = b.Memory.Length - 1;
                else
                    b.MemoryPointer--;
            });
        }

        private static void RegisterAlphabet(IVirtualMachine vm)
        {
            for (var i = 'A'; i <= 'Z'; i++)
            {
                var j = i;
                var k = Char.ToLower(i);
                vm.RegisterCommand(j, b => { b.Memory[b.MemoryPointer] = (byte)j; });
                vm.RegisterCommand(k, b => { b.Memory[b.MemoryPointer] = (byte)k; });
            }
            for (var i = '0'; i <= '9'; i++)
            {
                var j = i;
                vm.RegisterCommand(j, b => { b.Memory[b.MemoryPointer] = (byte)j; });
            }
        }
    }
}