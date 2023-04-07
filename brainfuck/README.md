# [Brainfuck](https://en.wikipedia.org/wiki/Brainfuck) interpreter
The task was to implement an interpreter for the well known minimalistic programming language Brainfuck.
This task served as delegates training.

<div align="center" >
  <img width="50%" src="/brainfuck/Brainfuck.gif">
</div>

## Installing

... to be added :alien:

## Controls

You are supposed to enter Brainfuck commands into the console and press `ENTER`.
For example, enter "++++++++[>++++++++<-]>+." (without quotation marks) to get an "A" symbol printed out.

## Tasks

:heavy_check_mark: [VirtualMachineTask](https://ulearn.me/course/basicprogramming2/Praktika_Virtual_naya_mashina_Brainfuck__6616377b-e3f9-43f7-9fb8-a9d6c921f1ef)

The first task was to implement some sort of virtual machine. The concept is to store possible commands as actions with possibility to easily extent command pool.
Aslo is has to store instructions and execute them in the given order.

:heavy_check_mark: [BrainfuckBasicCommandsTask](https://ulearn.me/course/basicprogramming2/Praktika_Prostye_komandy_Brainfuck__5eb14a3a-d030-4ca0-8f39-79daa0ba48ec)

Here I had to register the standard pool of Brainfuck commands except loops.
<div align="center" >
  <img width="50%" src="/brainfuck/brainfuck_commands.png">
</div>

:heavy_check_mark: [BrainfuckLoopCommandsTask](https://ulearn.me/course/basicprogramming2/Praktika_Tsikly_Brainfuck__32596182-f915-402b-8b64-df3b63198691)

And here I had to make loops work.