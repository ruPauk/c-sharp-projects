using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		private Dictionary<char, Action<IVirtualMachine>> commandDictionary;

		public VirtualMachine(string program, int memorySize)
		{
			this.Instructions = program;
			this.InstructionPointer = 0;
			this.MemoryPointer = 0;
			this.Memory = new byte[memorySize];
			this.commandDictionary = new Dictionary<char, Action<IVirtualMachine>>();
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			this.commandDictionary[symbol] = execute;
		}

		public void Run()
		{
			while (InstructionPointer < Instructions.Length)
			{
				if (this.commandDictionary.TryGetValue(Instructions[InstructionPointer], out var action))
					action.Invoke(this);
				InstructionPointer++;
			}
		}
	}
}