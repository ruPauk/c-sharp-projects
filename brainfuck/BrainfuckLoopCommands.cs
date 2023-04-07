using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		private static Dictionary<int, int> _openingBrackets;
		private static Dictionary<int, int> _closingBrackets;
		private static void InitializeDictionaries(IVirtualMachine vm)
		{
			_openingBrackets = new Dictionary<int, int>();
			_closingBrackets = new Dictionary<int, int>();
			var stack = new Stack<int>();
			for (var i = 0; i < vm.Instructions.Length; i++)
			{
				if (vm.Instructions[i] == '[')
				{
					stack.Push(i);
				}
				else if (vm.Instructions[i] == ']')
				{
					var opening = stack.Pop();
					if (!_openingBrackets.ContainsKey(opening)
						&& !_closingBrackets.ContainsKey(i))
					{
						_openingBrackets.Add(opening, i);
						_closingBrackets.Add(i, opening);
					}
				}
			}
		}

		public static void RegisterTo(IVirtualMachine vm)
		{
			InitializeDictionaries(vm);
			vm.RegisterCommand('[', b => {
				if (b.Memory[b.MemoryPointer] == 0)
					b.InstructionPointer = _openingBrackets[b.InstructionPointer];
			});
			vm.RegisterCommand(']', b => {
				if (b.Memory[b.MemoryPointer] > 0)
					b.InstructionPointer = _closingBrackets[b.InstructionPointer];
			});
		}
	}
}