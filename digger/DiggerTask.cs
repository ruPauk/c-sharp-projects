using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var resAct = new CreatureCommand();
            resAct.DeltaX = resAct.DeltaY = 0;
            resAct.TransformTo = this;
            return resAct;
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
            var creatureCommand = new CreatureCommand();
            switch (Game.KeyPressed)
            {
                case System.Windows.Forms.Keys.Left:
                case System.Windows.Forms.Keys.L:
                    if (IsPossibleToStep(x, y, -1, 0))
                        return GetCreatureCommand(creatureCommand, -1, 0);
                    break;
                case System.Windows.Forms.Keys.Right:
                case System.Windows.Forms.Keys.R:
                    if (IsPossibleToStep(x, y, 1, 0))
                        return GetCreatureCommand(creatureCommand, 1, 0);
                    break;
                case System.Windows.Forms.Keys.Up:
                case System.Windows.Forms.Keys.U:
                    if (IsPossibleToStep(x, y, 0, -1))
                        return GetCreatureCommand(creatureCommand, 0, -1);
                    break;
                case System.Windows.Forms.Keys.Down:
                case System.Windows.Forms.Keys.D:
                    if (IsPossibleToStep(x, y, 0, 1))
                        return GetCreatureCommand(creatureCommand, 0, 1);
                    break;
            }
            creatureCommand = GetCreatureCommand(creatureCommand, 0, 0);
            return creatureCommand;
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

        public CreatureCommand GetCreatureCommand(CreatureCommand creatureCommand, int deltaX, int deltaY)
        {
            creatureCommand.DeltaX = deltaX;
            creatureCommand.DeltaY = deltaY;
            creatureCommand.TransformTo = this;
            return creatureCommand;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack;
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
}