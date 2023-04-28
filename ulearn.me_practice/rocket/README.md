# Rocket
This series of tasks leads to elements of functional programming practice using delegates.
The concept is to control the rocketman through levels with different gravity types. Arrows show the gravity direction and its length represents the scale of gravity.

<div align="center" >
  <img width="50%" src="https://raw.githubusercontent.com/ruPauk/c-sharp-projects/ulearn.me_practice/main/rocket/Rocketman.gif">
</div>

## Installing

... to be added :alien:

## Controls

* Press `A` and `D` to rotate the rocketman to Left/Right;
* You are able to choose a level by clicking on the corresponding button.


## Tasks

:heavy_check_mark: [ForcesTask](https://ulearn.me/course/basicprogramming2/Praktika_Lyambdy_i_delegaty__bd74c462-3495-4ddf-9bbf-1e0db847d071)

Practicing delegates. The task was to dive into the initial project and to implement three methods responsible for gravity and forces that affect the rocketman. As result the rocketman is able to fly and be controlled with `A` and `D` buttons.

:heavy_check_mark: [LevelsTask](https://ulearn.me/course/basicprogramming2/Praktika_Urovni__f9e8c237-cf28-4839-b9eb-d5b18003fc7d)

Here the goal was to implement 6 levels with different gravities:
- Zero - without gravity;
- Heavy - constant 0.9 downward;
- Up - upward gravity calculated by the formula 300 / (d + 300.0);
- WhiteHole - directed from the target and evaluated from the formula 140 * d / (d * d + 1);
- BlackHole - there is an anomaly between rocketman and target. Gravity is directed to the anomaly. Scalar of gravity vector is equal to 300 * d / (d * d + 1);
- BlackAndWhite - is equal to arithmetic mean of WhiteHole and BlackHole.

:heavy_check_mark: [ControlTask](https://ulearn.me/course/basicprogramming2/Praktika_Upravlenie__71eef94d-7f70-4bad-9606-3c8a3e903b30)

And final task was to make the rocket go through first four level automatically without manual control.