using System;
using System.Collections.Generic;

namespace Digger
{
    struct Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = this };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    class Player : ICreature
    {
        static string sprite = "Digger.png";
        static int drawPriority = 100;

        public CreatureCommand Act(int x, int y)
        {
            if ((Game.KeyPressed == System.Windows.Forms.Keys.L ||
                Game.KeyPressed == System.Windows.Forms.Keys.Left) &&
                IsPossibleToStep(x, y, -1, 0))
                return new CreatureCommand() { TransformTo = this, DeltaX = -1, DeltaY = 0 };
            if ((Game.KeyPressed == System.Windows.Forms.Keys.R ||
                Game.KeyPressed == System.Windows.Forms.Keys.Right) &&
                IsPossibleToStep(x, y, 1, 0))
                return new CreatureCommand() { TransformTo = this, DeltaX = 1, DeltaY = 0 };
            if ((Game.KeyPressed == System.Windows.Forms.Keys.U ||
                Game.KeyPressed == System.Windows.Forms.Keys.Up) &&
                IsPossibleToStep(x, y, 0, -1))
                return new CreatureCommand() { TransformTo = this, DeltaX = 0, DeltaY = -1 };
            if ((Game.KeyPressed == System.Windows.Forms.Keys.D ||
                Game.KeyPressed == System.Windows.Forms.Keys.Down) &&
                IsPossibleToStep(x, y, 0, 1))
                return new CreatureCommand() { TransformTo = this, DeltaX = 0, DeltaY = 1 };
            return new CreatureCommand() { TransformTo = this, DeltaX = 0, DeltaY = 0 };
        }

        public static Position GetLocation()
        {
            for (int i = 0; i < Game.MapWidth; i++)
                for (int j = 0; j < Game.MapHeight; j++)
                {
                    if (Game.Map[i, j]?.GetType() == typeof(Player))
                        return new Position(i, j);
                }
            return new Position(-1, -1);
        }

        public bool IsPossibleToStep(int currentX, int currentY, int deltaX, int deltaY)
        {
            if (currentX + deltaX < 0 ||
                currentX + deltaX > Game.Map.GetUpperBound(0) ||
                Game.Map[currentX + deltaX, currentY]?.GetType() == typeof(Sack))
                return false;
            return !(currentY + deltaY < 0 ||
               currentY + deltaY > Game.Map.GetUpperBound(1) ||
               Game.Map[currentX, currentY + deltaY]?.GetType() == typeof(Sack));
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Sack || conflictedObject is Monster)
            {
                return true;
            }
            return false;
        }

        public int GetDrawingPriority()
        {
            return drawPriority;
        }

        public string GetImageFileName()
        {
            return sprite;
        }
    }

    class Sack : ICreature
    {
        static string sprite = "Sack.png";
        static int drawPriority = 90;
        public int FallingState = 0;

        public CreatureCommand Act(int x, int y)
        {
            var creatureCommand = InitialiseCreatureCommandSack(new CreatureCommand());
            if (y == Game.Map.GetUpperBound(1))
            {
                if (FallingState > 1)
                {
                    creatureCommand.TransformTo = new Gold();
                }
                return creatureCommand;
            }
            if (Game.Map[x, y + 1] != null)
            {
                var mapSegmentType = Game.Map[x, y + 1]?.GetType().Name;
                if (IsSegmentAnyTypeOf(mapSegmentType, "Gold", "Sack", "Terrain"))
                {
                    if (FallingState > 1)
                    {
                        creatureCommand.TransformTo = new Gold();
                        FallingState = 0;
                    }
                    return creatureCommand;
                }
                if (IsSegmentAnyTypeOf(mapSegmentType, "Player"))
                {
                    if (FallingState > 0)
                    {
                        creatureCommand.DeltaY = 1;
                    }
                    return creatureCommand;
                }
                if (IsSegmentAnyTypeOf(mapSegmentType, "Monster"))
                {
                    if (FallingState > 0)
                    {
                        FallingState++;
                        creatureCommand.DeltaY = 1;
                    }
                    return creatureCommand;
                }
            }
            else
            {
                FallingState++;
                creatureCommand.DeltaY = 1;
            }
            return creatureCommand;
        }

        public bool IsSegmentAnyTypeOf(string segment, params string[] args)
        {
            if (segment == null)
                return false;
            foreach (var obj in args)
            {
                if (segment == obj)
                    return true;
            }
            return false;
        }

        public CreatureCommand InitialiseCreatureCommandSack(CreatureCommand creatureCommand)
        {
            creatureCommand.TransformTo = this;
            creatureCommand.DeltaX = creatureCommand.DeltaY = 0;
            return creatureCommand;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return drawPriority;
        }

        public string GetImageFileName()
        {
            return sprite;
        }
    }

    class Gold : ICreature
    {
        static string sprite = "Gold.png";
        static int drawPriority = 90;

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = this };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
            {
                Game.Scores += 10;
                return true;
            }
            if (conflictedObject is Monster)
            {
                return true;
            }
            return false;
        }

        public int GetDrawingPriority()
        {
            return drawPriority;
        }

        public string GetImageFileName()
        {
            return sprite;
        }
    }

    class Monster : ICreature
    {
        static string sprite = "Monster.png";
        static int drawPriority = 100;

        public CreatureCommand Act(int x, int y)
        {
            if (Player.GetLocation().X == -1)
                return new CreatureCommand() { TransformTo = this, DeltaX = 0, DeltaY = 0 };
            return GetDirection(x, y);
        }

        public CreatureCommand GetDirection(int x, int y)
        {
            var result = new CreatureCommand() { TransformTo = this, DeltaX = 0, DeltaY = 0 };
            double bestWay = GetDistanceToPlayer(x, y);
            bestWay = TryDirection(bestWay, x, y, -1, 0, result);
            bestWay = TryDirection(bestWay, x, y, 1, 0, result);
            bestWay = TryDirection(bestWay, x, y, 0, -1, result);
            bestWay = TryDirection(bestWay, x, y, 0, 1, result);
            return result;
        }

        public double TryDirection(double bestWay, int x, int y, int deltaX, int deltaY, CreatureCommand result)
        {
            var newBestWay = GetDistanceToPlayer(x + deltaX, y + deltaY);
            if (IsDirectionPossible(x + deltaX, y + deltaY) && newBestWay <= bestWay)
            {
                result.DeltaX = deltaX;
                result.DeltaY = deltaY;
                return newBestWay;
            }
            return bestWay;
        }

        public bool IsDirectionPossible(int x, int y)
        {
            return x >= 0 && x <= Game.MapWidth - 1 && y >= 0 && y <= Game.MapHeight - 1 &&
                Game.Map[x, y]?.GetType() != typeof(Terrain) &&
                Game.Map[x, y]?.GetType() != typeof(Sack) &&
                Game.Map[x, y]?.GetType() != typeof(Monster);
        }

        public double GetDistanceToPlayer(int x, int y)
        {
            var deltaX = Math.Abs(x - Player.GetLocation().X);
            var deltaY = Math.Abs(y - Player.GetLocation().Y);
            var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            return Math.Round(distance, 15);
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return drawPriority;
        }

        public string GetImageFileName()
        {
            return sprite;
        }
    }
}