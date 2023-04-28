# Manipulator
In this series of tasks I was supposed to code a robotic arm (kind of).

The concept is represented on the diagram down below.

<div align="center" >
  <img width="50%" src="/manipulator/manipulator.png">
</div>

And that's what the result looks like.

Moving the manipulator tip with a mouse|Changing alpha angle with a mouse wheel
:-------------------------:|:-------------------------:
![](/manipulator/Manipulator.gif) | ![](/manipulator/ManipulatorAlpha.gif)

## Installing

... to be added :alien:

## Controls

The green circle represents the valid positions for the manipulator. The manipulator disappears if the position is invalid and gets back after you put your cursor inside the circle.

* Move your `mouse` to change the manipulator's tip position;
* Press `Q` and `A` to increase/decrease `Shoulder` angle;
* Press `W` and `S` to increase/decrease `Elbow` angle;
* Use your mouse wheel to change `Alpha` angle.

## Tasks

:heavy_check_mark: [AnglesToCoordinatesTask](https://ulearn.me/course/basicprogramming/Praktika_Manipulyator__f460a5b6-3f82-4c55-9462-ac3fcf2d1888)

Firstly, I had to calculate joint coordinates by the angle values of `shoulder`, `elbow` and `wrist`.

So I've implemented `PointF[] GetJointPositions(double shoulder, double elbow, double wrist)` method in `AnglesToCoordinatesTask` class.  To make sure that it works properly, I have written unit tests covering the most crucial situations.

:heavy_check_mark: [VisualizerTask](https://ulearn.me/course/basicprogramming/Praktika_Vizualizatsiya__4f9ae5a0-2be9-4d2b-aa2c-7f99deaabf7a)

The next step was visualization and controls.

Added keyboard (`Q`, `A`, `S` and `W` buttons) and mouse wheel controls to regulate joint angles. In `DrawManipulator` method I draw the joints and limbs in their current state.

:heavy_check_mark: [TriangleTask](https://ulearn.me/course/basicprogramming/Praktika_Poisk_ugla__dd1993d4-6600-4368-bc9f-68055ef1eae4)

Implemented `double GetABAngle(a, b, c)` method. It returns an angle between `a` and `b` in the triangle with sides `a`, `b` and `c`.  Surely the triangle is allowed to be degenerate for the practical purposes. It means that any side might be equal to 0. In case of invalid arguments or inability to determine the angle the method returns `double.NaN`.

Unit tests are included.

:heavy_check_mark: [ManipulatorTask](https://ulearn.me/course/basicprogramming/Praktika_Reshenie_manipulyatora__2088e9aa-8fdd-4df3-b190-d57bb2390dfd)

Finally, I implemented `MoveManipulatorTo` method in `ManipulatorTask` class.
It returns `new[] {shoulder, elbow, wrist}` array of angles needed to bring the manipulator's tip to position `(x, y)` relative to the anchor `(0, 0)`. On top of that, the angle between the last joint and horizontal had to remain equal to `alpha`.
If impossible, it should return an array of three `double.NaN`.

Randomized unit test is included in the same file following ulearn.me task to pass the automatic platform tests.
